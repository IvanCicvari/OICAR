using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Starplex_Frontend.Models;
using System.Text;

namespace Starplex_Frontend.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IHttpClientFactory httpClientFactory, ILogger<AccountController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var token = await AuthenticateUserAsync(model.Username, model.Password);
                HttpContext.Session.SetString("AuthToken", token);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login");
                ModelState.AddModelError(string.Empty, "An error occurred. Please try again later.");
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
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}

