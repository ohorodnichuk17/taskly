using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskly_Domain.Entities;

namespace Taskly_Infrastructure.Common.Persistence.FluentConfig;

public class FluentTimeRangeConfig : IEntityTypeConfiguration<TimeRangeEntity>
{
    public void Configure(EntityTypeBuilder<TimeRangeEntity> builder)
    {
        builder.HasKey(tr => tr.Id);
    }
}