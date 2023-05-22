using MediatR;
using TaskManagementSystem.Application.Features.Users.DTO;
using TaskManagementSystem.Application.Responses;

namespace TaskManagementSystem.Application.Features.Users.CQRS.Commands;

public class CreateUserCommand : IRequest<BaseCommandResponse<CreateUserDto>>
{
    public CreateUserDto createUserDto {get; set;}
}
