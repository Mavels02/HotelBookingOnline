using API_HotelBooking.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_HotelBooking.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoaiPhongController : ControllerBase
	{
		private readonly ILoaiPhongService _loaiPhongService;

		public LoaiPhongController(ILoaiPhongService loaiPhongService)
		{
			_loaiPhongService = loaiPhongService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var data = await _loaiPhongService.GetAllAsync();
			return Ok(data);
		}



	}
}
