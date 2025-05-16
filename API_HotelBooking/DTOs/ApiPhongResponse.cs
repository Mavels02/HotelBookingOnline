

namespace API_HotelBooking.DTOs
{
	public class ApiPhongResponse

	{
		public IEnumerable<PhongViewModel> Rooms { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
	}
}
