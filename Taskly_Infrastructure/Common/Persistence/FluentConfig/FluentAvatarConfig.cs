using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskly_Domain.Entities;

namespace Taskly_Infrastructure.Common.Persistence.FluentConfig;

public class FluentAvatarConfig : IEntityTypeConfiguration<AvatarEntity>
{
    public void Configure(EntityTypeBuilder<AvatarEntity> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasMany(a => a.Users)
            .WithOne(u => u.Avatar)
            .HasForeignKey(u => u.AvatarId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}