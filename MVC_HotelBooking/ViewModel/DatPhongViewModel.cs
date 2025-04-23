namespace MVC_HotelBooking.ViewModel
{
	public class DatPhongViewModel
	{
		public int MaND { get; set; }
		public int MaP { get; set; }
		public string TenPhong {  get; set; }
		public int? MaKM { get; set; }
		public DateTime ThoiGianCheckIn { get; set; }
		public DateTime ThoiGianCheckOut { get; set; }
		public int SoNguoi { get; set; }
	}
}
