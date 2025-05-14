using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskly_Domain.Entities;

namespace Taskly_Infrastructure.Common.Persistence.FluentConfig;

public class FluentUserLevelConfig : IEntityTypeConfiguration<UserLevelEntity>
{
    public void Configure(EntityTypeBuilder<UserLevelEntity> builder)
    {
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Level)
                .IsRequired();

            builder.Property(x => x.CompletedTasks)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithOne(x => x.UserLevel)
                .HasForeignKey<UserLevelEntity>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.UserId)
                .IsRequired();
        }
    }
}