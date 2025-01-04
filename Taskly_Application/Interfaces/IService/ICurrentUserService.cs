using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IService;

public interface ICurrentUserService
{
    string GetCurrentUserId();
    Task<UserEntity> GetUserById(Guid userId);
}