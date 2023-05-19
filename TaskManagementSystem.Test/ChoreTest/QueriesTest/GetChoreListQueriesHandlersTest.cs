

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
using TaskManagementSystem.Application.Features.UserChore.CQRS.Queries;

namespace TaskManagementSystem.Test.ChoreTest.QueriesTest;

public class GetChoreListQueriesHandlersTest

{
    private IMapper _mapper { get; set; }
    private Mock<IUnitOfWorks> _mockUnitOfWork { get; set; }
    private GetChoreListQueryHandler _handler { get; set; }


    public GetChoreListQueriesHandlersTest()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

        _mapper = new MapperConfiguration(c =>
        {
            c.AddProfile<Mappings>();
        }).CreateMapper();

        _handler = new GetChoreListQueryHandler(_mockUnitOfWork.Object, _mapper);
    }


    [Fact]
    public async Task GetChoreListValid()
    {
        var result = await _handler.Handle(new GetChoreListQueries(), CancellationToken.None);
        result.Value.Count.ShouldNotBe(1);
    }

}

