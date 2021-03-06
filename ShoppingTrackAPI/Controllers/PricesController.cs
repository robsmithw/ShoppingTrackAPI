using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShoppingTrackAPI.Models;

namespace ShoppingTrackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        private readonly ShoppingTrackContext _context;

        public PricesController(ShoppingTrackContext context)
        {
            _context = context;
        }

        // GET: api/Prices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prices>>> GetPrices()
        {
            return await _context.Prices.ToListAsync();
        }

        [HttpGet]
        [Route("Estimate")]
        public ActionResult<decimal> GetEstimate(int userId)
        {
            decimal estimate = 0;

            //get all items that for user that are not purchased or deleted.
            var purchasedItemIds = _context.Items.Where(item => item.User_Id == userId && !item.Purchased && !item.Deleted).Select(item => item.ItemId).ToList();
            var prices = _context.Prices.Where(price => purchasedItemIds.Contains(price.ItemId)).ToList();
            foreach(var price in prices)
            {
                estimate += price.Price;
            }

            var estimateJson = JsonConvert.SerializeObject(estimate);

            return Ok(estimateJson);
        }

        // GET: api/Prices/5
        [HttpGet("{itemId}")]
        public async Task<ActionResult<IEnumerable<Prices>>> GetPrices(int itemId)
        {
            var prices = await _context.Prices.Where(price => price.ItemId == itemId).ToListAsync();

            if (prices == null)
            {
                return NotFound();
            }

            return prices;
        }

        // PUT: api/Prices/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrices(int id, Prices prices)
        {
            if (id != prices.Id)
            {
                return BadRequest();
            }

            _context.Entry(prices).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PricesExists(id))
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

        // POST: api/Prices
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Prices>> PostPrices([FromBody]Prices prices)
        {

            if(prices.Id == 0)
            {
                prices.Id = GetNextAvailableId();
            }

            _context.Prices.Add(prices);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrices", new { itemId = prices.ItemId }, prices);
        }

        // DELETE: api/Prices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Prices>> DeletePrices(int id)
        {
            var prices = await _context.Prices.FindAsync(id);
            if (prices == null)
            {
                return NotFound();
            }

            _context.Prices.Remove(prices);
            await _context.SaveChangesAsync();

            return prices;
        }

        private bool PricesExists(int id)
        {
            return _context.Prices.Any(e => e.Id == id);
        }

        private int GetNextAvailableId()
        {
            return _context.Prices.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
        }
    }
}
