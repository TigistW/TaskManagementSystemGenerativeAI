using MediatR;
using TaskManagementSystem.Application.Features.CheckList.DTO;
using TaskManagementSystem.Application.Responses;

namespace TaskManagementSystem.Application.Features.CheckList.CQRS.Queries;

public class GetCheckListQueries : IRequest<BaseCommandResponse<List<GetCheckListDto>>>
{
}
