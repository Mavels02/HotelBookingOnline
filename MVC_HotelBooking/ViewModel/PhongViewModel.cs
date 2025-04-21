namespace MVC_HotelBooking.ViewModel
{
	public class PhongViewModel
	{
		public int MaP { get; set; }
		public int MaLP { get; set; }
		public LoaiPhongViewModel LoaiPhong { get; set; }
		public string TenPhong { get; set; }
		public decimal GiaPhong { get; set; }
		public string TrangThai { get; set; }
		public string ImageUrl { get; set; }
	}
}
