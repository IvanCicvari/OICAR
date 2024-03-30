using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly StarplexContext _context;

        public CommentsController(StarplexContext context)
        {
            _context = context;
        }

        // GET: api/Comments/GetComments
        [HttpGet("GetComments")]
        [Authorize] // Requires authorization to access
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            return await _context.Comments.ToListAsync();
        }

        // GET: api/Comments/GetComment/5
        [HttpGet("GetComment/{id}")]
        [AllowAnonymous] // Allows anonymous access
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/Comments/UpdateComment/5
        [HttpPut("UpdateComment/{id}")]
        [Authorize] // Requires authorization to access
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.CommentId)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/Comments/CreateComment
        [HttpPost("CreateComment")]
        [Authorize] // Requires authorization to access
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            if (_context.Comments == null)
            {
                return Problem("Entity set 'StarplexContext.Comments' is null.");
            }
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.CommentId }, comment);
        }

        // DELETE: api/Comments/DeleteComment/5
        [HttpDelete("DeleteComment/{id}")]
        [Authorize] // Requires authorization to access
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(int id)
        {
            return (_context.Comments?.Any(e => e.CommentId == id)).GetValueOrDefault();
        }
    }
}
