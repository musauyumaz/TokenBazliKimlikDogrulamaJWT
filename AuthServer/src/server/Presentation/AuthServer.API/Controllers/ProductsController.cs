using AuthServer.Application.Features.Products.Commands;
using AuthServer.Application.Features.Products.DTOs;
using AuthServer.Application.Features.Products.Queries;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Results;

namespace AuthServer.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]AddProductCommandRequest command)
        {
            Result result = await _mediator.Send(command);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] GetAllProductsQueryRequest query)
        {
            Result<List<ProductDTO>> result = await _mediator.Send(query);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommandRequest command)
        {
            Result result = await _mediator.Send(command);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromRoute] DeleteProductCommandRequest command)
        {
            Result result = await _mediator.Send(command);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdProduct([FromRoute] GetByIdProductCommandRequest query)
        {
            Result<ProductDTO> result = await _mediator.Send(query);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
