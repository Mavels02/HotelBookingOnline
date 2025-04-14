using Microsoft.AspNetCore.Mvc;

namespace MVC_HotelBooking.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
