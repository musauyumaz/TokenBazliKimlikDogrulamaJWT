using AuthServer.Application.Features.Auths.DTOs;
using AuthServer.Domain.Entities;

namespace AuthServer.Application.Abstractions.Services
{
    public interface ITokenService
    {
        TokenDTO CreateTokenAsync(User user);
        ClientTokenDTO CreateTokenByClientAsync(Client client);
    }
}
