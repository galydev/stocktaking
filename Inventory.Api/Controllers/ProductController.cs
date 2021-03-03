using Inventory.Application.Commands.Products;
using Inventory.Application.HttpErrors;
using Inventory.Application.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Inventory.Api.Controllers
{
    [EnableCors("CorsPolicyInventory")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Get list products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpError))]
        public async Task<IActionResult> GetListProducts()
        {
            var query = new GetAllProductsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get product for id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("byid/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpError))]
        public async Task<IActionResult> ProductById(Guid id)
        {
            var query = new GetProductByIdQuery(id);
            var response = await _mediator.Send(query);
            return response != null ? Ok(response) : NotFound();
        }

        /// <summary>
        /// Get product for name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("byname/{name}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpError))]
        public async Task<IActionResult> GetProductByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            var query = new GetProductByNameQuery(name);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Create product
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpError))]
        public async Task<IActionResult> Product([FromBody] CreateProductCommand productDto)
        {
            var result = await _mediator.Send(productDto);
            return Ok(result);
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productCommand"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpError))]
        public async Task<IActionResult> Put(Guid id, UpdateProductCommand productCommand)
        {
            if (id != productCommand.Id)
            {
                return BadRequest();
            }

            productCommand.Id = id;
            var result = await _mediator.Send(productCommand);
            return Ok(result);
        }

        /// <summary>
        /// Remove product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpError))]
        public async Task<IActionResult> Delete(Guid id)
        {
            if(id == Guid.Empty)
            {
                return BadRequest();
            }

            var command = new DeleteProductCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}