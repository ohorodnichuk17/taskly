using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Api.Common;

public static class DataInitializer
{
    public static async void Initialize(this IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var service = scope.ServiceProvider;

        var dbContext = service.GetRequiredService<TasklyDbContext>();

        if (!dbContext.Avatars.Any())
        {
            AvatarEntity[] avatars = [
                new AvatarEntity()
                {
                    Id = Guid.NewGuid(),
                    ImagePath = "corn"
                },
                new AvatarEntity()
                {
                    Id = Guid.NewGuid(),
                    ImagePath = "crab"
                },
                new AvatarEntity()
                {
                    Id = Guid.NewGuid(),
                    ImagePath = "fish"
                },
                new AvatarEntity()
                {
                    Id = Guid.NewGuid(),
                    ImagePath = "flamingo"
                },
                new AvatarEntity()
                {
                    Id = Guid.NewGuid(),
                    ImagePath = "octopus"
                },
                new AvatarEntity()
                {
                    Id = Guid.NewGuid(),
                    ImagePath = "orange"
                },
                new AvatarEntity()
                {
                    Id = Guid.NewGuid(),
                    ImagePath = "pineapple"
                },
                new AvatarEntity()
                {
                    Id = Guid.NewGuid(),
                    ImagePath = "rabbit"
                },
                new AvatarEntity()
                {
                    Id = Guid.NewGuid(),
                    ImagePath = "star"
                },
                new AvatarEntity()
                {
                    Id = Guid.NewGuid(),
                    ImagePath = "watermelon"
                }
            ];

            await dbContext.Avatars.AddRangeAsync(avatars);    
        }
        await dbContext.SaveChangesAsync();
    }
}
