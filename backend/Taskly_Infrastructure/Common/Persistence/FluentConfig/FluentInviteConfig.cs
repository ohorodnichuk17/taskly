using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskly_Domain.Entities;

public class FluentInviteConfig : IEntityTypeConfiguration<InviteEntity>
{
    public void Configure(EntityTypeBuilder<InviteEntity> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();

        builder.HasOne(i => i.InvitedByUser)
            .WithMany(u => u.SentInvites) 
            .HasForeignKey(i => i.InvitedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.RegisteredUser)
            .WithMany(u => u.ReceivedInvites) 
            .HasForeignKey(i => i.RegisteredUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}