using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Taskly_Domain.Entities;

namespace Taskly_Infrastructure.Common.Persistence;

public class TasklyDbContext : IdentityDbContext<UserEntity,IdentityRole<Guid>,Guid>
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<AvatarEntity> Avatars { get; set; }
    public DbSet<BoardEntity> Boards { get; set; }
    public DbSet<BoardTemplateEntity> BoardTemplates { get; set; }
    public DbSet<CardEntity> Cards { get; set; }
    public DbSet<CardListEntity> CardLists { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<ToDoTableEntity> ToDoTables { get; set; }
    public DbSet<ToDoItemEntity> ToDoItems { get; set; }
    public DbSet<VerificationEmailEntity> EmailVerifications { get; set; }
    
    public TasklyDbContext() : base()
    {
    }

    public TasklyDbContext(DbContextOptions<TasklyDbContext> options) : base(options)
    {
    }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // UserEntity
    modelBuilder.Entity<UserEntity>(entity =>
    {
        entity.HasOne(u => u.Avatar)
              .WithOne()
              .HasForeignKey<UserEntity>(u => u.Id);

        entity.HasMany(u => u.Boards)
              .WithMany();

        entity.HasMany(u => u.ToDoTables)
              .WithMany();
    });

    // AvatarEntity
    modelBuilder.Entity<AvatarEntity>(entity =>
    {
        entity.HasKey(a => a.Id);
    });

    // BoardEntity
    modelBuilder.Entity<BoardEntity>(entity =>
    {
        entity.HasKey(b => b.Id);

        entity.HasMany(b => b.BoardTemplates)
              .WithOne()
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(b => b.CardLists)
              .WithOne()
              .OnDelete(DeleteBehavior.Cascade);
    });

    // BoardTemplateEntity
    modelBuilder.Entity<BoardTemplateEntity>(entity =>
    {
        entity.HasKey(bt => bt.Id);
    });

    // CardEntity
    modelBuilder.Entity<CardEntity>(entity =>
    {
        entity.HasKey(c => c.Id);

        entity.HasMany(c => c.Comments)
              .WithOne()
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(c => c.TimeRangeEntity)
              .WithOne()
              .HasForeignKey<CardEntity>(c => c.Id);
    });

    // CardListEntity
    modelBuilder.Entity<CardListEntity>(entity =>
    {
        entity.HasKey(cl => cl.Id);

        entity.HasMany(cl => cl.Cards)
              .WithOne()
              .OnDelete(DeleteBehavior.Cascade);
    });

    // CommentEntity
    modelBuilder.Entity<CommentEntity>(entity =>
    {
        entity.HasKey(c => c.Id);
    });

    // TimeRangeEntity
    modelBuilder.Entity<TimeRangeEntity>(entity =>
    {
        entity.HasKey(tr => tr.Id);
    });

    // ToDoItemEntity
    modelBuilder.Entity<ToDoItemEntity>(entity =>
    {
        entity.HasKey(td => td.Id);

        entity.HasOne(td => td.ToDoTable)
              .WithMany(tt => tt.ToDoItems)
              .HasForeignKey(td => td.ToDoTableId);

        entity.HasOne(td => td.TimeRange)
              .WithOne()
              .HasForeignKey<ToDoItemEntity>(td => td.Id);
    });

    // ToDoTableEntity
    modelBuilder.Entity<ToDoTableEntity>(entity =>
    {
        entity.HasKey(tt => tt.Id);

        entity.HasMany(tt => tt.ToDoItems)
              .WithOne(td => td.ToDoTable)
              .OnDelete(DeleteBehavior.Cascade);
    });

    // VerificationEmailEntity
    modelBuilder.Entity<VerificationEmailEntity>(entity =>
    {
        entity.HasKey(v => v.Id);
    });
    
    // BoardTeamEntity
    modelBuilder.Entity<BoardTeamEntity>(entity =>
    {
        entity.HasKey(bt => bt.Id);

        entity.HasMany(bt => bt.Members)
              .WithMany();

        entity.HasOne(bt => bt.Board)
              .WithOne()
              .HasForeignKey<BoardTeamEntity>(bt => bt.Id);
    });
}
}