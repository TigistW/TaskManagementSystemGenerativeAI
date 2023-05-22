using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly TaskManagementSystemDbContext _dbContext;
    public UserRepository(TaskManagementSystemDbContext dbContex) : base(dbContex)
    {

        _dbContext = dbContex;
    }
}
