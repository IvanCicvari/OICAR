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
    public class LikesDislikesController : ControllerBase
    {
        private readonly StarplexContext _context;

        public LikesDislikesController(StarplexContext context)
        {
            _context = context;
        }

        // GET: api/LikesDislikes/GetLikesDislikes
        [HttpGet("GetLikesDislikes")]
        public async Task<ActionResult<IEnumerable<LikesDislike>>> GetLikesDislikes()
        {
            if (_context.LikesDislikes == null)
            {
                return NotFound();
            }
            return await _context.LikesDislikes.ToListAsync();
        }

        // GET: api/LikesDislikes/GetLikesDislike/5
        [HttpGet("GetLikesDislike/{id}")]
        public async Task<ActionResult<LikesDislike>> GetLikesDislike(int id)
        {
            if (_context.LikesDislikes == null)
            {
                return NotFound();
            }
            var likesDislike = await _context.LikesDislikes.FindAsync(id);

            if (likesDislike == null)
            {
                return NotFound();
            }

            return likesDislike;
        }

        // PUT: api/LikesDislikes/UpdateLikesDislike/5
        [HttpPut("UpdateLikesDislike/{id}")]
        public async Task<IActionResult> PutLikesDislike(int id, LikesDislike likesDislike)
        {
            if (id != likesDislike.LikeId)
            {
                return BadRequest();
            }

            _context.Entry(likesDislike).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LikesDislikeExists(id))
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

        // POST: api/LikesDislikes/CreateLikesDislike
        [HttpPost("CreateLikesDislike")]
        public async Task<ActionResult<LikesDislike>> CreateLikesDislike(LikesDislike likesDislike)
        {
            if (_context.LikesDislikes == null)
            {
                return Problem("Entity set 'StarplexContext.LikesDislikes'  is null.");
            }
            _context.LikesDislikes.Add(likesDislike);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLikesDislike", new { id = likesDislike.LikeId }, likesDislike);
        }

        // DELETE: api/LikesDislikes/DeleteLikesDislike/5
        [HttpDelete("DeleteLikesDislike/{id}")]
        public async Task<IActionResult> DeleteLikesDislike(int id)
        {
            if (_context.LikesDislikes == null)
            {
                return NotFound();
            }
            var likesDislike = await _context.LikesDislikes.FindAsync(id);
            if (likesDislike == null)
            {
                return NotFound();
            }

            _context.LikesDislikes.Remove(likesDislike);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LikesDislikeExists(int id)
        {
            return (_context.LikesDislikes?.Any(e => e.LikeId == id)).GetValueOrDefault();
        }
    }
}
