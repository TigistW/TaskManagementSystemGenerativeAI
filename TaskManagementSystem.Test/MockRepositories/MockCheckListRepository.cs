


using Moq;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Domain;
namespace TaskManagementSystem.Test.MockRepositories;

public static class MockCheckListRepository
{
    public static Mock<ICheckListRepository> GetCheckListRepository()
    {
        var checkLists = new List<Domain.CheckList>
        {
           new CheckList
                {
                    id = 1,
                    Title = "Task 1",
                    Description = "Task 1 Description",
                    Status = 0
                },
                new CheckList
                {
                    id = 2,
                    Title = "Task 2",
                    Description = "Task 2 Description",
                    Status = 0
                }
        };

        var mockRepository = new Mock<ICheckListRepository>();

        mockRepository.Setup(r => r.GetAll()).ReturnsAsync(checkLists);

        mockRepository.Setup(r => r.Add(It.IsAny<Domain.CheckList>())).ReturnsAsync((Domain.CheckList checkList) =>
        {
            checkList.id = checkLists.Count() + 1;
            checkLists.Add(checkList);
            return checkList;
        });

        mockRepository.Setup(r => r.Update(It.IsAny<Domain.CheckList>())).Callback((Domain.CheckList checkList) =>
        {
            var newChores = checkLists.Where((r) => r.id != checkList.id);
            checkLists = newChores.ToList();
            checkLists.Add(checkList);
        });

        mockRepository.Setup(r => r.Delete(It.IsAny<Domain.CheckList>())).Callback((Domain.CheckList checkList) =>
        {
            if (checkLists.Exists(b => b.id == checkList.id))
                checkLists.Remove(checkLists.Find(b => b.id == checkList.id)!);
        });

        mockRepository.Setup(r => r.Exists(It.IsAny<int>())).ReturnsAsync((int id) =>
        {
            var checkList = checkLists.FirstOrDefault((r) => r.id == id);
            return checkList != null;
        });

        mockRepository.Setup(r => r.Get(It.IsAny<int>()))!.ReturnsAsync((int id) =>
        {
            return checkLists.FirstOrDefault((r) => r.id == id);
        });

        return mockRepository;
    }
}