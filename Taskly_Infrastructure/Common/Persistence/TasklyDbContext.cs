using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence.FluentConfig;

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
    public DbSet<TimeRangeEntity> TimeRanges { get; set; }
    
    public TasklyDbContext() : base()
    {
    }

    public TasklyDbContext(DbContextOptions<TasklyDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new FluentUserConfig());
        modelBuilder.ApplyConfiguration(new FluentAvatarConfig());
        modelBuilder.ApplyConfiguration(new FluentBoardConfig());
        modelBuilder.ApplyConfiguration(new FluentCardConfig());
        modelBuilder.ApplyConfiguration(new FluentCardListConfig());
        modelBuilder.ApplyConfiguration(new FluentCommentConfig());
        modelBuilder.ApplyConfiguration(new FluentTimeRangeConfig());
        modelBuilder.ApplyConfiguration(new FluentToDoItemConfig());
        modelBuilder.ApplyConfiguration(new FluentToDoTableConfig());
        modelBuilder.ApplyConfiguration(new FluentVerificationEmailConfig());
    }
}