using AuthServer.Application.Abstractions.Repositories;
using AuthServer.Application.Abstractions.Services;
using AuthServer.Application.Features.Auths.DTOs;
using AuthServer.Domain.Entities;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Results;
using System.Net;

namespace AuthServer.Application.Features.Auths.Commands
{
    public record CreateRefreshTokenCommandRequest(string RefreshToken) : IRequest<Result<TokenDTO>>;
    public sealed class CreateRefreshTokenCommandHandler(IReadRepository<UserRefreshToken> _userRefreshTokenReadRepository, IWriteRepository<UserRefreshToken> _userRefreshTokenWriteRepository, UserManager<User> _userManager, ITokenService _tokenService) : IRequestHandler<CreateRefreshTokenCommandRequest, Result<TokenDTO>>
    {
        public async ValueTask<Result<TokenDTO>> Handle(CreateRefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            var refreshToken = await _userRefreshTokenReadRepository.GetWhere(x => x.Code == request.RefreshToken).FirstOrDefaultAsync(cancellationToken);
            if (refreshToken == null)
                return Result<TokenDTO>.Fail("Refresh Token not found", HttpStatusCode.NotFound, true);

            var user = await _userManager.FindByIdAsync(refreshToken.UserId);
            if (user == null)
                return Result<TokenDTO>.Fail("User not found", HttpStatusCode.NotFound, true);

            var token = _tokenService.CreateTokenAsync(user);
            refreshToken.Code = token.RefreshToken;
            refreshToken.Expiration = token.RefreshTokenExpiration;

            await _userRefreshTokenWriteRepository.SaveChangesAsync();

            return Result<TokenDTO>.Success(token,HttpStatusCode.OK);
        }
    }

}
