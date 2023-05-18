using MediatR;
using TaskManagementSystem.Application.Features.CheckList.DTO;
using TaskManagementSystem.Application.Responses;

namespace TaskManagementSystem.Application.Features.CheckList.CQRS.Commands;

public class DeleteCheckListCommand : IRequest<BaseCommandResponse<Unit>>
{
    public DeleteCheckListDto deleteCheckListDto {get; set;}
}
