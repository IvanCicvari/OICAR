using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly StarplexContext _context;

        public SubscriptionsController(StarplexContext context)
        {
            _context = context;
        }

        // GET: api/Subscriptions/GetSubscriptions
        [HttpGet("GetSubscriptions")]
        [Authorize] // Requires authorization to access
        public async Task<ActionResult<IEnumerable<Subscription>>> GetSubscriptions()
        {
            if (_context.Subscriptions == null)
            {
                return NotFound();
            }
            return await _context.Subscriptions.ToListAsync();
        }

        // GET: api/Subscriptions/GetSubscription/5
        [HttpGet("GetSubscription/{id}")]
        [AllowAnonymous] // Allows anonymous access
        public async Task<ActionResult<Subscription>> GetSubscription(int id)
        {
            if (_context.Subscriptions == null)
            {
                return NotFound();
            }
            var subscription = await _context.Subscriptions.FindAsync(id);

            if (subscription == null)
            {
                return NotFound();
            }

            return subscription;
        }

        // PUT: api/Subscriptions/UpdateSubscription/5
        [HttpPut("UpdateSubscription/{id}")]
        [Authorize] // Requires authorization to access
        public async Task<IActionResult> PutSubscription(int id, Subscription subscription)
        {
            if (id != subscription.SubscriptionId)
            {
                return BadRequest();
            }

            _context.Entry(subscription).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscriptionExists(id))
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

        // POST: api/Subscriptions/CreateSubscription
        [HttpPost("CreateSubscription")]
        [Authorize] // Requires authorization to access
        public async Task<ActionResult<Subscription>> PostSubscription(Subscription subscription)
        {
            if (_context.Subscriptions == null)
            {
                return Problem("Entity set 'StarplexContext.Subscriptions' is null.");
            }
            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubscription", new { id = subscription.SubscriptionId }, subscription);
        }

        // DELETE: api/Subscriptions/DeleteSubscription/5
        [HttpDelete("DeleteSubscription/{id}")]
        [Authorize] // Requires authorization to access
        public async Task<IActionResult> DeleteSubscription(int id)
        {
            if (_context.Subscriptions == null)
            {
                return NotFound();
            }
            var subscription = await _context.Subscriptions.FindAsync(id);
            if (subscription == null)
            {
                return NotFound();
            }

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubscriptionExists(int id)
        {
            return (_context.Subscriptions?.Any(e => e.SubscriptionId == id)).GetValueOrDefault();
        }
    }
}
