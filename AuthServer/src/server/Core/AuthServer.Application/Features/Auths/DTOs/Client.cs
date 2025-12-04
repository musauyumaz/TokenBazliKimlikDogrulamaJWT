namespace AuthServer.Application.Features.Auths.DTOs
    public record Client(string Id, string Secret, List<string> Audiences);

}
