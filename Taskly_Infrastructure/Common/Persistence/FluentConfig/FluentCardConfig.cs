using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskly_Domain.Entities;

namespace Taskly_Infrastructure.Common.Persistence.FluentConfig;

public class FluentCardConfig : IEntityTypeConfiguration<CardEntity>
{
    public void Configure(EntityTypeBuilder<CardEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasMany(c => c.Comments)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.TimeRangeEntity)
            .WithOne()
            .HasForeignKey<CardEntity>(c => c.TimeRangeEntityId)
            .IsRequired(false);

        builder.HasOne(c => c.CardList)
            .WithMany(cl => cl.Cards)
            .HasForeignKey(c => c.CardListId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}