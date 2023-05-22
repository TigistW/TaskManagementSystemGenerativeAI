
using AutoMapper;
using FluentAssertions;
using Moq;
using Shouldly;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.Users.CQRS.Commands;
using TaskManagementSystem.Application.Features.Users.CQRS.Handlers;
using TaskManagementSystem.Application.Features.Users.DTO;
using TaskManagementSystem.Domain;
namespace TaskManagementSystem.Test.UserTest.CommandTest;
public class UpdateUserCommandHandlerTests
{

    private readonly UpdateUserDto _userDto;
    private readonly UpdateUserDto _invalidUserDto;

    private readonly UpdateUserCommandHandler _handler;
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWorks> _mockUnitOfWork;

    public UpdateUserCommandHandlerTests()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UpdateUserDto, User>();
        });

        _mapper = mapperConfig.CreateMapper();
        _mockUnitOfWork = new Mock<IUnitOfWorks>();
        _handler = new UpdateUserCommandHandler(_mockUnitOfWork.Object, _mapper);

    }
    [Fact]
    public async Task UpdateUserCommandHandler_WhenUserExists_ShouldUpdateUser()
    {
        // Arrange
        var existingUserId = 1;
        var existingUser = new User
        {
            Id = existingUserId,
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@example.com",
            Password = "password"
        };
        var updateDto = new UpdateUserDto
        {
            Id = existingUserId,
            FirstName = "Jane",
            LastName = "Doe",
            Email = "janedoe@example.com",
            Password = "newpassword"
        };

        var command = new UpdateUserCommand()
        {
            updateUserDto = updateDto
        };
        _mockUnitOfWork.Setup(uow => uow.UserRepository.Get(existingUserId))
            .ReturnsAsync(existingUser);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockUnitOfWork.Verify(uow => uow.UserRepository.Update(It.IsAny<User>()), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
        existingUser.FirstName.Should().Be(updateDto.FirstName);
        existingUser.LastName.Should().Be(updateDto.LastName);
        existingUser.Email.Should().Be(updateDto.Email);
        existingUser.Password.Should().Be(updateDto.Password);
}
        [Fact]
        public async Task UpdateUserCommandHandler_WhenUserDoesNotExist_ShouldThrowNotFoundException()
        {
            // Arrange
            var nonExistingUserId = 1;
            var updateDto = new UpdateUserDto
            {
                Id = nonExistingUserId,
                FirstName = "Jane",
                LastName = "Doe",
                Email = "janedoe@example.com",
                Password = "newpassword"
            };
            var command = new UpdateUserCommand()
            {
                updateUserDto = updateDto
            };
            _mockUnitOfWork.Setup(uow => uow.UserRepository.Get(nonExistingUserId))
                .ReturnsAsync(() => null);

            // Act & Assert
            await Assert.ThrowsAsync<FileNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }