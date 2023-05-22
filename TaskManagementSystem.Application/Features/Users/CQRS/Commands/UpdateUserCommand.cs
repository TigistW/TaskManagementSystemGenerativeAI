using MediatR;
using TaskManagementSystem.Application.Features.Users.DTO;
using TaskManagementSystem.Application.Responses;

namespace TaskManagementSystem.Application.Features.Users.CQRS.Commands;

public class UpdateUserCommand : IRequest<BaseCommandResponse<UpdateUserDto>>
{
    public UpdateUserDto updateUserDto {get; set;}
}
