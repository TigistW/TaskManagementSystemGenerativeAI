

using AutoMapper;
using Shouldly;
using Moq;
using Xunit;
using TaskManagementSystem.Test.MockRepositories;
using TaskManagementSystem.Application.Mappings;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Handlers;
using TaskManagementSystem.Application.Features.CheckList.DTO;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Commands;

namespace TaskManagementSystem.Test.CheckListTest.CommandsTest;

public class CreateCheckListCommandHandlerTest

{
    private IMapper _mapper { get; set; }
    private Mock<IUnitOfWorks> _mockUnitOfWork { get; set; }
    private CreateCheckListCommandHandler _handler { get; set; }


    public CreateCheckListCommandHandlerTest()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

        _mapper = new MapperConfiguration(c =>
        {
            c.AddProfile<Mappings>();
        }).CreateMapper();

        _handler = new CreateCheckListCommandHandler(_mockUnitOfWork.Object, _mapper);
    }


    [Fact]
    public async Task CreateCheckListValid()
    {

        CreateCheckListDto createCheckListDto = new()
        {
            Title = "Title of the new chore",
            Description = "Body of the new chore",
            Status = 0,

        };

        var result = await _handler.Handle(new CreateCheckListCommand() { createCheckListDto = createCheckListDto }, CancellationToken.None);

        result.Value.Description.ShouldBeEquivalentTo(createCheckListDto.Description);
        result.Value.Title.ShouldBeEquivalentTo(createCheckListDto.Title);

        (await _mockUnitOfWork.Object.CheckListRepository.GetAll()).Count.ShouldBe(3);
    }



    // [Fact]
    // public async Task CreateCheckListInValid()
    // {

    //     CreateCheckListDto createCheckListDto = new()
    //     {
    //         Description = "Body of the new chore",

    //     };

    //     var result = await _handler.Handle(new CreateCheckListCommand() { createChoreDto = createChoreDto }, CancellationToken.None);

    //     // result.Value.ShouldBe(null);
    //     (await _mockUnitOfWork.Object.ChoreRepository.GetAll()).Count.ShouldBe(2);
    // }

}

