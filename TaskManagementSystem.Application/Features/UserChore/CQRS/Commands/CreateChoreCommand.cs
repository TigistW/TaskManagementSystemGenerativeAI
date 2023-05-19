using MediatR;
using TaskManagementSystem.Application.Features.UserChore.DTO;
using TaskManagementSystem.Application.Responses;

namespace TaskManagementSystem.Application.Features.UserChore.CQRS.Commands;

public class CreateChoreCommand : IRequest<BaseCommandResponse<CreateChoreDto>>
{
    public CreateChoreDto createChoreDto { get; set; }
}
