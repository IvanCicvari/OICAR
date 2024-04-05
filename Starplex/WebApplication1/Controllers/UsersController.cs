using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly StarplexContext _context;

        public UsersController(StarplexContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var users = _context.Users.ToList();
                if (users.Count == 0)
                {
                    return NotFound("Users are not available.");
                }
                return Ok(users);
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
                var user = _context.Users.Find(id);
                if (user == null)
                {
                    return NotFound($"User with id {id} is not found.");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public IActionResult Post(User user)
        {
            try
            {
                _context.Add(user);
                _context.SaveChanges();
                return Ok("User created.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(User user)
        {
            if (user == null || user.Iduser == 0)
            {
                if (user == null)
                {
                    return BadRequest("User data is invalid");
                }
                else if (user.Iduser == 0)
                {
                    return BadRequest($"User id {user.Iduser} is invalid.");
                }
            }

            try
            {
                var userModel = _context.Users.Find(user.Iduser);
                if (userModel == null)
                {
                    return NotFound($"User with id {user.Iduser} not found.");
                }

                userModel.FirstName = user.FirstName;
                userModel.LastName = user.LastName;
                userModel.Username = user.Username;
                userModel.Email = user.Email;
                userModel.PasswordHash = user.PasswordHash;
                userModel.PasswordSalt = user.PasswordSalt;
                userModel.Password = user.Password;
                userModel.CreatedAt = user.CreatedAt;
                userModel.LastLogin = user.LastLogin;
                userModel.ProfileImage = user.ProfileImage;
                userModel.Bio = user.Bio;
                userModel.IsVerified = user.IsVerified;
                userModel.SubscriptionStatus = user.SubscriptionStatus;

                _context.SaveChanges();
                return Ok(userModel);
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
                var user = _context.Users.Find(id);
                if (user == null)
                {
                    return NotFound($"User with id {id} not found.");
                }
                _context.Users.Remove(user);
                _context.SaveChanges();
                return Ok($"User with id {id} deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
