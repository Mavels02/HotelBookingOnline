using Microsoft.AspNetCore.Mvc;
using MVC_HotelBooking.ViewModel;
using Newtonsoft.Json;

namespace MVC_HotelBooking.Controllers
{
    public class ThanhToanController : Controller
    {
        private readonly HttpClient _httpClient;

        public ThanhToanController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7077/api/");
        }
       

       
        public IActionResult DaThanhToan() {
            return View();
        }
    }
}
