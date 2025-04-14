using Microsoft.AspNetCore.Mvc;

namespace MVC_HotelBooking.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
