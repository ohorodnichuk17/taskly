using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface IAvatarRepository
{
    Task<AvatarEntity?> GetAvatarById(Guid AvatarId);
}
