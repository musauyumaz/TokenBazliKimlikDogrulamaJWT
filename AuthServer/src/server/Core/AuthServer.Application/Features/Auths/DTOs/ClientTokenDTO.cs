namespace AuthServer.Application.Features.Auths.DTOs
{
    public record ClientTokenDTO(string AccessToken, DateTime AccessTokenExpiration);
}
