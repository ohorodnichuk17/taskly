using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskly_Domain.Entities;

namespace Taskly_Infrastructure.Common.Persistence.FluentConfig;

public class FluentItemConfig : IEntityTypeConfiguration<TableItemEntity>
{
    public void Configure(EntityTypeBuilder<TableItemEntity> builder)
    {
        builder.HasKey(td => td.Id);

        builder.HasOne(td => td.Table)
            .WithMany(tt => tt.ToDoItems)
            .HasForeignKey(td => td.ToDoTableId);

        builder.HasOne(td => td.TimeRange)
            .WithOne()
            .HasForeignKey<TableItemEntity>(td => td.Id);
    }
}