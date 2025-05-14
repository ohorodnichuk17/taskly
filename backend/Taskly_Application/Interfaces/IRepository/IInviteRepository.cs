using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface IInviteRepository : IRepository<InviteEntity>
{
    Task<int> CountSuccessfulInvitesAsync(Guid userId);
}
