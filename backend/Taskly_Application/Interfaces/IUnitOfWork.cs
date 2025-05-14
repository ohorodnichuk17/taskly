using Taskly_Application.Interfaces.IRepository;

namespace Taskly_Application.Interfaces;

public interface IUnitOfWork 
{
    Task SaveChangesAsync(string errorMessage);
    IAuthenticationRepository Authentication { get; }
    IBoardRepository Board { get; }
    IAvatarRepository Avatar { get; }
    ITableRepository Table { get; }
    ITableItemsRepository TableItems { get; }
    IBoardTemplateRepository BoardTemplates { get; }
    ICardRepository Cards { get; }
    IFeedbackRepository Feedbacks { get; }
    IAchievementRepository Achievements { get; }
    IChallengeRepository Challenges { get; }
    IInviteRepository Invites { get; }
    IBadgeRepository Badges { get; }
    IUserLevelRepository UserLevels { get; }
    IUserBadgeRepository UserBadges { get; }
}