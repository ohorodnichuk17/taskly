using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IService;

public interface IUserService
{
    string GetCurrentUserId();
    Task<UserEntity> GetUserByIdAsync(Guid userId);
    Task<UserEntity> GetUserByEmailAsync(string email);
    Task<UserEntity> UpdateUserAsync(UserEntity user);
}