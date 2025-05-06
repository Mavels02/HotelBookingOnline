using API_HotelBooking.Models;

namespace API_HotelBooking.Service
{
    public interface IDichVuService
    {
        Task<IEnumerable<DichVu>> GetAllAsync();
        Task<DichVu> GetByIdAsync(int id);
        Task<DichVu> CreateAsync(DichVu dichVu);
        Task<bool> UpdateAsync(int id, DichVu dichVu);
        Task<bool> DeleteAsync(int id);
    }
}