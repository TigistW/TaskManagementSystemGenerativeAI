using MediatR;
using TaskManagementSystem.Application.Features.CheckList.DTO;
using TaskManagementSystem.Application.Responses;

namespace TaskManagementSystem.Application.Features.CheckList.CQRS.Commands;

public class UpdateCheckListCommand : IRequest<BaseCommandResponse<UpdateCheckListDto>>
{
    public UpdateCheckListDto updateCheckListDto {get; set;}
}
