using API_HotelBooking.Models;
using API_HotelBooking.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_HotelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThanhToanController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public ThanhToanController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet("{maDP}")]
        public async Task<IActionResult> GetByMaDatPhong(int maDP)
        {

            var datPhong = await _appDbContext.DatPhongs
                .Include(dp => dp.Phong).ThenInclude(p => p.LoaiPhong)
                .Include(dp => dp.NguoiDung)
                .Include(dp => dp.KhuyenMai)
                .FirstOrDefaultAsync(dp => dp.MaDP == maDP);

            if (datPhong == null)
                return NotFound();

            var model = new ThanhToanViewModel
            {
                MaDP = datPhong.MaDP,
                TenPhong = datPhong.Phong.TenPhong,
                TenLoaiPhong = datPhong.Phong.LoaiPhong.TenLoai,
                TenNguoiDung = datPhong.NguoiDung.Ten,
                ThoiGianCheckIn = datPhong.ThoiGianCheckIn,
                ThoiGianCheckOut = datPhong.ThoiGianCheckOut,
                SoNguoi = datPhong.SoNguoi,
                TenKhuyenMai = datPhong.KhuyenMai?.TenKM,

                TongTien = datPhong.TongTien

            };
            datPhong.TrangThai = "Đã thanh toán";
            return Ok(model);
        }
    }
}