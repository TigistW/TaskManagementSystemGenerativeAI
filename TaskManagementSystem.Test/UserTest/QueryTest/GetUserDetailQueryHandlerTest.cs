
using AutoMapper;

using Moq;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.Users.CQRS.Handlers;
using TaskManagementSystem.Application.Features.Users.CQRS.Queries;
using TaskManagementSystem.Application.Features.Users.DTO;
using TaskManagementSystem.Domain;
using Xunit;
namespace TaskManagementSystem.Test.UserTest.QueryTest;


public class GetUserDetailQueryHandlerTest
{
    private readonly Mock<IUnitOfWorks> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;

    public GetUserDetailQueryHandlerTest()
    {
        _mockUnitOfWork = new Mock<IUnitOfWorks>();
        _mockMapper = new Mock<IMapper>();
    }

    [Fact]
    public async void Handle_WithValidQuery_ShouldReturnUserDto()
    {
        // Arrange
        var userId = 1;
        var user = new User { Id = userId, FirstName = "John", LastName = "Doe", Email = "johndoe@example.com" };
        var expectedUserDto = new GetUserByIdDto { Id = userId, FirstName = "John", LastName = "Doe", Email = "johndoe@example.com" };
        var query = new GetUserDetailQuery { Id = userId };
        var cancellationToken = new CancellationToken();
        _mockUnitOfWork.Setup(uow => uow.UserRepository.Get(userId)).ReturnsAsync(user);
        _mockMapper.Setup(m => m.Map<GetUserByIdDto>(user)).Returns(expectedUserDto);
        var handler = new GetUserDetailQueryHandler(_mockUnitOfWork.Object, _mockMapper.Object);
        // Act
        var result = await handler.Handle(query, cancellationToken);
        
        // Assert
        Assert.Equal(expectedUserDto, result.Value);
    }
}
