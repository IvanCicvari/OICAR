using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly StarplexContext _context;
        private IConfiguration _config;

        public UsersController(StarplexContext context, IConfiguration config)
        {
            _context = context;
            _config = config;

        }

        // GET: api/Users
        [HttpGet("GetUsers")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateUser/{id}")]
        [Authorize]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Iduser)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("CreateUser")]
        [Authorize]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'StarplexContext.Users' is null.");
            }

            byte[] salt = GenerateSalt();

            string hashedPassword = HashPassword(user.Password, salt);

            // Save hashed password and salt to user object
            user.PasswordHash = hashedPassword;
            user.PasswordSalt = Convert.ToBase64String(salt);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Iduser }, user);
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private string HashPassword(string password, byte[] salt)
        {
            using (var hmac = new HMACSHA512(salt))
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedPasswordBytes = hmac.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashedPasswordBytes);
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("DeleteUser/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Iduser == id);
        }

    
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(User loginRequest)
        {
            IActionResult response = Unauthorized();

            // Authenticate the user
            var user = AuthenticateUser(loginRequest.Username, loginRequest.Password);

            if (user != null)
            {
                // Generate JWT token
                var token = GenerateTokens(user);
                response = Ok(new { token });
            }

            return response;
        }

        private User AuthenticateUser(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            return user;
        }

        private string GenerateTokens(User user)
        {
            // Generate JWT token using user claims
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null, expires: DateTime.Now.AddDays(1), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}