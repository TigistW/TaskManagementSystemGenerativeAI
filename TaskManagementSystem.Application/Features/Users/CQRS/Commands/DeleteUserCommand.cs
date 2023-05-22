using MediatR;
using TaskManagementSystem.Application.Features.Users.DTO;
using TaskManagementSystem.Application.Responses;

namespace TaskManagementSystem.Application.Features.Users.CQRS.Commands;

public class DeleteUserCommand : IRequest<BaseCommandResponse<Unit>>
{
    public DeleteUserDto deleteUserDto {get;set;}
}
