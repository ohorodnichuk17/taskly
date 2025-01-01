using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces;

public interface IAvatarRepository
{
    Task<AvatarEntity?> GetAvatarById(Guid AvatarId);
}
