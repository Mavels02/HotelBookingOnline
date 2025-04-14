using Microsoft.AspNetCore.Mvc;

namespace MVC_HotelBooking.Controllers
{
    public class KhuyenMaiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
