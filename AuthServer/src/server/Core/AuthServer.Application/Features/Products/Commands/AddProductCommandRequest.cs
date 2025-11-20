using AuthServer.Application.Abstractions.Repositories;
using AuthServer.Domain.Entities;
using Mapster;
using Mediator;
using SharedLibrary.Results;

namespace AuthServer.Application.Features.Products.Commands;

public record class AddProductCommandRequest(string UserId, string Name, decimal Price, int Stock) : IRequest<Result>;
public sealed class AddProductCommandHandler(IWriteRepository<Product> _writeRepository) : IRequestHandler<AddProductCommandRequest, Result>
{
    public async ValueTask<Result> Handle(AddProductCommandRequest request, CancellationToken cancellationToken)
    {
        await _writeRepository.AddAsync(request.Adapt<Product>());
        return Result.Success();
    }
}
