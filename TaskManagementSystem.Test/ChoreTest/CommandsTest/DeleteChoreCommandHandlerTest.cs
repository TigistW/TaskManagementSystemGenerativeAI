
using AutoMapper;
using Shouldly;
using Moq;
using Xunit;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.UserChore.CQRS.Handlers;
using TaskManagementSystem.Test.MockRepositories;
using TaskManagementSystem.Application.Mappings;
using TaskManagementSystem.Application.Features.UserChore.DTO;
using TaskManagementSystem.Application.Features.UserChore.CQRS.Commands;

namespace TaskManagementSystem.Test.ChoreTest.CommandsTest;

public class DeleteChoreCommandHandlerTest

{
    private IMapper _mapper { get; set; }
    private Mock<IUnitOfWorks> _mockUnitOfWork { get; set; }
    private DeleteChoreCommandHandler _handler { get; set; }


    public DeleteChoreCommandHandlerTest()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

        _mapper = new MapperConfiguration(c =>
        {
            c.AddProfile<Mappings>();
        }).CreateMapper();

        _handler = new DeleteChoreCommandHandler(_mockUnitOfWork.Object, _mapper);
    }


    [Fact]
    public async Task CreateChoreValid()
    {

        DeleteChoreDto deleteChoreDto = new()
        {
            Id = 1
        };

        var result = await _handler.Handle(new DeleteChoreCommand() { deleteChoreDto = deleteChoreDto }, CancellationToken.None);

        (await _mockUnitOfWork.Object.ChoreRepository.GetAll()).Count.ShouldBe(1);
    }

    [Fact]
    public async Task DeleteChoreInvalid()
    {

        DeleteChoreDto deleteChoreDto = new() { Id = 100 };

        var result = await _handler.Handle(new DeleteChoreCommand() { deleteChoreDto = deleteChoreDto }, CancellationToken.None);

        (await _mockUnitOfWork.Object.ChoreRepository.GetAll()).Count.ShouldBe(2);
    }

}

