using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskly_Domain.Entities;

namespace Taskly_Infrastructure.Common.Persistence.FluentConfig;

public class FluentToDoTableConfig : IEntityTypeConfiguration<ToDoTableEntity>
{
    public void Configure(EntityTypeBuilder<ToDoTableEntity> builder)
    {
        builder.HasKey(tt => tt.Id);

        builder.HasMany(tt => tt.ToDoItems)
            .WithOne(td => td.ToDoTable)
            .OnDelete(DeleteBehavior.Cascade);
    }
}