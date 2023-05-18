


using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Domain;
using TaskManagementSystem.Domain.Common;

namespace TaskManagementSystem.Persistence;

public class TaskManagementSystemDbContext : DbContext
{

        public DbSet<Chore> chores { get; set; }

        public TaskManagementSystemDbContext(DbContextOptions<TaskManagementSystemDbContext> options)
           : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagementSystemDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }


    }