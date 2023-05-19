using Moq;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Test.MockRepositories;

public static class MockChoreRepository
{
    public static Mock<IChoreRepository> GetChoreRepository()
    {
        var chores = new List<Domain.Chore>
        {
           new Chore
                {
                    Id = 1,
                    Title = "Task 1",
                    Description = "Task 1 Description",
                    StartDate = new DateTime(2023, 5, 1),
                    EndDate = new DateTime(2023, 5, 1),
                    Status = 0
                },
                new Chore
                {
                    Id = 2,
                    Title = "Task 2",
                    Description = "Task 2 Description",
                    StartDate = new DateTime(2023, 5, 2),
                    EndDate = new DateTime(2023, 5, 1),
                    Status = 0
                }
        };

        var mockRepository = new Mock<IChoreRepository>();

        mockRepository.Setup(r => r.GetAll()).ReturnsAsync(chores);

        mockRepository.Setup(r => r.Add(It.IsAny<Domain.Chore>())).ReturnsAsync((Domain.Chore chore) =>
        {
            chore.Id = chores.Count() + 1;
            chores.Add(chore);
            return chore;
        });

        mockRepository.Setup(r => r.Update(It.IsAny<Domain.Chore>())).Callback((Domain.Chore chore) =>
        {
            var newChores = chores.Where((r) => r.Id != chore.Id);
            chores = newChores.ToList();
            chores.Add(chore);
        });

        mockRepository.Setup(r => r.Delete(It.IsAny<Domain.Chore>())).Callback((Domain.Chore chore) =>
        {
            if (chores.Exists(b => b.Id == chore.Id))
                chores.Remove(chores.Find(b => b.Id == chore.Id)!);
        });

        mockRepository.Setup(r => r.Exists(It.IsAny<int>())).ReturnsAsync((int id) =>
        {
            var chore = chores.FirstOrDefault((r) => r.Id == id);
            return chore != null;
        });

        mockRepository.Setup(r => r.Get(It.IsAny<int>()))!.ReturnsAsync((int id) =>
        {
            return chores.FirstOrDefault((r) => r.Id == id);
        });

        return mockRepository;
    }
}