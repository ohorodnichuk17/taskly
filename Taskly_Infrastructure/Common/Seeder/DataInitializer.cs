using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Taskly_Domain;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Common.Seeder;

public static class DataInitializer
{
   public static async Task InitializeData(this IApplicationBuilder applicationBuilder)
   {
      using var scope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
      var dbContext = scope.ServiceProvider.GetRequiredService<TasklyDbContext>();

      await InitializeBoardsAsync(dbContext);
      await InitializeDashboardTemplatesAsync(dbContext);
      await InitializeAvatarsAsync(dbContext);
      await InitializeAchievementsAsync(dbContext);
      await InitializeRolesAndAdminAsync(applicationBuilder, dbContext);
      await InitializeBadgesAsync(dbContext);
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
         await dbContext.SaveChangesAsync();
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

           var board = new BoardEntity
           {
               Id = Guid.NewGuid(),
               Name = "Sample Board",
               IsTeamBoard = false,
               Tag = "Template",
           };

           await dbContext.Boards.AddAsync(board);
           await dbContext.SaveChangesAsync();

           var cardLists = await GetDefaultCardLists(dbContext, timeRange1, board.Id);
           foreach (var cardList in cardLists)
           {
               cardList.BoardId = board.Id;
               await dbContext.CardLists.AddAsync(cardList);
           }
       
           await dbContext.SaveChangesAsync();
       }
   }

   private static async Task InitializeBadgesAsync(TasklyDbContext dbContext)
   {
       if (!dbContext.Badges.Any())
       {
           BadgeEntity[] badges =
           [
               new BadgeEntity()
               {
                   Id = Guid.NewGuid(),
                     Name = Constants.BegginerLevel,
                     Icon = "beginner",
                     RequiredTasksToReceiveBadge = 5,
                     Level = 1
               },
               new BadgeEntity()
               {
                   Id = Guid.NewGuid(),
                   Name = Constants.AdvancedLevel,
                   Icon = "advanced",
                   RequiredTasksToReceiveBadge = 15,
                   Level = 2
               },
               new BadgeEntity()
               {
                   Id = Guid.NewGuid(),
                   Name = Constants.MasteryLevel,
                   Icon = "mastery",
                   RequiredTasksToReceiveBadge = 50,
                   Level = 3
               },
           ];
           
           await dbContext.AddRangeAsync(badges);
           await dbContext.SaveChangesAsync();
       }
   }

    private static async Task InitializeAchievementsAsync(TasklyDbContext dbContext)
    {
        if (!dbContext.Achievements.Any())
        {
            AchievementEntity [] achievements = [
                new AchievementEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = Constants.Achievement_FirstHeights,
                    Description = "Complete 10 tasks.",
                    Reward = 1,
                    Icon = "first_heights",
                    PercentageOfCompletion = 0
                },
                new AchievementEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = Constants.Achievement_TirelessWorker,
                    Description = "Complete 30 tasks.",
                    Reward = 2,
                    Icon = "tireless",
                    PercentageOfCompletion = 0
                },
                new AchievementEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = Constants.Achievement_MasterOfCards,
                    Description = "Complete 50 tasks.",
                    Reward = 3,
                    Icon = "mastery",
                    PercentageOfCompletion = 0
                }
            ];

            await dbContext.AddRangeAsync(achievements);
            await dbContext.SaveChangesAsync();
        }
    }
   private static async Task<List<CardListEntity>> GetDefaultCardLists(TasklyDbContext dbContext, TimeRangeEntity timeRange1, Guid boardId)
   {
      var cardLists = new List<CardListEntity>
      {
        new CardListEntity
        {
            Id = Guid.NewGuid(),
            BoardId = boardId,
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
            BoardId = boardId,
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
            BoardId = boardId,
            Title = Constants.Done,
            Cards = new List<CardEntity>
            {
                new CardEntity { Id = Guid.NewGuid(), Description = "Signed up for Taskly! 👏", Status = "Done" }
            }
        }
    };

      return cardLists;
   }
   
   private static async Task InitializeRolesAndAdminAsync(this IApplicationBuilder app, TasklyDbContext dbContext)
   {
       using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
       var service = scope.ServiceProvider;

       var userManager = service.GetRequiredService<UserManager<UserEntity>>();
       var roleManager = service.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

       if (!await roleManager.RoleExistsAsync(Constants.AdminRole))
       {
           await roleManager.CreateAsync(new IdentityRole<Guid> { Name = Constants.AdminRole });
       }
       if (!await roleManager.RoleExistsAsync(Constants.UserRole))
       {
           await roleManager.CreateAsync(new IdentityRole<Guid> { Name = Constants.UserRole });
       }

       const string adminEmail = "tasklytodolist@gmail.com";
       var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

       if (existingAdmin == null)
       {
           var adminUser = new UserEntity
           {
               Email = adminEmail,
               EmailConfirmed = true,
               UserName = adminEmail
           };

           var adminResult = await userManager.CreateAsync(adminUser, "gempLU419589");

           if (adminResult.Succeeded)
           {
               await userManager.AddToRoleAsync(adminUser, Constants.AdminRole);
           }
           else
           {
               var errors = string.Join(", ", adminResult.Errors.Select(e => e.Description));
               throw new Exception($"Failed to create admin user: {errors}");
           }
       }
   }

   // private static async Task<List<UserEntity>> GetDefaultUsers(TasklyDbContext dbContext)
   // {
   //     var users = new List<UserEntity>
   //     {
   //          new UserEntity
   //          {
   //              Id = Guid.NewGuid(),
   //              UserName = "andrii",
   //              Email = "andrii@gmail.com",
   //              AvatarId = Guid.Parse("44cbb6cc-15ae-4d6c-b02e-c889374c9086")
   //          },
   //          new UserEntity
   //          {
   //              Id = Guid.NewGuid(),
   //              UserName = "ivan",
   //              Email = "ivan@gmail.com",
   //              AvatarId = Guid.Parse("8d529912-d8d8-41bc-9326-2424e746d461")
   //          },
   //          new UserEntity
   //          {
   //              Id = Guid.NewGuid(),
   //              UserName = "vova",
   //              Email = "vova@gmail.com",
   //              AvatarId = Guid.Parse("9ef340f4-725b-443a-a022-66c2e4114d2a")
   //          },
   //          new UserEntity
   //          {
   //              Id = Guid.NewGuid(),
   //              UserName = "oleg",
   //              Email = "oleg@gmail.com",
   //              AvatarId = Guid.Parse("a69851d4-fcca-4f10-939b-c9b5a4372671")
   //          }
   //     };
   //     return users;
   // }
}
