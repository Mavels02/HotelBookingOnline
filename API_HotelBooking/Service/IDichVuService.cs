using API_HotelBooking.Models;

namespace API_HotelBooking.Service
{
    public interface IDichVuService
    {
        Task<IEnumerable<DichVu>> GetAllAsync();
        Task<DichVu> GetByIdAsync(int id);
        Task<DichVu> AddAsync(DichVu dichVu);
        Task UpdateAsync(DichVu dichVu);
        Task DeleteAsync(int id);
    }
}
