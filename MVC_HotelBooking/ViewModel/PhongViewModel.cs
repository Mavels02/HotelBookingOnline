namespace MVC_HotelBooking.ViewModel
{
	public class PhongViewModel
	{
		public int MaP { get; set; }
		public string TenPhong { get; set; }
		public decimal GiaPhong { get; set; }
		public string TrangThai { get; set; }
		public string ImageUrl { get; set; }
		public int SoLuongNguoiToiDa { get; set; }
		public DateTime? CheckInDate { get; set; }
		public DateTime? CheckOutDate { get; set; }
		public int? SoNguoi { get; set; }
	}
}
