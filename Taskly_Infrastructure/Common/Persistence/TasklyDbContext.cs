using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Taskly_Domain.Entities;

namespace Taskly_Infrastructure.Common.Persistence;

public class TasklyDbContext : IdentityDbContext<User,IdentityRole<Guid>,Guid>
{
    public DbSet<VerificationEmail> EmailVerifications { get; set; }
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