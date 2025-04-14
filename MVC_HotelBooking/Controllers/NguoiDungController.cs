using Microsoft.AspNetCore.Mvc;

namespace MVC_HotelBooking.Controllers
{
    public class NguoiDungController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
