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
    /// <summary>
    /// Represents the API for managing views.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ViewsController : ControllerBase
    {
        private readonly StarplexContext _context;

        public ViewsController(StarplexContext context)
        {
            _context = context;
        }

        // GET: api/Views/GetAllViews
        [HttpGet("GetAllViews")]
        [Authorize] // Requires authorization to access
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<View>>> GetViews()
        {
            if (_context.Views == null)
            {
                return NotFound();
            }
            return await _context.Views.ToListAsync();
        }

        // GET: api/Views/GetView/5
        [HttpGet("GetView/{id}")]
        [AllowAnonymous] // Allows anonymous access
        public async Task<ActionResult<View>> GetView(int id)
        {
            if (_context.Views == null)
            {
                return NotFound();
            }
            var view = await _context.Views.FindAsync(id);

            if (view == null)
            {
                return NotFound();
            }

            return view;
        }

        // PUT: api/Views/UpdateView/5
        [HttpPut("UpdateView/{id}")]
        [Authorize] // Requires authorization to access
        public async Task<IActionResult> PutView(int id, View view)
        {
            if (id != view.ViewId)
            {
                return BadRequest();
            }

            _context.Entry(view).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ViewExists(id))
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

        // POST: api/Views/AddView
        [HttpPost("AddView")]
        [Authorize] // Requires authorization to access
        public async Task<ActionResult<View>> PostView(View view)
        {
            if (_context.Views == null)
            {
                return Problem("Entity set 'StarplexContext.Views' is null.");
            }
            _context.Views.Add(view);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetView", new { id = view.ViewId }, view);
        }

        // DELETE: api/Views/DeleteView/5
        [HttpDelete("DeleteView/{id}")]
        [Authorize] // Requires authorization to access
        public async Task<IActionResult> DeleteView(int id)
        {
            if (_context.Views == null)
            {
                return NotFound();
            }
            var view = await _context.Views.FindAsync(id);
            if (view == null)
            {
                return NotFound();
            }

            _context.Views.Remove(view);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ViewExists(int id)
        {
            return (_context.Views?.Any(e => e.ViewId == id)).GetValueOrDefault();
        }
    }
}
