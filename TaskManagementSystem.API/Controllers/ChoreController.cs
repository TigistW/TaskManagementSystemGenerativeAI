
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Features.UserChore.CQRS.Commands;
using TaskManagementSystem.Application.Features.UserChore.CQRS.Queries;
using TaskManagementSystem.Application.Features.UserChore.DTO;

namespace TaskManagementSystem.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ChoreController : BaseController
{
    private IMediator _mediator { get; set; }

    public ChoreController(IMediator mediator)
    {
        _mediator = mediator;
        
    }

    [HttpGet]

    public async Task<ActionResult<List<ChoreListDto>>>  GetListOfChore (){
        return HandleResult(await _mediator.Send(new GetChoreListQueries()));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetChoreDetail (int id){
        return HandleResult(await _mediator.Send(new GetChoreDetailQueries {Id = id}));
    }

    [HttpPost]

    public async Task<ActionResult> CreateChore ([FromBody] CreateChoreDto createChoreDto){
        return HandleResult(await _mediator.Send(new CreateChoreCommand {createChoreDto = createChoreDto}));
    }

    [HttpPut]
    public async Task<ActionResult> UpdateChore ([FromBody] UpdateChoreDto updateChoreDto){
        return HandleResult(await _mediator.Send(new UpdateChoreCommand {updateChoreDto = updateChoreDto}));

    }

    [HttpDelete]
    public async Task<ActionResult> DeleteChore ([FromBody] DeleteChoreDto deleteChoreDto){

        return HandleResult(await _mediator.Send(new DeleteChoreCommand {deleteChoreDto = deleteChoreDto}));
    }
}




