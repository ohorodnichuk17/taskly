using Microsoft.EntityFrameworkCore;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class UserLevelRepository(TasklyDbContext tasklyDbContext) 
    : Repository<UserLevelEntity>(tasklyDbContext), IUserLevelRepository
{
    public async Task<int> GetLevelPropertyByUserIdAsync(Guid userId)
    {
        var userLevel = await tasklyDbContext.UserLevels
            .FirstOrDefaultAsync(x => x.UserId == userId);
        if (userLevel == null)
            throw new Exception("User level not found");
        return userLevel.Level;
    }

    public async Task<int> IncreaseCompletedTasksAsync(Guid userId)
    {
        var userLevel = await tasklyDbContext.UserLevels
            .FirstOrDefaultAsync(x => x.UserId == userId);
        if (userLevel == null)
            throw new Exception("User level not found");

        userLevel.CompletedTasks++; 
    
        tasklyDbContext.UserLevels.Update(userLevel);
        await tasklyDbContext.SaveChangesAsync();  

        return userLevel.CompletedTasks;  
    }

    public void Update(UserLevelEntity userLevel) =>
        tasklyDbContext.UserLevels.Update(userLevel);

    public async Task<UserLevelEntity> GetUserLevelByUserIdAsync(Guid userId) =>
        await tasklyDbContext.UserLevels
            .FirstOrDefaultAsync(x => x.UserId == userId)
            ?? throw new Exception("User level not found");

}
