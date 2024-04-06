using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Starplex_Fronted.Models;
using System.Text;
using API.Models;


namespace Starplex_Fronted.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IHttpClientFactory httpClientFactory, ILogger<LoginController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var token = await AuthenticateUserAsync(model.Username, model.Password);
                if (!string.IsNullOrEmpty(token))
                {
                    // Save token to session or cookie if needed
                    HttpContext.Session.SetString("AuthToken", token); // Saving token to session
                    return RedirectToAction("Index", "Home"); // Redirect to index page upon successful login
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                // Log error and return error view
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View(model);
            }
        }


        private async Task<string> AuthenticateUserAsync(string username, string password)
        {
            var loginRequest = new
            {
                Username = username,
                Password = password
            };

            var client = _httpClientFactory.CreateClient();
            var jsonContent = JsonConvert.SerializeObject(loginRequest);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:5022/api/Users/Login", content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return null;
            }
        }

    }
}

