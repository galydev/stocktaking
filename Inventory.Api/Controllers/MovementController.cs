using Inventory.Application.Commands.Movements;
using Inventory.Application.HttpErrors;
using Inventory.Application.Queries.Movements;
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
    public class MovementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovementController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Create Movement in inventory
        /// </summary>
        /// <param name="createMovement"></param>
        /// <returns></returns>
        [Route("loadwarehouse")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpError))]
        public async Task<IActionResult> CreateMovement([FromBody] CreateMovementCommand createMovement)
        {
            var result = await _mediator.Send(createMovement);
            return Ok(result);
        }

        /// <summary>
        /// Delete Product of Warehouse
        /// </summary>
        /// <param name="removeProductWarehouse"></param>
        /// <returns></returns>
        [Route("removeproductofwarehouse")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpError))]
        public async Task<IActionResult> DeleteProductWarehouse([FromBody] RemoveProductWarehouseCommand removeProductWarehouse)
        {
            var result = await _mediator.Send(removeProductWarehouse);
            return Ok(result);
        }

        /// <summary>
        /// Move Product Other Warehouse
        /// </summary>
        /// <param name="moveProductCommand"></param>
        /// <returns></returns>
        [Route("moveproductotherwarehouse")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpError))]
        public async Task<IActionResult> MoveProductOtherWarehouse([FromBody] MoveProductCommand  moveProductCommand)
        {
            var result = await _mediator.Send(moveProductCommand);
            return Ok(result);
        }

        /// <summary>
        /// Get total shopping product from movements
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Route("totalshoppingproduct")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpError))]
        public async Task<IActionResult> GetTotalShoppingProduct(Guid productId)
        {
            var query = new GetTotalShoppingProductQuery(productId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}