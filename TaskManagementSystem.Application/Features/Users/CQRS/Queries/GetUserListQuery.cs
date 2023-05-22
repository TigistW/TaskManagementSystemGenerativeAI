using MediatR;
using TaskManagementSystem.Application.Features.Users.DTO;
using TaskManagementSystem.Application.Responses;

namespace TaskManagementSystem.Application.Features.Users.CQRS.Queries;

public class GetUserListQuery : IRequest <BaseCommandResponse<List<GetUserListDto>>>
{
}
