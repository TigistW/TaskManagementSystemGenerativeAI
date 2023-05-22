using Moq;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Domain;


namespace TaskManagementSystem.Test.MockRepositories;


public static class MockUserRepository
{
    public static Mock<IUserRepository> GetUserRepository()
    {
        var chores = new List<Domain.User>
        {
           new User
                {
                    Id = 1,
                    FirstName = "Abenuel",
                    LastName = "Zema",
                    
                },
                new User
                {
                    Id = 2,
                    FirstName = "Eyouel",
                    LastName = "Adu",
                }
        };

        var mockRepository = new Mock<IUserRepository>();

        mockRepository.Setup(r => r.GetAll()).ReturnsAsync(chores);

        mockRepository.Setup(r => r.Add(It.IsAny<Domain.User>())).ReturnsAsync((Domain.User chore) =>
        {
            chore.Id = chores.Count() + 1;
            chores.Add(chore);
            return chore;
        });

        mockRepository.Setup(r => r.Update(It.IsAny<Domain.User>())).Callback((Domain.User chore) =>
        {
            var newChores = chores.Where((r) => r.Id != chore.Id);
            chores = newChores.ToList();
            chores.Add(chore);
        });

        mockRepository.Setup(r => r.Delete(It.IsAny<Domain.User>())).Callback((Domain.User chore) =>
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