using AuthServer.Application.Abstractions.Repositories;
using AuthServer.Application.Features.Products.DTOs;
using AuthServer.Domain.Entities;
using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Results;

namespace AuthServer.Application.Features.Products.Queries
{
    public record GetAllProductsQueryRequest() : IRequest<Result<List<ProductDTO>>>;
    public sealed class GetAllProductsQueryHandler(IReadRepository<Product> _readRepository) : IRequestHandler<GetAllProductsQueryRequest, Result<List<ProductDTO>>>
    {
        public async ValueTask<Result<List<ProductDTO>>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _readRepository.GetAll().ProjectToType<ProductDTO>().ToListAsync();
            return Result<List<ProductDTO>>.Success(data);
        }
    }

}
