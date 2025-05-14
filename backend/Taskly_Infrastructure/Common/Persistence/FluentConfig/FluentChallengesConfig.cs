using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskly_Domain.Entities;

namespace Taskly_Infrastructure.Common.Persistence.FluentConfig;

public class FluentChallengesConfig : IEntityTypeConfiguration<ChallengeEntity>
{
    public void Configure(EntityTypeBuilder<ChallengeEntity> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.RuleKey)
            .IsRequired()
            .HasMaxLength(70);
        
        builder.HasOne(td => td.TimeRange)
            .WithOne()
            .HasForeignKey<ChallengeEntity>(td => td.Id);
        
        builder.Property(c => c.Points) 
            .IsRequired()
            .HasPrecision(18, 2);
        
        builder.Property(c => c.IsBooked)
            .HasDefaultValue(false);
        
        builder.Property(c => c.IsCompleted)
            .HasDefaultValue(false);
        
        builder.Property(c => c.IsActive)
            .HasDefaultValue(true);
        
        builder.HasOne(c => c.User) 
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}