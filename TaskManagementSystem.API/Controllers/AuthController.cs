using System.Net;
namespace TaskManagementSystem.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using MediatR;
using AutoMapper;
using TaskManagementSystem.Application.Models.Identity;
using TaskManagementSystem.Application.Responses;
using TaskManagementSystem.Application.Features.Users.CQRS.Commands;
using TaskManagementSystem.Application.Features.Users.DTO;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AuthController(IAuthService authService, IMediator mediator, IMapper mapper)
    {
        _authService = authService;
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("Login")]
    
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginModel request)
    {
        var response = await _authService.Login(request);
        return HandleResult(response);
    }

    [HttpPost]
    [Route("Register")]
    public async Task<ActionResult<BaseCommandResponse<RegisterResponse>>> Register([FromBody] RegisteDto registerDto)
    {
        var response = await _authService.Register(_mapper.Map<RegisterModel>(registerDto));
        var command = new CreateUserCommand { createUserDto = _mapper.Map<CreateUserDto>(registerDto) };
        if (!response.Success || response.Value == null)
            return HandleResult(response);

        command.createUserDto.AccountId = response.Value.UserId;
        try
        {
            var userResponse = await _mediator.Send(command);
            return HandleResult(response);
        }
        catch (Exception e)
        {
            await _authService.DeleteUser(registerDto.Email);
            response.Success = false;
            response.Errors.Add(e.Message);
            return HandleResult(response);
        }

    }

    [HttpDelete]
    [Route("Delete")]
    [Authorize]
    public async Task<ActionResult<bool>> Delete(string email)
    {
        var response = await _authService.DeleteUser(email);
        return Ok(response);
    }
}