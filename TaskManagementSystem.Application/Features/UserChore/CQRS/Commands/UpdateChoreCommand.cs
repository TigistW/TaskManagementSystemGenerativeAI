using MediatR;
using TaskManagementSystem.Application.Features.UserChore.DTO;
using TaskManagementSystem.Application.Responses;

namespace TaskManagementSystem.Application.Features.UserChore.CQRS.Commands;

public class UpdateChoreCommand : IRequest<BaseCommandResponse<UpdateChoreDto>>
{
    public UpdateChoreDto updateChoreDto {get; set;}
}
