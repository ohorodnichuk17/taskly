using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class AuthenticationRepository(UserManager<UserEntity> userManager, TasklyDbContext tasklyDbContext) : IAuthenticationRepository
{
    readonly private DbSet<VerificationEmailEntity> _verificationEmailEntities = tasklyDbContext.Set<VerificationEmailEntity>();
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

    public async Task CreateNewUser(UserEntity NewUser,string Password)
    {
        await userManager.AddPasswordAsync(NewUser, Password);
        await tasklyDbContext.Users.AddAsync(NewUser);
        await tasklyDbContext.SaveChangesAsync();
    }
    public async Task<UserEntity?> GetUserByEmail(string Email)
    {
        return await userManager.FindByEmailAsync(Email);
    }
    public async Task<bool> IsPasswordValid(UserEntity User, string Password)
    {
        return await userManager.CheckPasswordAsync(User, Password);
    }
}
