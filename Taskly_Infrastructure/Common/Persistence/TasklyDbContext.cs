using Microsoft.EntityFrameworkCore;

namespace Taskly_Infrastructure.Common.Persistence;

public class TasklyDbContext : DbContext
{
    public TasklyDbContext() : base()
    {
    }

    public TasklyDbContext(DbContextOptions<TasklyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}