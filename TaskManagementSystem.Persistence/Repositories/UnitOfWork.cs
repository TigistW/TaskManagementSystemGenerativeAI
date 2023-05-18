using TaskManagementSystem.Application.Contracts.Persistence;

namespace TaskManagementSystem.Persistence.Repositories;

public class UnitOfWork :IUnitOfWorks
{
    private readonly TaskManagementSystemDbContext _context;
    private IChoreRepository _choreRepository;
    public UnitOfWork(TaskManagementSystemDbContext context)
    {
        _context = context;
        
    }
    public IChoreRepository ChoreRepository{
        get
        {
            if (_choreRepository == null)
                _choreRepository = new ChoreRepository(_context);
            return _choreRepository;
        }

    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
    public async Task<int> Save()
    {
        return await _context.SaveChangesAsync();
    }

}
