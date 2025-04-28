namespace MVC_HotelBooking.ViewModel
{
	public class HomeViewModel
	{
		public List<Models.Phong> Rooms { get; set; } = new();
		public List<Models.LoaiPhong> LoaiPhongs { get; set; } = new();
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }

		// Lọc
		public int? SelectedLoaiPhong { get; set; }
		public string? SelectedStatus { get; set; }
		public decimal? MinPrice { get; set; }
		public decimal? MaxPrice { get; set; }
		public string? SearchQuery { get; set; }

	}
}
