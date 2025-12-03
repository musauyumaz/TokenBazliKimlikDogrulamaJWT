namespace AuthServer.Infrastructure.Services.Tokens.Configurations
{
    public record Client(string Id, string Secret, List<string> Audiences);

}
