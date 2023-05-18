using MediatR;
using TaskManagementSystem.Application.Features.UserChore.DTO;
using TaskManagementSystem.Application.Responses;

namespace TaskManagementSystem.Application.Features.UserChore.CQRS.Queries;

public class GetChoreDetailQueries :IRequest<BaseCommandResponse<ChoreDetailDto>>
{
    public int Id { get; set; }
}
