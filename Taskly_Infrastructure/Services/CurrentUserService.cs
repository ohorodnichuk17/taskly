using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Taskly_Infrastructure.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor,
    TasklyDbContext context) : ICurrentUserService
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
    
    private async Task<UserEntity> GetUserByConditionAsync(Expression<Func<UserEntity, bool>> predicate)
    {
        var user =  await context.Users.FirstOrDefaultAsync(predicate);
        if(user == null)
            throw new ArgumentException("User not found");
        return user;
    }
}