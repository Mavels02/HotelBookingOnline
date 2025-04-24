namespace MVC_HotelBooking.Models
{
	public class DatPhong
	{

		public int MaDP { get; set; }
		public int MaND { get; set; }
		public int MaP { get; set; }
		public DateTime ThoiGianCheckIn { get; set; }
		public DateTime ThoiGianCheckOut { get; set; }
		public int SoNguoi { get; set; }
		public decimal TongTien { get; set; }
		public string TrangThai { get; set; }

	}
}
