using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShoppingTrackAPI.HelperFunctions;
using ShoppingTrackAPI.Models;

namespace ShoppingTrackAPI.Features.Prices
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PricesController : ControllerBase
    {
        private readonly ShoppingTrackContext _context;
        private readonly IMediator _mediator;
        private readonly IHelper _helper;

        public PricesController(ShoppingTrackContext context, IMediator mediator, IHelper helper)
        {
            _context = context;
            _mediator = mediator;
            _helper = helper;
        }

        // GET: api/Prices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Price>>> GetPrices()
        {
            var cancellationToken = _helper.GetCancellationToken(3000);
            var prices = await _mediator.Send(new GetPrices.Query(), cancellationToken);
            return Ok(prices);
        }

        [HttpGet]
        [Route("Estimate")]
        public async Task<ActionResult<string>> GetEstimate(Guid userId)
        {
            var cancellationToken = _helper.GetCancellationToken(3000);
            var estimate = await _mediator.Send(new GetEstimate.Query(userId), cancellationToken);
            return Ok(estimate);
        }

        // GET: api/Prices/5
        [HttpGet("{itemId}")]
        public async Task<ActionResult<IEnumerable<Price>>> GetPrices(Guid itemId)
        {
            var cancellationToken = _helper.GetCancellationToken(3000);
            var prices = await _mediator.Send(new GetPrices.Query() { ItemId = itemId }, cancellationToken);

            if (prices == null)
            {
                return NotFound();
            }

            return Ok(prices);
        }

        // POST: api/Prices
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Price>> PostPrices([FromBody]Price price)
        {
            var cancellationToken = _helper.GetCancellationToken(3000);
            var response = await _mediator.Send(new AddPrice.Command(price), cancellationToken);

            return CreatedAtAction("GetPrices", new { itemId = price.ItemId }, price);
        }

        // DELETE: api/Prices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePrices(Guid id)
        {
            var cancellationToken = _helper.GetCancellationToken(3000);
            var price = await _mediator.Send(new DeletePrice.Command(id), cancellationToken);
            
            if (price is null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
