namespace AuthServer.Application.Features.Auths.Commands;

public record AuthLoginCommandRequest(string UserNameOrEmail, string Password)
{
}
