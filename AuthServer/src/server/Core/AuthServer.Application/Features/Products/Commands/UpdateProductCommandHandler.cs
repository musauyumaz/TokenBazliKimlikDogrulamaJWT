using AuthServer.Application.Abstractions.Repositories;
using AuthServer.Domain.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Results;
using System.Net;

namespace AuthServer.Application.Features.Products.Commands
{
    public record UpdateProductCommandRequest(string Id, string Name, decimal Price, string UserId) : IRequest<Result>;
    public sealed class UpdateProductCommandHandler(IWriteRepository<Product> _writeRepository, IReadRepository<Product> _readRepository) : IRequestHandler<UpdateProductCommandRequest, Result>
    {
        public async ValueTask<Result> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _readRepository.GetByIdAsync(request.Id);
            if (product is null)
                return Result.Fail("Product not found", System.Net.HttpStatusCode.NotFound, true);

            await _writeRepository.Table
                .Where(p => p.Id == request.Id)
                .ExecuteUpdateAsync(setters =>setters
            .SetProperty(p => p.Name, request.Name)
            .SetProperty(p => p.Price, request.Price)
            .SetProperty(p => p.UserId, request.UserId)
            .SetProperty(p => p.UpdatedDate, DateTime.UtcNow));

            return Result.Success(HttpStatusCode.NoContent);
        }
    }

}
