using AuthServer.Application.Features.Auths.Commands;
using AuthServer.Application.Features.Auths.DTOs;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Results;

namespace AuthServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Login(AuthLoginCommandRequest authLoginCommandRequest)
        {
            Result<TokenDTO> result = await _mediator.Send(authLoginCommandRequest);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> ClientLogin(ClientLoginCommandRequest clientLoginCommandRequest)
        {
            Result<ClientTokenDTO> result = await _mediator.Send(clientLoginCommandRequest);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RevokeRefreshTokenCommandRequest revokeRefreshTokenCommandRequest)
        {
            Result<string> result = await _mediator.Send(revokeRefreshTokenCommandRequest);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(CreateRefreshTokenCommandRequest createRefreshTokenCommandRequest)
        {
            Result<TokenDTO> result = await _mediator.Send(createRefreshTokenCommandRequest);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
