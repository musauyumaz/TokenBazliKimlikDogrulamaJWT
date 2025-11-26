using AuthServer.Application.Abstractions.Services;
using AuthServer.Application.Features.Auths.DTOs;
using AuthServer.Domain.Entities;
using AuthServer.Infrastructure.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace AuthServer.Infrastructure.Services
{
    public sealed class TokenService(UserManager<User> _userManager, IOptions<CustomTokenOption> _customTokenOption) : ITokenService
    {
        public TokenDTO CreateTokenAsync(User user)
        {
            throw new NotImplementedException();
        }

        public ClientTokenDTO CreateTokenByClientAsync(string userId, string password)
        {
            throw new NotImplementedException();
        }

        private string CreateRefreshToken()
        {
            byte[]? numberByte = new byte[32];
            using RandomNumberGenerator? randomNumber = RandomNumberGenerator.Create();
            randomNumber.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }
    }

}
