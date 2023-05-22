using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TaskManagementSystem.Identity;

public class TaskManagementSystemIdentityDbContextFactory : IDesignTimeDbContextFactory<TaskManagementSystemIdentityDbContext>
{
    public TaskManagementSystemIdentityDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory() + "../../TaskManagementSystem.API")
             .AddJsonFile("appsettings.json")
             .Build();

        var builder = new DbContextOptionsBuilder<TaskManagementSystemIdentityDbContext>();
        var connectionString = configuration.GetConnectionString("ChoreIdentityConnectionString");

        builder.UseNpgsql(connectionString);

        return new TaskManagementSystemIdentityDbContext(builder.Options);
    }
}
