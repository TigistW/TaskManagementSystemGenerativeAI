using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Domain;
using TaskManagementSystem.Persistence.Repositories;

namespace TaskManagementSystem.Persistence;

public class ChoreRepository : GenericRepository<Chore>, IChoreRepository
{
    private readonly TaskManagementSystemDbContext _dbContext;
    public ChoreRepository(TaskManagementSystemDbContext context) : base(context)
    {
        _dbContext = context;
    }
}
