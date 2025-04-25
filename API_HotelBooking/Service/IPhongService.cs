using API_HotelBooking.Models;
using API_HotelBooking.ViewModels;

namespace API_HotelBooking.Service
{
    public interface IPhongService
    {
		Task<IEnumerable<PhongViewModel>> GetAllAsync();
		Task<PhongViewModel?> GetByIdAsync(int id);
		Task CreateAsync(PhongViewModel dto);
		Task<bool> UpdateAsync(int id, PhongViewModel dto);
		Task<bool> DeleteAsync(int id);
		Task<(List<Phong> Rooms, int TotalPages)> GetPagedRoomsAsync(
	   int page, int pageSize,
	   int? loaiPhong = null, decimal? min = null,
	   decimal? max = null, string? status = null);
	
	}
}
