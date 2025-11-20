using AuthServer.Application.Abstractions.Repositories;
using AuthServer.Domain.Entities;
using Mediator;
using SharedLibrary.Results;

namespace AuthServer.Application.Features.Products.Commands.Delete;

public record class DeleteProductCommandRequest(string Id) : IRequest<Result>;
public sealed class DeleteProductCommandHandler(IWriteRepository<Product> _writeRepository) : IRequestHandler<DeleteProductCommandRequest, Result>
{
    public async ValueTask<Result> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        var data = await _writeRepository.ExecuteDeleteAsync(x => x.Id == request.Id);
        return Result.Success();
    }
}
