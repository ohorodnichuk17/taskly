using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskly_Domain.Entities;

namespace Taskly_Infrastructure.Common.Persistence.FluentConfig;

public class FluentCardListConfig : IEntityTypeConfiguration<CardListEntity>
{
    public void Configure(EntityTypeBuilder<CardListEntity> builder)
    {
        builder.HasKey(cl => cl.Id);

        builder.HasMany(cl => cl.Cards)
            .WithOne(c => c.CardList)
            .HasForeignKey(c => c.CardListId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cl => cl.Board)
            .WithMany(b => b.CardLists)
            .HasForeignKey(cl => cl.BoardId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}