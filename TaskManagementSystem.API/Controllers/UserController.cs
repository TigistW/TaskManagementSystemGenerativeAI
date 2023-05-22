using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Features.Users.CQRS.Commands;
using TaskManagementSystem.Application.Features.Users.CQRS.Queries;
using TaskManagementSystem.Application.Features.Users.DTO;

namespace TaskManagementSystem.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : BaseController
{

    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
        
    }

    [HttpGet]
    public async Task<IActionResult> GetUserList()
    {
        return HandleResult(await _mediator.Send(new GetUserListQuery ()));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserById(int id){
        return HandleResult(await _mediator.Send(new GetUserDetailQuery{Id = id}));
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser ([FromBody] CreateUserDto createUserDto){
        return HandleResult(await _mediator.Send( new CreateUserCommand{createUserDto = createUserDto}));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
    {
        return HandleResult(await _mediator.Send(new UpdateUserCommand{updateUserDto = updateUserDto}));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser([FromBody] DeleteUserDto deleteUserDto)
    {
        return HandleResult(await _mediator.Send(new DeleteUserCommand { deleteUserDto = deleteUserDto }));
    }


}
