using API_HotelBooking.Models;
using API_HotelBooking.DTOs;

namespace API_HotelBooking.Service
{
    public interface IKhuyenMaiService
    {
        Task<IEnumerable<KhuyenMaiViewModel>> GetAllAsync();
        Task<KhuyenMaiViewModel> GetByIdAsync(int id);
        Task<KhuyenMaiViewModel> CreateAsync(KhuyenMaiViewModel dto);
        Task<bool> UpdateAsync(int id, KhuyenMaiViewModel dto);
        Task<bool> DeleteAsync(int id);
    }
}