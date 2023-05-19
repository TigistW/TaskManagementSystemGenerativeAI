
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


public class DeleteCheckListCommandHandlerTest

{
    private IMapper _mapper { get; set; }
    private Mock<IUnitOfWorks> _mockUnitOfWork { get; set; }
    private DeleteCheckListCommandHandler _handler { get; set; }


    public DeleteCheckListCommandHandlerTest()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

        _mapper = new MapperConfiguration(c =>
        {
            c.AddProfile<Mappings>();
        }).CreateMapper();

        _handler = new DeleteCheckListCommandHandler(_mockUnitOfWork.Object, _mapper);
    }


    [Fact]
    public async Task DeleteCheckListValid()
    {

        DeleteCheckListDto deleteCheckListDto = new()
        {
           id = 1,

        };

        var result = await _handler.Handle(new DeleteCheckListCommand() { deleteCheckListDto = deleteCheckListDto }, CancellationToken.None);


        (await _mockUnitOfWork.Object.CheckListRepository.GetAll()).Count.ShouldBe(1);
    }


     [Fact]
    public async Task DeleteCheckListInvalid()
    {

        DeleteCheckListDto deleteCheckListDto = new() { id = 100 };

        var result = await _handler.Handle(new DeleteCheckListCommand() { deleteCheckListDto = deleteCheckListDto }, CancellationToken.None);

        (await _mockUnitOfWork.Object.CheckListRepository.GetAll()).Count.ShouldBe(2);
    }


}

