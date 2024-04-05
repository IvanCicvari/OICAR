using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;
using System.Security.Policy;

namespace Admin.Controllers
{
    public class VideoController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5022/api");
        private readonly HttpClient _client;

        public VideoController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<VideoViewModel> videoList = new List<VideoViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Video/Get").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                videoList = JsonConvert.DeserializeObject<List<VideoViewModel>>(data);
            }

            return View(videoList);
        }  

    }
}
