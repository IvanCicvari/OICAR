using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly StarplexContext _context;

        public CommentsController(StarplexContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var comments = _context.Comments.ToList();
                if (comments.Count == 0)
                {
                    return NotFound("Comments are not available.");
                }
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var comment = _context.Comments.Find(id);
                if (comment == null)
                {
                    return NotFound($"Comment with id {id} is not found.");
                }
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public IActionResult Post(Comment comment)
        {
            try
            {
                _context.Add(comment);
                _context.SaveChanges();
                return Ok("Comment created.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(Comment comment)
        {
            if (comment == null || comment.CommentId == 0)
            {
                if (comment == null)
                {
                    return BadRequest("Comment data is invalid");
                }
                else if (comment.CommentId == 0)
                {
                    return BadRequest($"Comment id {comment.CommentId} is invalid.");
                }
            }

            try
            {
                var commentModel = _context.Comments.Find(comment.CommentId);
                if (commentModel == null)
                {
                    return NotFound($"Comment with id {comment.CommentId} not found.");
                }

                commentModel.UserId = comment.UserId;
                commentModel.VideoId = comment.VideoId;
                commentModel.CommentText = comment.CommentText;
                commentModel.CommentDate = comment.CommentDate;

                _context.SaveChanges();
                return Ok(commentModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var comment = _context.Comments.Find(id);
                if (comment == null)
                {
                    return NotFound($"Comment with id {id} not found.");
                }
                _context.Comments.Remove(comment);
                _context.SaveChanges();
                return Ok($"Comment with id {id} deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
