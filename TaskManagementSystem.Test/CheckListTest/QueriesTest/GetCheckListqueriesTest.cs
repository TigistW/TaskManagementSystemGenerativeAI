

using AutoMapper;
using Shouldly;
using Moq;
using Xunit;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Test.MockRepositories;
using TaskManagementSystem.Application.Mappings;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Handlers;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Queries;

namespace TaskManagementSystem.Test.CheckListTest.QueriesTest;

public class GetCheckListqueriesTest

{
    private IMapper _mapper { get; set; }
    private Mock<IUnitOfWorks> _mockUnitOfWork { get; set; }
    private GetCheckListQueriesHandler _handler { get; set; }


    public GetCheckListqueriesTest()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

        _mapper = new MapperConfiguration(c =>
        {
            c.AddProfile<Mappings>();
        }).CreateMapper();

        _handler = new GetCheckListQueriesHandler(_mockUnitOfWork.Object, _mapper);
    }


    [Fact]
    public async Task GetCheckListValid()
    {

        var result = await _handler.Handle(new GetCheckListQueries(), CancellationToken.None);
        result.Value.Count.ShouldNotBe(1);
    }

}

