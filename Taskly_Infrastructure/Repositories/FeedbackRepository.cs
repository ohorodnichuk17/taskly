using Microsoft.EntityFrameworkCore;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class FeedbackRepository(TasklyDbContext tasklyDbContext)
    : Repository<FeedbackEntity>(tasklyDbContext), IFeedbackRepository
{
    public async Task<List<FeedbackEntity>> GetAllFeedbacksAsync() =>
        await dbSet.Include(x => x.User)
            .ToListAsync();

    public async Task<FeedbackEntity> GetFeedbackByIdAsync(Guid id) =>
        await dbSet
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<int> CountUserFeedbacksAsync(Guid userId) =>
        await dbSet
            .CountAsync(x => x.UserId == userId);
}