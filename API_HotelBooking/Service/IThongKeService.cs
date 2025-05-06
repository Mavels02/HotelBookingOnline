using API_HotelBooking.Models;

namespace API_HotelBooking.Service
{
    public interface IThongKeService
    {
        Task<TongDoanhThu> ThongKeDoanhThuAsync();
        Task<List<DoanhThuTheoNgay>> ThongKeDoanhThuTheoNgayAsync();
        Task<List<DoanhThuTheoThang>> ThongKeDoanhThuTheoThangAsync();
        Task<List<DoanhThuTheoNam>> ThongKeDoanhThuTheoNamAsync();
    }
}
