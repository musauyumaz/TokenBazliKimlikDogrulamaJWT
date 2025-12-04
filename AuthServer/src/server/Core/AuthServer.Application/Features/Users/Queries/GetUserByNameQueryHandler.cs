using AuthServer.Application.Features.Users.DTOs;
using AuthServer.Domain.Entities;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.Results;

namespace AuthServer.Application.Features.Users.Queries
{
    public record GetUserByNameQueryRequest(string UserName) : IRequest<Result<UserDTO>>;
    public sealed class GetUserByNameQueryHandler(UserManager<User> _userManager) : IRequestHandler<GetUserByNameQueryRequest, Result<UserDTO>>
    {
        public async ValueTask<Result<UserDTO>> Handle(GetUserByNameQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            return user is not null
                ? Result<UserDTO>.Success(user.Adapt<UserDTO>())
                : Result<UserDTO>.Fail("User not found", System.Net.HttpStatusCode.NotFound);
        }
    }
}
