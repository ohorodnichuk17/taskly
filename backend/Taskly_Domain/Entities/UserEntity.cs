using System.Collections;
using Microsoft.AspNetCore.Identity;

namespace Taskly_Domain.Entities;

public class UserEntity : IdentityUser<Guid>
{
    public Guid? AvatarId { get; set; } 
    public AvatarEntity? Avatar { get; set; }
    public double? SolBalance { get; set; } = 0;
    public ICollection<BoardEntity> Boards { get; set; } = new List<BoardEntity>();
    public ICollection<TableEntity> ToDoTables { get; set; } = new List<TableEntity>();
    public ICollection<TableItemEntity> ToDoTableItems { get; set; } = new List<TableItemEntity>();
    public ICollection<FeedbackEntity> Feedbacks { get; set; } = new List<FeedbackEntity>();
    public ICollection<AchievementEntity> Achievements { get; set; } = new List<AchievementEntity>();
    public string? PublicKey { get; set; }
    public ICollection<ChallengeEntity> Challenges { get; set; } = new List<ChallengeEntity>();
    public ICollection<UserBadgeEntity> Badges { get; set; } = new List<UserBadgeEntity>();
    public UserLevelEntity? UserLevel { get; set; }
    
    public string? ReferralCode { get; set; }
    
    public ICollection<InviteEntity> SentInvites { get; set; } = new List<InviteEntity>();
    public ICollection<InviteEntity> ReceivedInvites { get; set; } = new List<InviteEntity>();
}
