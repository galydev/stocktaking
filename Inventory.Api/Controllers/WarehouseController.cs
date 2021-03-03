using AutoMapper;
using Inventory.Application.Commands.Warehouses;
using Inventory.Application.DTOs;
using Inventory.Application.HttpErrors;
using Inventory.Application.Queries.Warehouses;
using Inventory.Application.Wrappers;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Api.Controllers
{
    [EnableCors("CorsPolicyInventory")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WarehouseController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
        }

        /// <summary>
        /// Get list warehouses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpError))]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllWarehouseQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HttpError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(HttpError))]
        public async Task<IActionResult> CreateWarehouse([FromBody] CreateWarehouseCommand createWarehouseCommand)
        {
            var result = await _mediator.Send(createWarehouseCommand);
            return Ok(result);
        }
    }
}