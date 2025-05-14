using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskly_Domain.Entities;

namespace Taskly_Infrastructure.Common.Persistence.FluentConfig;

public class FluentBoardConfig : IEntityTypeConfiguration<BoardEntity>
{
    public void Configure(EntityTypeBuilder<BoardEntity> builder)
    {
        builder.HasKey(b => b.Id);

        builder.HasOne(b => b.BoardTemplate)
            .WithMany(bt => bt.Boards)
            .HasForeignKey(bt => bt.BoardTemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.CardLists)
            .WithOne(cl => cl.Board)
            .HasForeignKey(cl => cl.BoardId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}