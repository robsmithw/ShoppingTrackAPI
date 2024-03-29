using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingTrackAPI.Models;
using MediatR;
using System.Threading;
using ShoppingTrackAPI.HelperFunctions;

namespace ShoppingTrackAPI.Features.Stores
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoresController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHelper _helper;

        public StoresController(IMediator mediator, IHelper helper)
        {
            _mediator = mediator;
            _helper = helper;
        }

        // GET: api/Stores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetStores()
        {
            var cancellationToken = _helper.GetCancellationToken(3000);
            var stores = await _mediator.Send(new GetStores.Query(), cancellationToken);
            return Ok(stores);
        }

        [HttpGet]
        [Route(nameof(GetStoresWithItemsByUser))]
        public async Task<ActionResult<IEnumerable<Store>>> GetStoresWithItemsByUser(Guid userId)
        {
            var cancellationToken = _helper.GetCancellationToken(3000);
            var stores = await _mediator.Send(new GetStores.Query { UserId = userId }, cancellationToken);
            return Ok(stores);
        }

        // GET: api/Stores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStores(Guid id)
        {
            var cancellationToken = _helper.GetCancellationToken(3000);
            var store = await _mediator.Send(new GetStore.Query(id), cancellationToken);

            if (store == null)
            {
                return NotFound();
            }

            return Ok(store);
        }

        // POST: api/Stores
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Store>> PostStores(Store store)
        {
            var cancellationToken = _helper.GetCancellationToken(3000);
            var response = await _mediator.Send(new AddStore.Command(store), cancellationToken);
            return Ok(response);
        }

        // DELETE: api/Stores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Store>> DeleteStores(Guid id)
        {
            var cancellationToken = _helper.GetCancellationToken(3000);
            var response = await _mediator.Send(new DeleteStore.Command(id), cancellationToken);

            return Ok();
        }
    }
}
