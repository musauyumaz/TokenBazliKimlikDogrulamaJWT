using AuthServer.Application.Features.Users.Commands;
using AuthServer.Application.Features.Users.DTOs;
using AuthServer.Application.Features.Users.Queries;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Results;

namespace AuthServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            Result<UserDTO> result = await _mediator.Send(createUserCommandRequest);
            return StatusCode(((int)result.StatusCode), result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserByName(GetUserByNameQueryRequest getUserByNameQueryRequest)
        {
            Result<UserDTO> result = await _mediator.Send(getUserByNameQueryRequest);
            return StatusCode(((int)result.StatusCode), result);
        }
    }
}
