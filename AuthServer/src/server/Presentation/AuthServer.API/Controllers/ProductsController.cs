using AuthServer.Application.Features.Products.Commands;
using AuthServer.Application.Features.Products.DTOs;
using AuthServer.Application.Features.Products.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Results;

namespace AuthServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateProduct(AddProductCommandRequest command)
        {
            Result result = await _mediator.Send(command);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(GetAllProductsQueryRequest query)
        {
            Result<List<ProductDTO>> result = await _mediator.Send(query);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommandRequest command)
        {
            Result result = await _mediator.Send(command);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("/{Id}")]
        public async Task<IActionResult> DeleteProduct(DeleteProductCommandRequest command)
        {
            Result result = await _mediator.Send(command);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdProduct(GetByIdProductCommandRequest query)
        {
            Result<ProductDTO> result = await _mediator.Send(query);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
