using Microsoft.AspNetCore.Mvc;
using MVC_HotelBooking.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq.Dynamic.Core;
using System.Text.Json;

namespace MVC_HotelBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly HttpClient _httpClient;

		
		public HomeController(ILogger<HomeController> logger, HttpClient httpClient, IHttpClientFactory factory)
		{
			_logger = logger;
			_httpClient = httpClient;
			_httpClient = factory.CreateClient();
			_httpClient.BaseAddress = new Uri("http://localhost:40841/api"); //
		}

		public IActionResult Index()
        {
            return View();
        }
		public async Task<IActionResult> Index(int page = 1, int pageSize = 6)
		{
			var response = await _httpClient.GetAsync($"api/Phong?page={page}&pageSize={pageSize}");

			if (!response.IsSuccessStatusCode)
			{
				return View("Error");
			}

			var json = await response.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<PagedResult<Phong>>(json);

			return View(result);
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
