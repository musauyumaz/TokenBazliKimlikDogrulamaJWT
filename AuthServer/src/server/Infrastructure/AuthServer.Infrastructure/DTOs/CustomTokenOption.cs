namespace AuthServer.Infrastructure.DTOs
{
    public record CustomTokenOption(List<string> Audience, string Issuer, int AccessTokenExpiration, int RefreshTokenExpiration, string SecurityKey);

}
