using Microsoft.EntityFrameworkCore;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Api.Common;

public class VerificationEmailCleaner(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
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

                var delay = TimeSpan.FromMinutes(1);
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
}
