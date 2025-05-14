using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskly_Domain.Entities;

namespace Taskly_Infrastructure.Common.Persistence.FluentConfig;

public class FluentTableConfig : IEntityTypeConfiguration<TableEntity>
{
    public void Configure(EntityTypeBuilder<TableEntity> builder)
    {
        builder.HasKey(tt => tt.Id);

        builder.HasMany(tt => tt.ToDoItems)
            .WithOne(td => td.Table)
            .OnDelete(DeleteBehavior.Cascade);
    }
}