using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor,
    TasklyDbContext context) : ICurrentUserService
{
    public string GetCurrentUserId()
    {
        var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
        return userIdClaim?.Value ?? string.Empty;
    }

    public async Task<UserEntity> GetUserById(Guid userId)
    {
        if(userId == Guid.Empty)
            throw new ArgumentException("User id is empty");
        var user = await context.Users.FindAsync(userId);
        if(user == null)
            throw new ArgumentException("User not found");
        return user;
    }
}