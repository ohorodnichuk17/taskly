using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskly_Domain.Entities;

namespace Taskly_Infrastructure.Common.Persistence.FluentConfig;

public class FluentCommentConfig : IEntityTypeConfiguration<CardCommentEntity>
{
    public void Configure(EntityTypeBuilder<CardCommentEntity> builder)
    {
        builder.HasKey(c => c.Id);
    }
}