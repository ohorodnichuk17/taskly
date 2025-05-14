using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskly_Domain.Entities;

namespace Taskly_Infrastructure.Common.Persistence.FluentConfig;

public class FluentUserBadgeConfig : IEntityTypeConfiguration<UserBadgeEntity>
{
    public void Configure(EntityTypeBuilder<UserBadgeEntity> builder)
    {
        builder.HasKey(x => new { x.UserId, x.BadgeId });

        builder.HasOne(x => x.User)
            .WithMany(x => x.Badges)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Badge)
            .WithMany()
            .HasForeignKey(x => x.BadgeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.DateAwarded)
            .IsRequired();
    }
}