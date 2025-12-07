using AuthServer.Application.Abstractions.Repositories;
using AuthServer.Domain.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Results;
using System.Net;

namespace AuthServer.Application.Features.Auths.Commands
{
    public record RevokeRefreshTokenCommandRequest(string RefreshToken) : IRequest<Result<string>>;
    public sealed class RevokeRefreshTokenCommandHandler(IReadRepository<UserRefreshToken> _readRepository, IWriteRepository<UserRefreshToken> _writeRepository) : IRequestHandler<RevokeRefreshTokenCommandRequest, Result<string>>
    {
        public async ValueTask<Result<string>> Handle(RevokeRefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            var existRefreshToken = await _readRepository.GetWhere(x => x.Code == request.RefreshToken).FirstOrDefaultAsync();
            if (existRefreshToken == null)
                return Result<string>.Fail("Refresh Token Not Found", HttpStatusCode.NotFound, true);

            await _writeRepository.ExecuteDeleteAsync(x => x.Code == request.RefreshToken);
            await _writeRepository.SaveChangesAsync();

            return Result<string>.Success("RefreshToken deleted");
        }
    }

}
