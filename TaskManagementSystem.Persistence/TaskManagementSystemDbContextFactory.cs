using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TaskManagementSystem.Persistence;

public class TaskManagementSystemDbContextFactory:  IDesignTimeDbContextFactory<TaskManagementSystemDbContext>
    {
        public TaskManagementSystemDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory() + "../../TaskManagementSystem.API")
                 .AddJsonFile("appsettings.json")
                 .Build();

            var builder = new DbContextOptionsBuilder<TaskManagementSystemDbContext>();
            var connectionString = configuration.GetConnectionString("ChoreConnectionString");

            builder.UseNpgsql(connectionString);

            return new TaskManagementSystemDbContext(builder.Options);
        }
}
