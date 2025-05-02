using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskly_Domain.Entities;

namespace Taskly_Infrastructure.Common.Persistence.FluentConfig;

public class FluentFeedbackConfig : IEntityTypeConfiguration<FeedbackEntity>
{
    public void Configure(EntityTypeBuilder<FeedbackEntity> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Review)
            .IsRequired()
            .HasMaxLength(500); 

        builder.Property(f => f.Rating)
            .IsRequired();

        builder.HasOne(td => td.TimeRange)
            .WithOne()
            .HasForeignKey<FeedbackEntity>(td => td.Id);
    }
}