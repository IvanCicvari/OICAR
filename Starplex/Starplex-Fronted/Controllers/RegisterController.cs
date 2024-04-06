using API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Starplex_Fronted.Models;
using System.Text;

namespace Starplex_Fronted.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<LoginController> _logger;

        public RegisterController(IHttpClientFactory httpClientFactory, ILogger<LoginController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = await CreateUserAsync(model);
                if (response.IsSuccessStatusCode)
                {
                    // User created successfully, redirect to appropriate page or show success message
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Handle failure response from API
                    ModelState.AddModelError(string.Empty, "Failed to create user. Please try again later.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                // Log error and return error view
                _logger.LogError(ex, "An error occurred while creating user");
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View(model);
            }
        }

        private async Task<HttpResponseMessage> CreateUserAsync(CreateViewModel model)
        {
            var client = _httpClientFactory.CreateClient();

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                Email = model.Email,
                Bio = model.Bio,
                Password = model.Password

            };

            var jsonContent = JsonConvert.SerializeObject(user);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:5022/api/Users/CreateUser", content);
            response.EnsureSuccessStatusCode(); // Throws exception if response is not successful
            return response;
        }
    }
}
