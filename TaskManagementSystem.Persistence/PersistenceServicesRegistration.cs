using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagementSystem.Persistence.Repositories;
using TaskManagementSystem.Application.Contracts.Persistence;
namespace TaskManagementSystem.Persistence;

public static class PersistenceServicesRegistration
{

    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<TaskManagementSystemDbContext>(opt =>
           opt.UseNpgsql(configuration.GetConnectionString("ChoreConnectionString")));
        services.AddScoped<IUnitOfWorks, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IChoreRepository, ChoreRepository>();
        services.AddScoped<ICheckListRepository, CheckListRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
