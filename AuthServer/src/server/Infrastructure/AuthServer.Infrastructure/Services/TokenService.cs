using AuthServer.Application.Abstractions.Services;
using AuthServer.Application.Features.Auths.DTOs;
using AuthServer.Domain.Entities;
using AuthServer.Infrastructure.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

        private IEnumerable<Claim> GetClaimsByUserAsync(User user, List<string> audiences)
        {
            var userClaims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email, user.Email),
                new(ClaimTypes.Name , user.UserName),
                new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            userClaims.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud,x)));

            return userClaims;
        }
    }

}
