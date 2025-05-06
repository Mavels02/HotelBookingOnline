using API_HotelBooking.Models;

namespace API_HotelBooking.Service
{
    public interface IDatDichVuService
    {
        Task<IEnumerable<DatDichVu>> GetAllAsync();
        Task<DatDichVu?> GetByIdAsync(int id);
        Task<DatDichVu> CreateAsync(DatDichVu entity);
        Task<bool> UpdateAsync(DatDichVu entity);
        Task<bool> DeleteAsync(int id);
    }
}