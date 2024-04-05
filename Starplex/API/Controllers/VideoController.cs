using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly StarplexContext _context;

        public VideoController(StarplexContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var videos = _context.Videos.ToList();
                if (videos.Count == 0)
                {
                    return NotFound("Videos are not available.");
                }
                return Ok(videos);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var video = _context.Videos.Find(id);
                if (video == null)
                {
                    return NotFound($"Video with id {id} is not found.");
                }
                return Ok(video);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public IActionResult Post(Video video)
        {
            try
            {
                _context.Add(video);
                _context.SaveChanges();
                return Ok("Video created.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(Video video)
        {
            if (video == null || video.Idvideo == 0)
            {
                if (video == null)
                {
                    return BadRequest("Video data is invalid");
                }
                else if (video.Idvideo == 0)
                {
                    return BadRequest($"Video id {video.Idvideo} is invalid.");
                }
            }

            try
            {
                var videoModel = _context.Videos.Find(video.Idvideo);
                if ( videoModel == null )
                {
                    return NotFound($"Video with id {video.Idvideo} not found.");
                }
                videoModel.UserId = video.UserId;
                videoModel.Title = video.Title;
                videoModel.Description = video.Description;
                videoModel.UploadDate = video.UploadDate;
                videoModel.ThumbnailUrl = video.ThumbnailUrl;
                videoModel.VideoUrl = video.VideoUrl;
                videoModel.Duration = video.Duration;
                videoModel.Categories = video.Categories;
                videoModel.PrivacySetting = video.PrivacySetting;
                videoModel.TotalLikes = video.TotalLikes;
                videoModel.TotalViews = video.TotalViews;
                videoModel.TotalSubscribers = video.TotalSubscribers;
                _context.SaveChanges();
                return Ok(videoModel);
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
                var video = _context.Videos.Find(id);
                if (video == null)
                {
                    return NotFound($"Video with id {id} not found.");
                }
                _context.Videos.Remove(video);
                _context.SaveChanges();
                return Ok($"Video with id {id} deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
