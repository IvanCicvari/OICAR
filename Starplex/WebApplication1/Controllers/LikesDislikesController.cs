using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LikesDislikesController : ControllerBase
    {
        private readonly StarplexContext _context;

        public LikesDislikesController(StarplexContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var likes = _context.LikesDislikes.ToList();
                if (likes.Count == 0)
                {
                    return NotFound("LikesDislikes are not available.");
                }
                return Ok(likes);
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
                var like = _context.LikesDislikes.Find(id);
                if (like == null)
                {
                    return NotFound($"LikeDislike with id {id} is not found.");
                }
                return Ok(like);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public IActionResult Post(LikesDislike likes)
        {
            try
            {
                _context.Add(likes);
                _context.SaveChanges();
                return Ok("LikeDislike created.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(LikesDislike like)
        {
            if (like == null || like.LikeId == 0)
            {
                if (like == null)
                {
                    return BadRequest("LikeDislike data is invalid");
                }
                else if (like.LikeId == 0)
                {
                    return BadRequest($"LikeDislike id {like.LikeId} is invalid.");
                }
            }

            try
            {
                var likeModel = _context.LikesDislikes.Find(like.LikeId);
                if (likeModel == null)
                {
                    return NotFound($"LikeDislike with id {like.LikeId} not found.");
                }

                likeModel.UserId = like.UserId;
                likeModel.VideoId = like.VideoId;
                likeModel.LikeStatus = like.LikeStatus;

                _context.SaveChanges();
                return Ok(likeModel);
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
                var like = _context.LikesDislikes.Find(id);
                if (like == null)
                {
                    return NotFound($"LikesDislikes with id {id} not found.");
                }
                _context.LikesDislikes.Remove(like);
                _context.SaveChanges();
                return Ok($"LikesDislikes with id {id} deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
