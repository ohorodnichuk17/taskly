using Microsoft.EntityFrameworkCore;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class ChallengeRepository(TasklyDbContext tasklyDbContext, 
    IRuleEvaluatorService ruleEvaluatorService) 
    : Repository<ChallengeEntity>(tasklyDbContext), IChallengeRepository
{
    public async Task<IEnumerable<ChallengeEntity>> GetAllChallengesAsync() =>
        await tasklyDbContext
            .Challenges
            .Include(u => u.User)
            .Include(t => t.TimeRange)
            .ToListAsync();

    public async Task<IEnumerable<ChallengeEntity>> GetAllActiveChallengesAsync() =>
        await tasklyDbContext
            .Challenges
            .Where(x => x.IsActive)
            .Include(u => u.User)
            .Include(t => t.TimeRange)
            .ToListAsync();

    public async Task<IEnumerable<ChallengeEntity>> GetAvailableChallengesAsync() =>
        await tasklyDbContext
            .Challenges
            .Where(x => x.IsActive && !x.IsBooked)
            .Include(u => u.User)
            .Include(t => t.TimeRange)
            .ToListAsync();

    public Task<ChallengeEntity?> GetChallengeByIdAsync(Guid id) =>
        tasklyDbContext.Challenges
            .Include(x => x.User)
            .Include(t => t.TimeRange)
            .FirstOrDefaultAsync(x => x.Id == id);
    
    public async Task BookChallengeAsync(Guid challengeId, Guid userId)
    {
        var challenge = await tasklyDbContext.Challenges
            .FirstOrDefaultAsync(x => x.Id == challengeId);
        var userExists = await tasklyDbContext.Users.AnyAsync(u => u.Id == userId);
        if (!userExists)
            throw new InvalidOperationException("User not found");
        if (challenge == null)
            throw new InvalidOperationException("Challenge not found");
        if (challenge.IsBooked)
            throw new InvalidOperationException("Challenge already booked");
        challenge.IsBooked = true;
        challenge.UserId = userId;
        await SaveAsync(challenge);
    }

    public async Task CompleteChallengeAsync(Guid challengeId)
    {
        var challenge = await tasklyDbContext.Challenges.FirstOrDefaultAsync(
            x => x.Id == challengeId);
        
        if(challenge == null)
            throw new InvalidOperationException("Challenge not found");
        if(challenge.IsCompleted)
            throw new InvalidOperationException("Challenge already completed");
        if(challenge.UserId == null)
            throw new InvalidOperationException("Challenge is not booked by any user");

        int progress = await ruleEvaluatorService.EvaluateRuleAsync(
            challenge.RuleKey,
            challenge.UserId.Value);
        
        if (progress < challenge.TargetAmount)
            throw new InvalidOperationException($"Challenge not yet completed. Progress: {progress}/{challenge.TargetAmount}");
        
        challenge.IsCompleted = true;
        
        var user = await tasklyDbContext.Users
            .FirstOrDefaultAsync(u => u.Id == challenge.UserId);
        if (user == null)
            throw new InvalidOperationException("User not found");
        
        user.SolBalance += challenge.Points;
        tasklyDbContext.Users.Update(user);
        await tasklyDbContext.SaveChangesAsync();

        await SaveAsync(challenge);
    }
}
