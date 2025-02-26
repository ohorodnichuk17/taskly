using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskly_Domain.Entities;

namespace Taskly_Infrastructure.Common.Persistence.FluentConfig;

public class FluentToDoItemConfig : IEntityTypeConfiguration<ToDoItemEntity>
{
    public void Configure(EntityTypeBuilder<ToDoItemEntity> builder)
    {
        builder.HasKey(td => td.Id);

        builder.HasOne(td => td.ToDoTable)
            .WithMany(tt => tt.ToDoItems)
            .HasForeignKey(td => td.ToDoTableId);

        builder.HasOne(td => td.TimeRange)
            .WithOne()
            .HasForeignKey<ToDoItemEntity>(td => td.Id);
    }
}