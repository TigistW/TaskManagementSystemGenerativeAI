using MediatR;
using TaskManagementSystem.Application.Features.UserChore.DTO;
using TaskManagementSystem.Application.Responses;

namespace TaskManagementSystem.Application.Features.UserChore.CQRS.Commands;

public class DeleteChoreCommand: IRequest<BaseCommandResponse<Unit>>
{
    public DeleteChoreDto deleteChoreDto { get; set; }
}