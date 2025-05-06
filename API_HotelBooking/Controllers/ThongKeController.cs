using API_HotelBooking.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_HotelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {
        private readonly IThongKeService _thongKeService;

        public ThongKeController(IThongKeService thongKeService)
        {
            _thongKeService = thongKeService;
        }

        [HttpGet("doanhthu-tong")]
        public async Task<ActionResult<decimal>> GetDoanhThu()
        {
            var doanhThu = await _thongKeService.ThongKeDoanhThuAsync();
            
            return Ok(doanhThu);
        }

        [HttpGet("doanhthu-ngay")]
        public async Task<ActionResult> ThongKeTheoNgay()
        {
            var data = await _thongKeService.ThongKeDoanhThuTheoNgayAsync();
            return Ok(data);
        }
        [HttpGet("doanhthu-thang")]
        public async Task<ActionResult> ThongKeTheoThang()
        {
            var data = await _thongKeService.ThongKeDoanhThuTheoThangAsync();
            return Ok(data);
        }
        [HttpGet("doanhthu-nam")]
        public async Task<ActionResult> ThongKeTheoNam()
        {
            var data = await _thongKeService.ThongKeDoanhThuTheoNamAsync();
            return Ok(data);
        }
    }
}
