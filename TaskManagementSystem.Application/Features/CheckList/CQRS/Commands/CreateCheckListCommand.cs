using MediatR;
using TaskManagementSystem.Application.Features.CheckList.DTO;
using TaskManagementSystem.Application.Responses;

namespace TaskManagementSystem.Application.Features.CheckList.CQRS.Commands;

public class CreateCheckListCommand : IRequest<BaseCommandResponse<CreateCheckListDto>>
{
    public CreateCheckListDto createCheckListDto {get;set;}
}
