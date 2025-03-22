using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class AuthenticationRepository(UserManager<UserEntity> userManager, TasklyDbContext tasklyDbContext) : Repository<UserEntity>(tasklyDbContext),IAuthenticationRepository 
{

    readonly private DbSet<VerificationEmailEntity> _verificationEmailEntities = tasklyDbContext.Set<VerificationEmailEntity>();
    readonly private DbSet<ChangePasswordKeyEntity> _changePasswordKeyEntity = tasklyDbContext.Set<ChangePasswordKeyEntity>();
    readonly private DbSet<UserEntity> _userEntity = tasklyDbContext.Set<UserEntity>();

    public async Task<bool> IsUserExist(string Email)
    {
        return await userManager.FindByEmailAsync(Email) != null;
    }

    public async Task<string> AddVerificationEmail(string Email, string Code)
    {
        await _verificationEmailEntities.AddAsync(new VerificationEmailEntity() {
            Id = Guid.NewGuid(),
            Email = Email,
            Code = Code
        });
        await tasklyDbContext.SaveChangesAsync();

        return Email;
    }

    public async Task<bool> IsVerificationEmailExistAndCodeValid(string Email, string Code)
    {
       return await _verificationEmailEntities.AnyAsync(e => e.Email == Email && e.Code == Code);
    }

    public async Task VerificateEmail(string Email)
    {
        var verificateEmail = await _verificationEmailEntities.FirstOrDefaultAsync(e => e.Email == Email);
        if(verificateEmail != null)
        {
            _verificationEmailEntities.Remove(verificateEmail);
            await tasklyDbContext.SaveChangesAsync();
        }
    }

    public async Task<ErrorOr<UserEntity>> CreateNewUser(UserEntity NewUser,string Password)
    {
        var result = await userManager.CreateAsync(NewUser,Password);

        if(!result.Succeeded && result.Errors.Any())
            return Error.Conflict(result.Errors.FirstOrDefault()!.Description);

        return NewUser;
    }
    public async Task<UserEntity?> GetUserByEmail(string Email)
    {
        return await _userEntity.Include(u => u.Avatar).FirstOrDefaultAsync(u => u.Email == Email);
    }

    public async Task<bool> IsPasswordValid(UserEntity User, string Password)
    {
        return await userManager.CheckPasswordAsync(User, Password);
    }
    public async Task AddChangePasswordKey(string Email, Guid Key)
    {
        var changePasswordKeyEntity = await _changePasswordKeyEntity.FirstOrDefaultAsync(c => c.Email == Email);
        if(changePasswordKeyEntity != null)
        {
             _changePasswordKeyEntity.Remove(changePasswordKeyEntity);
        }
        await _changePasswordKeyEntity.AddAsync(
            new ChangePasswordKeyEntity() {
            Key = Key,
            Email = Email 
        });

        await tasklyDbContext.SaveChangesAsync();
    }
    public async Task<string?> GetUserEmailByChangePasswordKeyAsync(Guid Key)
    {
        var user = await _changePasswordKeyEntity.FirstOrDefaultAsync(c => c.Key == Key);
        if(user == null)
            return null;
        return user.Email;
    }
    public async Task<bool> HasUserSentRequestToChangePassword(string Email, Guid Key)
    {
        var changePasswordKeyEntity = await _changePasswordKeyEntity.FirstOrDefaultAsync(c => c.Key == Key && c.Email == Email);

        return changePasswordKeyEntity != null;
    }
    public async Task<ErrorOr<Guid>> ChangePasswordAsync(UserEntity user, string Password)
    {
        var resetPasswordToken = await userManager.GeneratePasswordResetTokenAsync(user);
        var result = await userManager.ResetPasswordAsync(user, resetPasswordToken, Password);
        if (result.Succeeded)
        {     
            return user.Id;
        }
        return Error.Conflict(result.Errors.FirstOrDefault()!.Description);
    }

}
