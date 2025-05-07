using System.Linq.Expressions;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class AuthenticationRepository(UserManager<UserEntity> userManager, TasklyDbContext tasklyDbContext) : Repository<UserEntity>(tasklyDbContext),IAuthenticationRepository 
{

    private readonly DbSet<VerificationEmailEntity> _verificationEmailEntities = tasklyDbContext.Set<VerificationEmailEntity>();
    private readonly DbSet<ChangePasswordKeyEntity> _changePasswordKeyEntity = tasklyDbContext.Set<ChangePasswordKeyEntity>();
    private readonly DbSet<UserEntity> _userEntity = tasklyDbContext.Set<UserEntity>();

    public async Task<bool> IsUserExist(string email) =>
        await userManager.FindByEmailAsync(email) != null;
    public async Task<bool> IsUserExist(Guid id) =>
        await userManager.FindByIdAsync(id.ToString()) != null;

    public async Task<string> AddVerificationEmail(string email, string code)
    {
        await _verificationEmailEntities.AddAsync(new VerificationEmailEntity() {
            Id = Guid.NewGuid(),
            Email = email,
            Code = code
        });
        await tasklyDbContext.SaveChangesAsync();

        return email;
    }

    public async Task<bool> IsVerificationEmailExistAndCodeValid(string email, string code) =>
        await _verificationEmailEntities.AnyAsync(e => e.Email == email && e.Code == code);

    public async Task VerificateEmail(string email)
    {
        var verificateEmail = await _verificationEmailEntities.FirstOrDefaultAsync(e => e.Email == email);
        if(verificateEmail != null)
        {
            _verificationEmailEntities.Remove(verificateEmail);
            await tasklyDbContext.SaveChangesAsync();
        }
    }

    public async Task<ErrorOr<UserEntity>> CreateNewUser(UserEntity newUser,string password)
    {
        var result = await userManager.CreateAsync(newUser,password);

        if(!result.Succeeded && result.Errors.Any())
            return Error.Conflict(result.Errors.FirstOrDefault()!.Description);

        return newUser;
    }
    public async Task<UserEntity?> GetUserByEmail(string email) => 
        await GetUserByConditionAsync(u => u.Email == email);
    public async Task<UserEntity?> GetUserByPublicKey(string publicKey) => 
        await GetUserByConditionAsync(u => u.PublicKey == publicKey);
    
    public async Task<bool> IsPasswordValid(UserEntity user, string password)
    {
        return await userManager.CheckPasswordAsync(user, password);
    }
    public async Task AddChangePasswordKey(string email, Guid key)
    {
        var changePasswordKeyEntity = await _changePasswordKeyEntity.FirstOrDefaultAsync(c => c.Email == email);
        if(changePasswordKeyEntity != null)
        {
             _changePasswordKeyEntity.Remove(changePasswordKeyEntity);
        }
        await _changePasswordKeyEntity.AddAsync(
            new ChangePasswordKeyEntity() {
            Key = key,
            Email = email 
        });

        await tasklyDbContext.SaveChangesAsync();
    }
    public async Task<string?> GetUserEmailByChangePasswordKeyAsync(Guid key)
    {
        var user = await _changePasswordKeyEntity.FirstOrDefaultAsync(c => c.Key == key);
        if(user == null)
            return null;
        return user.Email;
    }
    public async Task<bool> HasUserSentRequestToChangePassword(string email, Guid key)
    {
        var changePasswordKeyEntity = await _changePasswordKeyEntity.FirstOrDefaultAsync(c => c.Key == key && c.Email == email);

        return changePasswordKeyEntity != null;
    }
    public async Task<ErrorOr<Guid>> ChangePasswordAsync(UserEntity user, string password)
    {
        var resetPasswordToken = await userManager.GeneratePasswordResetTokenAsync(user);
        var result = await userManager.ResetPasswordAsync(user, resetPasswordToken, password);
        if (result.Succeeded)
        {     
            return user.Id;
        }
        return Error.Conflict(result.Errors.FirstOrDefault()!.Description);
    }

    public async Task<ErrorOr<UserEntity>> SetUserNameForSolanaUserAsync(string publicKey, string userName)
    {
        var user = await GetUserByPublicKey(publicKey);
        if (user == null)
            return Error.NotFound("User not found");
        user.UserName = userName;
        await SaveAsync(user);
        return user;
    }

    public async Task<string> GenerateReferralCode()
    {
        string code;
        bool exists;

        do
        {
            code = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6).ToUpper();
            exists = await tasklyDbContext.Users.AnyAsync(u => u.ReferralCode == code);
        } while (exists);

        return code;
    }

    public async Task<UserEntity> GetUserByReferralCodeAsync(string referralCode) =>
        await _userEntity
            .Include(u => u.Avatar)
            .FirstOrDefaultAsync(u => u.ReferralCode == referralCode);

    private async Task<UserEntity?> GetUserByConditionAsync(Expression<Func<UserEntity, bool>> predicate)
    {
        return await _userEntity
            .Include(u => u.Avatar)
            .FirstOrDefaultAsync(predicate);
    }
}
