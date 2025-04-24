using MVC_HotelBooking.ViewModel;

namespace MVC_HotelBooking.Models
{
	public class ApiPhongResponse
	{
		public List<Phong> Rooms { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
	}
}
