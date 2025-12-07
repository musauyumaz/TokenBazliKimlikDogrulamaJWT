namespace AuthServer.SharedLibrary.DTOs
{
    public class CustomTokenOption
    {
        public string Issuer { get; set; } = string.Empty;
        public List<string> Audience { get; set; } = new();
        public string SecurityKey { get; set; } = string.Empty;
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
    }

}
