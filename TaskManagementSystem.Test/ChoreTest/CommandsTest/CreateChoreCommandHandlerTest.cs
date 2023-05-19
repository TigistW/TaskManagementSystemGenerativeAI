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

public class CreateChoreCommandHandlerTest

{
    private IMapper _mapper { get; set; }
    private Mock<IUnitOfWorks> _mockUnitOfWork { get; set; }
    private CreateChoreCommandHandler _handler { get; set; }


    public CreateChoreCommandHandlerTest()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

        _mapper = new MapperConfiguration(c =>
        {
            c.AddProfile<Mappings>();
        }).CreateMapper();

        _handler = new CreateChoreCommandHandler(_mockUnitOfWork.Object, _mapper);
    }


    [Fact]
    public async Task CreateChoreValid()
    {

        CreateChoreDto createChoreDto = new()
        {
            Title = "Title of the new chore",
            Description = "Body of the new chore",
            Status = 0,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,

        };

        var result = await _handler.Handle(new CreateChoreCommand() { createChoreDto = createChoreDto }, CancellationToken.None);

        result.Value.Description.ShouldBeEquivalentTo(createChoreDto.Description);
        result.Value.Title.ShouldBeEquivalentTo(createChoreDto.Title);

        (await _mockUnitOfWork.Object.ChoreRepository.GetAll()).Count.ShouldBe(3);
    }



    // [Fact]
    // public async Task CreateChoreInValid()
    // {

    //     CreateChoreDto createChoreDto = new()
    //     {
    //         Description = "Body of the new chore",
    //         StartDate = DateTime.Now,
    //         EndDate = DateTime.Now,

    //     };

    //     var result = await _handler.Handle(new CreateChoreCommand() { createChoreDto = createChoreDto }, CancellationToken.None);

    //     // result.Value.ShouldBe(null);
    //     (await _mockUnitOfWork.Object.ChoreRepository.GetAll()).Count.ShouldBe(2);
    // }

}

