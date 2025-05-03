using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface IFeedbackRepository : IRepository<FeedbackEntity>
{
    Task<List<FeedbackEntity>> GetAllFeedbacksAsync();
    Task<FeedbackEntity> GetFeedbackById(Guid id);
}
