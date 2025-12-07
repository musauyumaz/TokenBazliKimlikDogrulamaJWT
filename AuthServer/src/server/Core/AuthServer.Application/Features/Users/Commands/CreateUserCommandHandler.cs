using AuthServer.Application.Features.Users.DTOs;
using AuthServer.Domain.Entities;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.Results;
using System.Net;

namespace AuthServer.Application.Features.Users.Commands
{
    public record CreateUserCommandRequest(string Email, string UserName, string City, string Password) : IRequest<Result<UserDTO>>;
    public sealed class CreateUserCommandHandler(UserManager<User> _userManager) : IRequestHandler<CreateUserCommandRequest, Result<UserDTO>>
    {
        public async ValueTask<Result<UserDTO>> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _userManager.CreateAsync(request.Adapt<User>(), request.Password);

            if (!result.Succeeded)
                return Result<UserDTO>.Fail(string.Join(",\n ", result.Errors.Select(e => e.Description)),HttpStatusCode.BadRequest,true);
            return Result<UserDTO>.Success((await _userManager.FindByEmailAsync(request.Email)).Adapt<UserDTO>(),HttpStatusCode.Created,true);
        }
    }

}
