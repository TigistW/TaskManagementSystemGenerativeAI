namespace TaskManagementSystem.Application.Contracts.Persistence;

public interface IUnitOfWorks : IDisposable
{
    IChoreRepository ChoreRepository { get; }
    ICheckListRepository CheckListRepository { get; }
    Task<int> Save();
}