using Microsoft.AspNetCore.Mvc;

namespace MVC_HotelBooking.Controllers
{
    public class DanhGiaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
