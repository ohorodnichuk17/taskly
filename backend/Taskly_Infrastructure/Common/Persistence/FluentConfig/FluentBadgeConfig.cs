using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskly_Domain.Entities;

namespace Taskly_Infrastructure.Common.Persistence.FluentConfig;

public class FluentBadgeConfig : IEntityTypeConfiguration<BadgeEntity>
{
    public void Configure(EntityTypeBuilder<BadgeEntity> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(b => b.Icon)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(b => b.RequiredTasksToReceiveBadge)
            .IsRequired();

        builder.Property(b => b.Level)
            .IsRequired();
    }
}