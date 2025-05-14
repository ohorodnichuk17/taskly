using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Taskly_Infrastructure.Services;

public class UserService(IHttpContextAccessor httpContextAccessor,
    TasklyDbContext context) : IUserService
{
    public string GetCurrentUserId()
    {
        var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
        return userIdClaim?.Value ?? string.Empty;
    }

    public async Task<UserEntity> GetUserByIdAsync(Guid userId)
    {
        if(userId == Guid.Empty)
            throw new ArgumentException("User id is empty");
        return await GetUserByConditionAsync(u => u.Id == userId);
    }

    public async Task<UserEntity> GetUserByEmailAsync(string email)
    {
        if(email == null)
            throw new ArgumentNullException(nameof(email));
        return await GetUserByConditionAsync(u => u.Email == email);
    }

    public Task<UserEntity> UpdateUserAsync(UserEntity user)
    {
        if(user == null)
            throw new ArgumentNullException(nameof(user));
        context.Users.Update(user);
        context.SaveChanges();
        return Task.FromResult(user);
    }

    private async Task<UserEntity> GetUserByConditionAsync(Expression<Func<UserEntity, bool>> predicate)
    {
        var user =  await context.Users.FirstOrDefaultAsync(predicate);
        if(user == null)
            throw new ArgumentException("User not found");
        return user;
    }
}