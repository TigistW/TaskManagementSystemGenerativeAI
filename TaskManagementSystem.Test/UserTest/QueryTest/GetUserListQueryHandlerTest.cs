
using AutoMapper;

using Moq;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.Users.CQRS.Handlers;
using TaskManagementSystem.Application.Features.Users.CQRS.Queries;
using TaskManagementSystem.Application.Features.Users.DTO;
using TaskManagementSystem.Application.Responses;
using TaskManagementSystem.Domain;
using Xunit;
namespace TaskManagementSystem.Test.UserTest.QueryTest;


public class GetUserListQueryHandlerTest
{
    private readonly Mock<IUnitOfWorks> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;

    public GetUserListQueryHandlerTest()
    {
        _mockUnitOfWork = new Mock<IUnitOfWorks>();
        _mockMapper = new Mock<IMapper>();
    }

    [Fact]
    public async Task Handle_ReturnsListOfUserDtos_WhenUsersExist()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "johndoe@mail.com" },
            new User { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "janedoe@mail.com" },
            new User { Id = 3, FirstName = "Bob", LastName = "Smith", Email = "bobsmith@mail.com" }
        };

        var userDtos = new List<GetUserListDto>
        {
            new GetUserListDto { Id = 1, FirstName = "John", LastName = "Doe", Email = "johndoe@mail.com" },
            new GetUserListDto { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "janedoe@mail.com" },
            new GetUserListDto { Id = 3, FirstName = "Bob", LastName = "Smith", Email = "bobsmith@mail.com" }
        };

        _mockUnitOfWork.Setup(u => u.UserRepository.GetAll()).ReturnsAsync(users);
        _mockMapper.Setup(m => m.Map<List<GetUserListDto>>(users)).Returns(userDtos);

        var handler = new GetUserListQueryHandler(_mockUnitOfWork.Object, _mockMapper.Object);

        // Act
        var result = await handler.Handle(new GetUserListQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BaseCommandResponse<List<GetUserListDto>>>(result);
        Assert.Equal(userDtos.Count, result.Value.Count);

        for (int i = 0; i < userDtos.Count; i++)
        {
            Assert.Equal(userDtos[i].Id, result.Value[i].Id);
            Assert.Equal(userDtos[i].FirstName, result.Value[i].FirstName);
            Assert.Equal(userDtos[i].LastName, result.Value[i].LastName);
        }
    }

    [Fact]
    public async Task Handle_ReturnsEmptyList_WhenNoUsersExist()
    {
        // Arrange
        var users = new List<User>();
        var userDtos = new List<GetUserListDto>();

        _mockUnitOfWork.Setup(u => u.UserRepository.GetAll()).ReturnsAsync(users);
        _mockMapper.Setup(m => m.Map<List<GetUserListDto>>(users)).Returns(userDtos);

        var handler = new GetUserListQueryHandler(_mockUnitOfWork.Object, _mockMapper.Object);

        // Act
        var result = await handler.Handle(new GetUserListQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BaseCommandResponse<List<GetUserListDto>>>(result);
        Assert.Empty(result.Value);
    }
}
