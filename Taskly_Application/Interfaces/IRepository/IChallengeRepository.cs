using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface IChallengeRepository : IRepository<ChallengeEntity>
{
    Task<IEnumerable<ChallengeEntity>> GetAllChallengesAsync();
    Task<IEnumerable<ChallengeEntity>> GetAllActiveChallengesAsync();
    Task<IEnumerable<ChallengeEntity>> GetAvailableChallengesAsync();
    Task<ChallengeEntity?> GetChallengeByIdAsync(Guid id);
    Task BookChallengeAsync(Guid challengeId, Guid userId);
    Task CompleteChallengeAsync(Guid challengeId);
}
