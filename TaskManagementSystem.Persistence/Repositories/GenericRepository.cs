using System.Data;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Application.Contracts.Persistence;

namespace TaskManagementSystem.Persistence.Repositories;

public class GenericRepository <T>: IGenericRepository<T> where T : class
{
    private readonly TaskManagementSystemDbContext _dbContext;

    public GenericRepository(TaskManagementSystemDbContext dbContex)
    {
        _dbContext = dbContex;
    }


    public async Task<T> Add(T entity)
    {
        await _dbContext.AddAsync(entity);
        return entity;
    }

    public async Task Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);

    }

    public async Task<bool> Exists(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id) != null;
    }

    public async Task<T?> Get(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAll()
    {
        return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task Update(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        
    }
}
