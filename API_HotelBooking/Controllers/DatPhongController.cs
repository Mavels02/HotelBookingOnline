using API_HotelBooking.Service;
using API_HotelBooking.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_HotelBooking.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DatPhongController : ControllerBase
	{
		private readonly IDatPhongService _service;
		public DatPhongController(IDatPhongService service)
		{
			_service = service;
		}
		[HttpPost("dat-phong")]
		public async Task<IActionResult> DatPhong([FromBody] DatPhongViewModel dto)
		{
			try
			{
				var result = await _service.DatPhongAsync(dto);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var list = await _service.GetAllAsync();
			return Ok(list);
		}
		[HttpGet("{maPhong}")]
		public async Task<IActionResult> GetThongTinDatPhong(int MaPhong)
		{
			try
			{
				var datPhongVM = await _service.GetThongTinDatPhongAsync(MaPhong);
				return Ok(datPhongVM);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> HuyDatPhong(int id)
		{
			var result = await _service.HuyDatPhongAsync(id);
			if (!result) return NotFound();
			return Ok(new { message = "Hủy thành công" });
		}
	}
}
	