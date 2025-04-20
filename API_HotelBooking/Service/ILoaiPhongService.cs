using API_HotelBooking.Models;

namespace API_HotelBooking.Service
{
	public interface ILoaiPhongService
	{
		Task<IEnumerable<LoaiPhong>> GetAllAsync();
	}
}
