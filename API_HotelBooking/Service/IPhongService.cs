using API_HotelBooking.Models;

namespace API_HotelBooking.Service
{
    public interface IPhongService
    {
        Task<IEnumerable<Phong>> GetAllAsync();
        Task<Phong?> GetByIdAsync(int id);
        Task<Phong> CreateAsync(Phong model);
        Task<bool> UpdateAsync(int id, Phong model);
        Task<bool> DeleteAsync(int id);
		Task<(List<Phong> Rooms, int TotalPages)> GetPagedRoomsAsync(
	   int page, int pageSize,
	   int? loaiPhong = null, decimal? min = null,
	   decimal? max = null, string? status = null);
	}
}
