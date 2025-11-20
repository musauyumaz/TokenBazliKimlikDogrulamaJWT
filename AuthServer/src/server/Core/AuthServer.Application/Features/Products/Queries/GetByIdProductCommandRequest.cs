using AuthServer.Application.Abstractions.Repositories;
using AuthServer.Application.Features.Products.DTOs;
using AuthServer.Domain.Entities;
using Mapster;
using Mediator;
using SharedLibrary.Results;

namespace AuthServer.Application.Features.Products.Queries
{
    public record GetByIdProductCommandRequest(string Id) : IRequest<Result<ProductDTO>>;
    public sealed class GetByIdProductCommandHandler(IReadRepository<Product> _readRepository) : IRequestHandler<GetByIdProductCommandRequest, Result<ProductDTO>>
    {
        public async ValueTask<Result<ProductDTO>> Handle(GetByIdProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _readRepository.GetByIdAsync(request.Id);
            if (product is null)
                return Result<ProductDTO>.Fail("Product not found");
            return Result<ProductDTO>.Success(product.Adapt<ProductDTO>());
        }
    }

}
