namespace TaskManagementSystem.Application.Contracts.Persistence;

public interface IUnitOfWorks : IDisposable
{
    IChoreRepository ChoreRepository { get; }
    Task<int> Save();
}