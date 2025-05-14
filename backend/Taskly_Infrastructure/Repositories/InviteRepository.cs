using Microsoft.EntityFrameworkCore;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class InviteRepository (TasklyDbContext tasklyDbContext) 
    : Repository<InviteEntity>(tasklyDbContext), IInviteRepository
{
    public async Task<int> CountSuccessfulInvitesAsync(Guid userId) =>
        await tasklyDbContext.Invites
            .Where(i => i.InvitedByUserId == userId &&
                        i.RegisteredUserId != null &&
                        i.RegisteredUserId != userId)
            .CountAsync();

}
