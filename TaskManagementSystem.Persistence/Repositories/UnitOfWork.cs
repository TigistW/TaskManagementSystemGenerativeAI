using TaskManagementSystem.Application.Contracts.Persistence;

namespace TaskManagementSystem.Persistence.Repositories;

public class UnitOfWork :IUnitOfWorks
{
    private readonly TaskManagementSystemDbContext _context;
    private IChoreRepository _choreRepository;
    private ICheckListRepository _checkListRepository;
    private IUserRepository _userRepository;
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

    public ICheckListRepository CheckListRepository {
        get
        {
            if (_checkListRepository == null){
                _checkListRepository = new CheckListRepository(_context);
            }
            return _checkListRepository;
        }
    }

    public IUserRepository UserRepository
    {
        get
        {
            if (_userRepository == null)
            {
                _userRepository = new UserRepository(_context);
            }
            return _userRepository;
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
