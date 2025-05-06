namespace API_HotelBooking.ViewModels
{
    public class ThanhToanViewModel
    {
        public int MaDP { get; set; }

        public string TenPhong { get; set; }
        public string TenLoaiPhong { get; set; }

        public string TenNguoiDung { get; set; }

        public decimal GiaPhong { get; set; }
        public string? TenKhuyenMai { get; set; }
        public int? PhanTramGiam { get; set; }

        public DateTime ThoiGianCheckIn { get; set; }
        public DateTime ThoiGianCheckOut { get; set; }

        public int SoNguoi { get; set; }

        public decimal TongTien { get; set; }
    }
}