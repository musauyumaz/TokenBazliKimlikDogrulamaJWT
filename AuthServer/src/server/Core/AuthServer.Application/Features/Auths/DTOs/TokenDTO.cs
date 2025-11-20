namespace AuthServer.Application.Features.Auths.DTOs
{
    public record TokenDTO(string AccessToken, DateTime AccessTokenExpiration, string RefreshToken, DateTime RefreshTokenExpiration);
}
