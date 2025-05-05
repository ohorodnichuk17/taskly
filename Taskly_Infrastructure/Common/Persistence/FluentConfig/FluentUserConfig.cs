using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskly_Domain.Entities;

namespace Taskly_Infrastructure.Common.Persistence.FluentConfig;

public class FluentUserConfig : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasMany(u => u.Boards)
            .WithMany(b => b.Members)
            .UsingEntity<Dictionary<string, object>>(
                "UserBoard",
                j => j.HasOne<BoardEntity>().WithMany().HasForeignKey("BoardId"),
                j => j.HasOne<UserEntity>().WithMany().HasForeignKey("UserId")
            );

        builder.HasMany(u => u.ToDoTables)
            .WithMany(t => t.Members)
            .UsingEntity<Dictionary<string, object>>(
                "UserTable",
                j => j.HasOne<TableEntity>().WithMany().HasForeignKey("TableId"),
                j => j.HasOne<UserEntity>().WithMany().HasForeignKey("UserId")
            );

        builder.HasMany(u => u.ToDoTableItems)
            .WithMany(td => td.Members)
            .UsingEntity<Dictionary<string, object>>(
                "UserTableItem",
                j => j.HasOne<TableItemEntity>().WithMany().HasForeignKey("ItemId"),
                j => j.HasOne<UserEntity>().WithMany().HasForeignKey("UserId")
            );

        builder.HasOne(u => u.Avatar)
            .WithMany(a => a.Users)
            .HasForeignKey(u => u.AvatarId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasMany(u => u.Feedbacks) 
            .WithOne(f => f.User)         
            .HasForeignKey(f => f.UserId);

        builder.HasMany(u => u.Achievements)
            .WithMany(a => a.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserAchievements",
                j => j.HasOne<AchievementEntity>().WithMany().HasForeignKey("AchievementId"),
                j => j.HasOne<UserEntity>().WithMany().HasForeignKey("UserId")
            );
    }
}