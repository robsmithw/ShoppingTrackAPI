using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShoppingTrackAPI.HelperFunctions;
using ShoppingTrackAPI.Models;

namespace ShoppingTrackAPI.Features.Items
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHelper _helper;

        public ItemsController(IMediator mediator, IHelper helper)
        {
            _mediator = mediator;
            _helper = helper;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems(Guid? userId = null)
        {
            var cancellationToken = _helper.GetCancellationToken(3000);
            var response = await _mediator.Send(new GetItems.Query(){ UserId = userId }, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route(nameof(GetItemsByStoreId))]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsByStoreId(Guid storeId, Guid userId)
        {
            var cancellationToken = _helper.GetCancellationToken(3000);
            var response = await _mediator.Send(new GetItems.Query(){ UserId = userId, StoreId = storeId }, cancellationToken);
            return Ok(response);
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItems(Guid id)
        {
            var cancellationToken = _helper.GetCancellationToken(3000);
            var response = await _mediator.Send(new GetItem.Query(id), cancellationToken);

            if(response is null) return NotFound();

            return Ok(response);
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItems(Guid id, [FromBody]Item item)
        {
            if (id != item.Id) return BadRequest();

            var cancellationToken = _helper.GetCancellationToken(3000);
            var responseItem = await _mediator.Send(new UpdateItem.Command(id, item), cancellationToken);

            return CreatedAtAction("GetItems", new { id = responseItem.Id }, responseItem);
        }

        // POST: api/Items
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Item>> PostItems([FromBody]Item requestedItem)
        {
            var cancellationToken = _helper.GetCancellationToken(3000);
            Item item = null;
            var result = await _mediator.Send(new AddItem.Command(requestedItem), cancellationToken);
            item = result.Item;

            if (!result.Successful)
                return BadRequest(result.Error);

            return CreatedAtAction("GetItems", new { id = item.Id }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> DeleteItems(Guid id)
        {
            var cancellationToken = _helper.GetCancellationToken(3000);
            var item = await _mediator.Send(new DeleteItem.Command(id), cancellationToken);
            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }
    }
}
