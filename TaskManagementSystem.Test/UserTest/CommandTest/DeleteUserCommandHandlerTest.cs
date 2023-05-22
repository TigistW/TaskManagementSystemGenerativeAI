
using AutoMapper;
using Moq;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.Users.CQRS.Commands;
using TaskManagementSystem.Application.Features.Users.CQRS.Handlers;
using TaskManagementSystem.Application.Features.Users.DTO;
using TaskManagementSystem.Application.Mappings;
using TaskManagementSystem.Application.Responses;
using TaskManagementSystem.Test.MockRepositories;

namespace TaskManagementSystem.Test.UserTest.CommandTest;

    public class Delete_UserCommandHandlerTests
    {
        private IMapper _mapper;
        private Mock<IUnitOfWorks> _mockUnitOfWork;
        private DeleteUserCommandHandler _handler;

        public Delete_UserCommandHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            _mapper = new MapperConfiguration(c =>
            {
                c.AddProfile<Mappings>();
            }).CreateMapper();

            _handler = new DeleteUserCommandHandler(_mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task DeleteUserValid()
        {
            // Arrange
            var user = new Domain.User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                Password = "password"
            };

            var deleteDto = new DeleteUserDto{
                Id = 1
            };

            _mockUnitOfWork.Setup(x => x.UserRepository.Get(user.Id)).ReturnsAsync(user);

            // Act
            await _handler.Handle(new DeleteUserCommand() { deleteUserDto = deleteDto}, CancellationToken.None);

            // Assert
            _mockUnitOfWork.Verify(x => x.UserRepository.Delete(user), Times.Once);
            _mockUnitOfWork.Verify(x => x.Save(), Times.Once);
        }

        [Fact]
        public async Task DeleteUserInvalid()
        {
        var deleteDto = new DeleteUserDto
        {
            Id = 999
        };
            // Arrange
            // _mockUnitOfWork.Setup(x => x.UserRepository.Get(999)).ReturnsAsync((Domain.User)null);

            // var result = new BaseCommandResponse<string>();
            // result.Value = null;
            // result.Message = "Failed Deletion";
            // result.Success = false;
            var result = await _handler.Handle(new DeleteUserCommand() { deleteUserDto = deleteDto }, CancellationToken.None);
            // Assert
            Assert.False(result.Success);
               
        }
    }
