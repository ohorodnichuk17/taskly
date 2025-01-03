using Microsoft.EntityFrameworkCore;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class AvatarRepository(TasklyDbContext tasklyDbContext) : IAvatarRepository
{
    private readonly DbSet<AvatarEntity> dbSet = tasklyDbContext.Set<AvatarEntity>();
    public async Task<AvatarEntity?> GetAvatarById(Guid AvatarId)
    {
        return await dbSet.FirstOrDefaultAsync(a => a.Id == AvatarId);
    }
}
