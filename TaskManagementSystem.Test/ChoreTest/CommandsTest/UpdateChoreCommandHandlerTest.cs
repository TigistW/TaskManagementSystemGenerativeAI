
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

public class UpdateChoreCommandHandlerTest

{
    private IMapper _mapper { get; set; }
    private Mock<IUnitOfWorks> _mockUnitOfWork { get; set; }
    private UpdateChoreCommandHandler _handler { get; set; }


    public UpdateChoreCommandHandlerTest()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

        _mapper = new MapperConfiguration(c =>
        {
            c.AddProfile<Mappings>();
        }).CreateMapper();

        _handler = new UpdateChoreCommandHandler(_mockUnitOfWork.Object, _mapper);
    }


    [Fact]
    public async Task UpdateChoreValid()
    {

        UpdateChoreDto updateChoreDto = new()
        {
            Title = "Title of the updated chore",
            Description = "Body of the updated chore",
            Status = 0,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,

        };

        var result = await _handler.Handle(new UpdateChoreCommand() { updateChoreDto = updateChoreDto }, CancellationToken.None);

        result.Value.Description.ShouldBeEquivalentTo(updateChoreDto.Description);
        result.Value.Title.ShouldBeEquivalentTo(updateChoreDto.Title);

        (await _mockUnitOfWork.Object.ChoreRepository.GetAll()).Count.ShouldBe(2);
    }



    // [Fact]
    // public async Task UpdateChoreInValid()
    // {

    //     UpdateChoreDto updateChoreDto = new()
    //     {
    //         Description = "Body of the new chore",
    //         StartDate = DateTime.Now,
    //         EndDate = DateTime.Now,

    //     };

    //     var result = await _handler.Handle(new UpdateChoreCommand() { updateChoreDto = updateChoreDto }, CancellationToken.None);

    //     // result.Value.ShouldBe(null);
    //     (await _mockUnitOfWork.Object.ChoreRepository.GetAll()).Count.ShouldBe(2);
    // }

}

