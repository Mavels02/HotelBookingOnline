using Microsoft.AspNetCore.Mvc;

namespace MVC_HotelBooking.Controllers
{
    public class DichVuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
