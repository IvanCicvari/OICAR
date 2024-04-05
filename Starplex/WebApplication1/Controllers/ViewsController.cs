using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Models;

namespace WEBAPI.Controllers
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

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var views = _context.Views.ToList();
                if (views.Count == 0)
                {
                    return NotFound("Views are not available.");
                }
                return Ok(views);
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
                var view = _context.Views.Find(id);
                if (view == null)
                {
                    return NotFound($"View with id {id} is not found.");
                }
                return Ok(view);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public IActionResult Post(View view)
        {
            try
            {
                _context.Add(view);
                _context.SaveChanges();
                return Ok("View created.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(View view)
        {
            if (view == null || view.ViewId == 0)
            {
                if (view == null)
                {
                    return BadRequest("View data is invalid");
                }
                else if (view.ViewId == 0)
                {
                    return BadRequest($"View id {view.ViewId} is invalid.");
                }
            }

            try
            {
                var viewModel = _context.Views.Find(view.ViewId);
                if (viewModel == null)
                {
                    return NotFound($"View with id {view.ViewId} not found.");
                }

                viewModel.UserId = view.UserId;
                viewModel.VideoId = view.VideoId;
                viewModel.ViewDate = view.ViewDate;

                _context.SaveChanges();
                return Ok(viewModel);
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
                var view = _context.Views.Find(id);
                if (view == null)
                {
                    return NotFound($"View with id {id} not found.");
                }
                _context.Views.Remove(view);
                _context.SaveChanges();
                return Ok($"View with id {id} deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
