using AuthServer.Application.Abstractions.Services;
using AuthServer.Application.Features.Auths.DTOs;
using Mediator;
using Microsoft.Extensions.Options;
using SharedLibrary.Results;

namespace AuthServer.Application.Features.Auths.Commands
{
    public record ClientLoginCommandRequest(string Id, string Secret) : IRequest<Result<ClientTokenDTO>>;
    public sealed class ClientLoginCommandHandler(ITokenService _tokenService, IOptions<List<Client>> options) : IRequestHandler<ClientLoginCommandRequest, Result<ClientTokenDTO>>
    {
        public ValueTask<Result<ClientTokenDTO>> Handle(ClientLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var client = options.Value.FirstOrDefault(c => c.Id == request.Id && c.Secret == request.Secret);
            return client is null ?
                ValueTask.FromResult(Result<ClientTokenDTO>.Fail("Client not found", System.Net.HttpStatusCode.Unauthorized)) :
                ValueTask.FromResult(Result<ClientTokenDTO>.Success(_tokenService.CreateTokenByClientAsync(client)));
        }
    }

}
