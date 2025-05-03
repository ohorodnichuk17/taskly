using Microsoft.EntityFrameworkCore;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class FeedbackRepository(TasklyDbContext tasklyDbContext) 
    : Repository<FeedbackEntity>(tasklyDbContext), IFeedbackRepository
{
    public Task<List<FeedbackEntity>> GetAllFeedbacksAsync() => 
        dbSet.Include(x => x.User)
            .ToListAsync();

    public Task<FeedbackEntity> GetFeedbackById(Guid id) => 
        dbSet
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);
}
