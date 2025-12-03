namespace AuthServer.Infrastructure.DTOs
{
    public record CustomClientOption(string Id, string Secret, List<string> Audiences);

}
