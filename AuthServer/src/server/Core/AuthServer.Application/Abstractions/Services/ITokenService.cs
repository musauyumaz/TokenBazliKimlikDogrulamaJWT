using AuthServer.Application.Features.Auths.DTOs;

namespace AuthServer.Application.Abstractions.Services
{
    public interface ITokenService
    {
        TokenDTO CreateTokenAsync(string userId, string password);
        ClientTokenDTO CreateTokenByClientAsync(string userId, string password);
    }
}
