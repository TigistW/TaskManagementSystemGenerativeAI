
using AutoMapper;
using FluentAssertions;
using Moq;
using Shouldly;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.Users.CQRS.Commands;
using TaskManagementSystem.Application.Features.Users.CQRS.Handlers;
using TaskManagementSystem.Application.Features.Users.DTO;
using TaskManagementSystem.Application.Mappings;
using TaskManagementSystem.Application.Responses;
using TaskManagementSystem.Test.MockRepositories;
using Xunit;

namespace TaskManagementSystem.Test.UserTest.CommandTest;
public class CreateUserCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWorks> _mockUow;
    private readonly CreateUserDto _userDto;
    private readonly CreateUserDto _invalidUserDto;

    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        _mockUow = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<Mappings>();
        });

        _mapper = mapperConfig.CreateMapper();
        _handler = new CreateUserCommandHandler(_mockUow.Object, _mapper);

        _userDto = new CreateUserDto
        {
            AccountId = "1",
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@example.com",
            Password = "password"
        };

        _invalidUserDto = new CreateUserDto
        {
            AccountId = "2",
            FirstName = "",
            LastName = "Doe",
            Email = "johndoe@example.com",
            Password = "password"
        };
    }

    [Fact]
    public async Task Valid_User_Added()
    {
        var result = await _handler.Handle(new CreateUserCommand() { createUserDto = _userDto }, CancellationToken.None);

        var users = await _mockUow.Object.UserRepository.GetAll();

        result.ShouldBeOfType<BaseCommandResponse<CreateUserDto>>();

        users.Count.ShouldBe(3);
    }

    [Fact]
    public async Task InValid_User_Added()
    {

        var result = await _handler.Handle(new CreateUserCommand() { createUserDto = _invalidUserDto }, CancellationToken.None);

        var users = await _mockUow.Object.UserRepository.GetAll();

        users.Count.ShouldBe(2);

        result.ShouldBeOfType<BaseCommandResponse<CreateUserDto>>();

    }
}
