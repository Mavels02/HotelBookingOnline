using API_HotelBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace API_HotelBooking.Service
{
    public class ThongKeService : IThongKeService
    {
        private readonly AppDbContext _context;

        public ThongKeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TongDoanhThu> ThongKeDoanhThuAsync()
        {
            var tong = await _context.ThanhToans
                .Where(t => t.TrangThai == "Đã thanh toán")
                .SumAsync(t => t.TienThanhToan);

            return new TongDoanhThu { TongTien = tong };
        }
        public async Task<List<DoanhThuTheoNgay>> ThongKeDoanhThuTheoNgayAsync()
        {
            return await _context.ThanhToans
                .Where(t => t.TrangThai == "Đã thanh toán")
                .GroupBy(t => t.DatPhong.ThoiGianDat.Date)
                .Select(g => new DoanhThuTheoNgay
                {
                    Ngay = g.Key,
                    TongTien = g.Sum(x => x.TienThanhToan)
                })
                .OrderByDescending(x => x.Ngay)
                .ToListAsync();
        }
        public async Task<List<DoanhThuTheoThang>> ThongKeDoanhThuTheoThangAsync()
        {
            return await _context.ThanhToans
                .Where(t => t.TrangThai == "Đã thanh toán")
                .GroupBy(t => new { t.DatPhong.ThoiGianDat.Year, t.DatPhong.ThoiGianDat.Month })
                .Select(g => new DoanhThuTheoThang
                {
                    Nam = g.Key.Year,
                    Thang = g.Key.Month,
                    TongTien = g.Sum(x => x.TienThanhToan)
                })
                .OrderByDescending(x => x.Nam).ThenByDescending(x => x.Thang)
                .ToListAsync();
        }

        public async Task<List<DoanhThuTheoNam>> ThongKeDoanhThuTheoNamAsync()
        {
            return await _context.ThanhToans
                .Where(t => t.TrangThai == "Đã thanh toán")
                .GroupBy(t => t.DatPhong.ThoiGianDat.Year)
                .Select(g => new DoanhThuTheoNam
                {
                    Nam = g.Key,
                    TongTien = g.Sum(x => x.TienThanhToan)
                })
                .OrderByDescending(x => x.Nam)
                .ToListAsync();
        }
    }
}
