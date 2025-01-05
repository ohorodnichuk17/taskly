using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;
using Constants = Taskly_Domain.Constants;

namespace Taskly_Infrastructure.Common.Seeder;

public static class DataInitializer
{
    public static async Task InitializeData(this IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TasklyDbContext>();

        await InitializeAvatarsAsync(dbContext);
        await InitializeDashboardTemplatesAsync(dbContext);
        await InitializeBoardsAsync(dbContext);
        await dbContext.SaveChangesAsync();
    }

    private static async Task InitializeAvatarsAsync(TasklyDbContext dbContext)
    {
        if (!dbContext.Avatars.Any())
        {
            var avatars = new List<AvatarEntity>
            {
                new() { Id = Guid.NewGuid(), ImagePath = "corn" },
                new() { Id = Guid.NewGuid(), ImagePath = "crab" },
                new() { Id = Guid.NewGuid(), ImagePath = "fish" },
                new() { Id = Guid.NewGuid(), ImagePath = "flamingo" },
                new() { Id = Guid.NewGuid(), ImagePath = "octopus" },
                new() { Id = Guid.NewGuid(), ImagePath = "orange" },
                new() { Id = Guid.NewGuid(), ImagePath = "pineapple" },
                new() { Id = Guid.NewGuid(), ImagePath = "rabbit" },
                new() { Id = Guid.NewGuid(), ImagePath = "star" },
                new() { Id = Guid.NewGuid(), ImagePath = "watermelon" }
            };

            await dbContext.Avatars.AddRangeAsync(avatars);
        }
    }

    private static async Task InitializeDashboardTemplatesAsync(TasklyDbContext dbContext)
    {
        if (!dbContext.BoardTemplates.Any())
        {
            var boardTemplates = new List<BoardTemplateEntity>
            {
                new() { Id = Guid.NewGuid(), ImagePath = "black_default", Name = "Black default" },
                new() { Id = Guid.NewGuid(), ImagePath = "white_default", Name = "White default" },
                new() { Id = Guid.NewGuid(), ImagePath = "blue_default", Name = "Blue default" },
                new() { Id = Guid.NewGuid(), ImagePath = "dark_texture", Name = "Dark texture" },
                new() { Id = Guid.NewGuid(), ImagePath = "grunge_white", Name = "Grunge white" },
                new() { Id = Guid.NewGuid(), ImagePath = "light_blue_frost", Name = "Light blue frost" },
                new() { Id = Guid.NewGuid(), ImagePath = "minimal_texture", Name = "Minimal texture" },
                new() { Id = Guid.NewGuid(), ImagePath = "neon_wave", Name = "Neon wave" },
                new() { Id = Guid.NewGuid(), ImagePath = "pink_cloud", Name = "Pink cloud" },
                new() { Id = Guid.NewGuid(), ImagePath = "soft_pink_blur", Name = "Soft pink blur" },
                new() { Id = Guid.NewGuid(), ImagePath = "vibrant_gradient", Name = "Vibrant gradient" }
            };

            await dbContext.BoardTemplates.AddRangeAsync(boardTemplates);
        }
    }
    
    private static async Task InitializeBoardsAsync(TasklyDbContext dbContext)
{
    if (!dbContext.Boards.Any())
    {
        var timeRange1 = new TimeRangeEntity
        {
            Id = Guid.NewGuid(),
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddHours(2)
        };

        await dbContext.TimeRanges.AddAsync(timeRange1);
        await dbContext.SaveChangesAsync();

        var boards = new List<BoardEntity>
        {
            new BoardEntity
            {
                Id = Guid.NewGuid(),
                Name = "Sample Board",
                IsTeamBoard = false,
                Tag = "Template",
                CardLists = GetDefaultCardLists(timeRange1)
            }
        };

        await dbContext.Boards.AddRangeAsync(boards);
        await dbContext.SaveChangesAsync();

        foreach (var board in boards)
        {
            foreach (var cardList in board.CardLists)
            {
                cardList.BoardId = board.Id; 
                foreach (var card in cardList.Cards)
                {
                    card.CardListId = cardList.Id;
                }
            }
        }

        await dbContext.SaveChangesAsync();
    }
}

private static List<CardListEntity> GetDefaultCardLists(TimeRangeEntity timeRange1)
{
    return new List<CardListEntity>
    {
        new CardListEntity
        {
            Id = Guid.NewGuid(),
            Title = Constants.Todo,
            Cards = new List<CardEntity>
            {
                new CardEntity
                {
                    Id = Guid.NewGuid(), Description = "This is a card! 👋 Select it to see its card back.",
                    Status = Constants.Todo
                },
                new CardEntity
                {
                    Id = Guid.NewGuid(), Description = "Hold and drag to move this card to another list 👉",
                    Status = Constants.Todo
                },
                new CardEntity
                {
                    Id = Guid.NewGuid(), Description = "Invite collaborators to this board to work together! 👥",
                    Status = Constants.Todo
                }
            }
        },

        new CardListEntity
        {
            Id = Guid.NewGuid(),
            Title = Constants.Inprogress,
            Cards = new List<CardEntity>
            {
                new CardEntity
                {
                    Id = Guid.NewGuid(), AttachmentUrl = "example_attachment",
                    Description = "This card has an attachment.", Status = "InProgress"
                },
                new CardEntity
                {
                    Id = Guid.NewGuid(), 
                    Description = "This card has a time range.", 
                    Status = "InProgress", 
                    TimeRangeEntity = timeRange1
                }
            }
        },

        new CardListEntity
        {
            Id = Guid.NewGuid(),
            Title = Constants.Done,
            Cards = new List<CardEntity>
            {
                new CardEntity { Id = Guid.NewGuid(), Description = "Signed up for Taskly! ��", Status = "Done" }
            }
        }
    };
}
}
