using Microsoft.EntityFrameworkCore;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Api.Common;

public class CustomCleaner<T>(IServiceScopeFactory serviceScopeFactory) : BackgroundService where T : TempEntity
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = serviceScopeFactory.CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<TasklyDbContext>();

                DbSet<T> cleanerSet = dbContext.Set<T>();

                var deletedEntities = await cleanerSet.Where(ve => ve.EndTime <= DateTime.UtcNow).ToListAsync(stoppingToken);
                if (deletedEntities.Any())
                {
                    cleanerSet.RemoveRange(deletedEntities);
                    await dbContext.SaveChangesAsync(stoppingToken);

                }

                var delay = TimeSpan.FromMinutes(5);
                var oldEntity = await cleanerSet.OrderBy(ve => ve.EndTime).FirstOrDefaultAsync(stoppingToken);
                if (oldEntity != null)
                {
                    delay = oldEntity.EndTime - DateTime.UtcNow;
                    if (delay < TimeSpan.Zero) delay = TimeSpan.FromSeconds(5);
                }
                await Task.Delay(delay, stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CustomCleaner <{nameof(T)}>: {ex.Message}");
            }

        }
    }
}

/*
 protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = serviceScopeFactory.CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<TasklyDbContext>();

                var deletedVerificationEmails = await dbContext.EmailVerifications.Where(ve => ve.EndTime <= DateTime.UtcNow).ToListAsync(stoppingToken);
                if (deletedVerificationEmails.Any())
                {
                    dbContext.EmailVerifications.RemoveRange(deletedVerificationEmails);
                    await dbContext.SaveChangesAsync(stoppingToken);

                }

                var delay = TimeSpan.FromMinutes(5);
                var oldersVerificationEmail = await dbContext.EmailVerifications.OrderBy(ve => ve.EndTime).FirstOrDefaultAsync(stoppingToken);
                if (oldersVerificationEmail != null)
                {
                    delay = oldersVerificationEmail.EndTime - DateTime.UtcNow;
                    if (delay < TimeSpan.Zero) delay = TimeSpan.FromSeconds(5);
                }
                await Task.Delay(delay, stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in VerifivationEmailCleaner: {ex.Message}");
            }

        }
    }
 */
