using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingTrackAPI.Models;

namespace ShoppingTrackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private static ShoppingTrackContext _context;
        private ErrorLogsController _errorContext = new ErrorLogsController(_context);

        public ItemsController(ShoppingTrackContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Items>>> GetItems(int? user_id = null)
        {
            if (user_id.HasValue)
            {
                return await _context.Items.Where(x=>x.User_Id == user_id && !x.Deleted).ToListAsync();
            }

            return await _context.Items.Where(x => !x.Deleted).ToListAsync();
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Items>> GetItems(int id)
        {
            var items = await _context.Items.FindAsync(id);

            if (items == null)
            {
                return NotFound();
            }

            return items;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItems(int id, [FromBody]Items items)
        {
            if (id != items.ItemId)
            {
                return BadRequest();
            }

            _context.Entry(items).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemsExists(id, items.User_Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Items
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Items>> PostItems([FromBody]Items items)
        {
            //take this out we check this in the front end
            try
            {
                if(items.ItemId == 0)
                {
                    items.ItemId = GetNextAvailableId();
                }
                //item doesnt exist for user
                if (!_context.Items.Where(x => x.Name == items.Name && x.User_Id == items.User_Id).Any())
                {
                    _context.Items.Add(items);
                    await _context.SaveChangesAsync();
                }
                //item exist for user, but is deleted
                if(_context.Items.Where(x => x.Name == items.Name && x.User_Id == items.User_Id && x.Deleted).Any())
                {
                    var existingItem = await _context.Items.FindAsync(items.ItemId);
                    existingItem.Deleted = false;
                    _context.Items.Update(items);
                    await _context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                var error = new ErrorLog();
                error.Location = nameof(this.PostItems);
                error.CallStack = ex.StackTrace;
                _errorContext.PostErrorLog(error);
            }

            return CreatedAtAction("GetItems", new { id = items.ItemId }, items);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Items>> DeleteItems(int id)
        {
            var items = await _context.Items.FindAsync(id);
            if (items == null)
            {
                return NotFound();
            }

            items.Deleted = true;
            _context.Items.Update(items);
            await _context.SaveChangesAsync();

            return items;
        }

        private bool ItemsExists(int id, int user_id)
        {
            return _context.Items.Any(e => e.ItemId == id && e.User_Id == user_id);
        }

        private int GetNextAvailableId()
        {
            return _context.Items.OrderByDescending(x => x.ItemId).FirstOrDefault().ItemId + 1;
        }
    }
}
