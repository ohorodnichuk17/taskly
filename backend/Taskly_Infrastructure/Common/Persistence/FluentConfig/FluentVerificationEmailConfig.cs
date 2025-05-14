using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskly_Domain.Entities;

namespace Taskly_Infrastructure.Common.Persistence.FluentConfig;

public class FluentVerificationEmailConfig : IEntityTypeConfiguration<VerificationEmailEntity>
{
    public void Configure(EntityTypeBuilder<VerificationEmailEntity> builder)
    {
        builder.HasKey(v => v.Id);
    }
}