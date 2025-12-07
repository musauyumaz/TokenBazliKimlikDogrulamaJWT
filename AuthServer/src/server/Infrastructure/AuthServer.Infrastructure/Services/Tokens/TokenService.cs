using AuthServer.Application.Abstractions.Services;
using AuthServer.Application.Features.Auths.DTOs;
using AuthServer.Domain.Entities;
using AuthServer.SharedLibrary.DTOs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthServer.Infrastructure.Services.Tokens
{
    public sealed class TokenService(IOptions<CustomTokenOption> _customTokenOption) : ITokenService
    {
        public TokenDTO CreateTokenAsync(User user)
        {
            var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_customTokenOption.Value.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.UtcNow.AddMinutes(_customTokenOption.Value.RefreshTokenExpiration);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_customTokenOption.Value.SecurityKey));

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken = new(
                issuer: _customTokenOption.Value.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.UtcNow,
                claims: GetClaimsByUserAsync(user, _customTokenOption.Value.Audience),
                signingCredentials: signingCredentials
                );

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);
            return new(token, accessTokenExpiration, CreateRefreshToken(), refreshTokenExpiration);
        }
           

        public ClientTokenDTO CreateTokenByClientAsync(Client client)
        {
            var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_customTokenOption.Value.AccessTokenExpiration);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_customTokenOption.Value.SecurityKey));

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken = new(
                issuer: _customTokenOption.Value.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.UtcNow,
                claims: GetClaimsByClient(client),
                signingCredentials: signingCredentials
                );

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);
            return new(token, accessTokenExpiration);
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

        private IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var clientClaims = new List<Claim>()
            {
                new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub,client.Id)
            };
            clientClaims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud,x)));

            return clientClaims;
        }
    }

}
