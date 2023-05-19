
using AutoMapper;
using Shouldly;
using Moq;
using Xunit;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Test.MockRepositories;
using TaskManagementSystem.Application.Mappings;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Handlers;
using TaskManagementSystem.Application.Features.CheckList.DTO;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Commands;

namespace TaskManagementSystem.Test.CheckListTest.CommandsTest;

public class UpdateCheckListCommandHandlerTest

{
    private IMapper _mapper { get; set; }
    private Mock<IUnitOfWorks> _mockUnitOfWork { get; set; }
    private UpdateCheckListCommandHandler _handler { get; set; }


    public UpdateCheckListCommandHandlerTest()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

        _mapper = new MapperConfiguration(c =>
        {
            c.AddProfile<Mappings>();
        }).CreateMapper();

        _handler = new UpdateCheckListCommandHandler(_mockUnitOfWork.Object, _mapper);
    }


    [Fact]
    public async Task UpdateChoreValid()
    {

        UpdateCheckListDto updateCheckListDto = new()
        {
            Title = "Title of the updated checkList",
            Description = "Body of the updated checkList",
            Status = 0
        };

        var result = await _handler.Handle(new UpdateCheckListCommand() { updateCheckListDto = updateCheckListDto }, CancellationToken.None);

        result.Value.Description.ShouldBeEquivalentTo(updateCheckListDto.Description);
        result.Value.Title.ShouldBeEquivalentTo(updateCheckListDto.Title);

        (await _mockUnitOfWork.Object.CheckListRepository.GetAll()).Count.ShouldBe(2);
    }



}

