using API_HotelBooking.Models;

namespace API_HotelBooking.Service
{
    public interface IPhongService
    {
        Task<IEnumerable<Phong>> GetAllAsync();
        Task<Phong?> GetByIdAsync(int id);
        Task<Phong> CreateAsync(Phong phong);
        Task<bool> UpdateAsync(int id, Phong phong);
        Task<bool> DeleteAsync(int id);
    }
}
