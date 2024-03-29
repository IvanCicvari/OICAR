using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewsController : ControllerBase
    {
        private readonly StarplexContext _context;

        public ViewsController(StarplexContext context)
        {
            _context = context;
        }

        // GET: api/Views
        [HttpGet]
        public async Task<ActionResult<IEnumerable<View>>> GetViews()
        {
          if (_context.Views == null)
          {
              return NotFound();
          }
            return await _context.Views.ToListAsync();
        }

        // GET: api/Views/5
        [HttpGet("{id}")]
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

        // PUT: api/Views/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
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

        // POST: api/Views
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<View>> PostView(View view)
        {
          if (_context.Views == null)
          {
              return Problem("Entity set 'StarplexContext.Views'  is null.");
          }
            _context.Views.Add(view);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetView", new { id = view.ViewId }, view);
        }

        // DELETE: api/Views/5
        [HttpDelete("{id}")]
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
