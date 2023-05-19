using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Commands;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Queries;
using TaskManagementSystem.Application.Features.CheckList.DTO;

namespace TaskManagementSystem.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CheckListControler : BaseController
{
    private IMediator _mediator;
    
    public CheckListControler(IMediator mediator)
    {
        _mediator = mediator;
        
    }

    [HttpGet]

    public async Task<ActionResult> GetCheckLists(){
        return HandleResult( await _mediator.Send(new GetCheckListQueries()));
    }


    [HttpPost]

    public async Task<ActionResult> CreateCheckList ([FromBody] CreateCheckListDto createCheckListDto){
        return HandleResult(await _mediator.Send(new CreateCheckListCommand {createCheckListDto = createCheckListDto}));
    }

    [HttpPut]

    public async Task<ActionResult> UpdateCheckList ([FromBody] UpdateCheckListDto updateCheckListDto){
        return HandleResult(await _mediator.Send(new UpdateCheckListCommand {updateCheckListDto = updateCheckListDto}));
    }

    [HttpDelete("id: int")]

    public async Task<ActionResult> DeleteCheckList ([FromBody] DeleteCheckListDto deleteCheckListDto){
        return HandleResult(await _mediator.Send(new DeleteCheckListCommand{deleteCheckListDto = deleteCheckListDto}));
    }
}
