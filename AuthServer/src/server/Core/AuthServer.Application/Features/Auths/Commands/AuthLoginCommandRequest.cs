using AuthServer.Application.Abstractions.Repositories;
using AuthServer.Application.Abstractions.Services;
using AuthServer.Application.Features.Auths.DTOs;
using AuthServer.Domain.Entities;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Results;
using System.Net;

namespace AuthServer.Application.Features.Auths.Commands;

public record AuthLoginCommandRequest(string UserNameOrEmail, string Password) : IRequest<Result<TokenDTO>>;

public sealed class AuthLoginCommandHandler(UserManager<User> _userManager, ITokenService _tokenService, IReadRepository<UserRefreshToken> _userRefreshTokenReadRepository, IWriteRepository<UserRefreshToken> _userRefreshTokenWriteRepository) : IRequestHandler<AuthLoginCommandRequest, Result<TokenDTO>>
{
    async ValueTask<Result<TokenDTO>> IRequestHandler<AuthLoginCommandRequest, Result<TokenDTO>>.Handle(AuthLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.UserNameOrEmail) ?? await _userManager.FindByNameAsync(request.UserNameOrEmail);
        if (user == null)
            return Result<TokenDTO>.Fail("Email or Password is wrong", HttpStatusCode.BadRequest, true);

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
            return Result<TokenDTO>.Fail("Email or Password is wrong", HttpStatusCode.BadRequest, true);

        var token = _tokenService.CreateTokenAsync(user);

        var userRefreshToken = await _userRefreshTokenReadRepository.GetWhere(x => x.UserId == user.Id).FirstOrDefaultAsync(cancellationToken);
        if (userRefreshToken == null)
            await _userRefreshTokenWriteRepository.AddAsync(new() { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
        else
        {
            userRefreshToken.Code = token.RefreshToken;
            userRefreshToken.Expiration = token.RefreshTokenExpiration;
        }

        await _userRefreshTokenWriteRepository.SaveChangesAsync();

        return Result<TokenDTO>.Success(token);
    }
}