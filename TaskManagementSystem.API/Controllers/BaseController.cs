
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Responses;

namespace TaskManagementSystem.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class BaseController : ControllerBase
{
    protected ActionResult HandleResult<T>(BaseCommandResponse<T> result)
    {
        if (result == null)
            return NotFound(result);
        else if (result.Success && result.Value != null)
            return Ok(result);
        else if (result.Success && result.Value == null)
            return NotFound(result);
        else
            return BadRequest(result);
    }
}
