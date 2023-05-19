using Moq;
using TaskManagementSystem.Application.Contracts.Persistence;

namespace TaskManagementSystem.Test.MockRepositories;

public class MockUnitOfWork
{
    public static Mock<IUnitOfWorks> GetUnitOfWork()
    {
        var mockUnitOfWorks = new Mock<IUnitOfWorks>();

        var mockChoreRepository = MockChoreRepository.GetChoreRepository();
        mockUnitOfWorks.Setup(r => r.ChoreRepository).Returns(mockChoreRepository.Object);

        var mockCheckListRepository = MockCheckListRepository.GetCheckListRepository();
        mockUnitOfWorks.Setup(r => r.CheckListRepository).Returns(mockCheckListRepository.Object);
        
        mockUnitOfWorks.Setup(r => r.Save()).ReturnsAsync(1);
        return mockUnitOfWorks;
    }
}
