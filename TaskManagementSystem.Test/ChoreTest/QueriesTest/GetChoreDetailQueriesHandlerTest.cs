
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

public class GetChoreDetailQueriesHandlerTest

{
    private IMapper _mapper { get; set; }
    private Mock<IUnitOfWorks> _mockUnitOfWork { get; set; }
    private GetChoreDetailQueryHandler _handler { get; set; }


    public GetChoreDetailQueriesHandlerTest()
    {
        _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

        _mapper = new MapperConfiguration(c =>
        {
            c.AddProfile<Mappings>();
        }).CreateMapper();

        _handler = new GetChoreDetailQueryHandler(_mockUnitOfWork.Object, _mapper);
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

        var result = await _handler.Handle(new GetChoreDetailQueries(){Id = 1}, CancellationToken.None);
        result.ShouldNotBe(null);
    }

}

