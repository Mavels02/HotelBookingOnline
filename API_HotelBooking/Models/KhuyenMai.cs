using System.ComponentModel.DataAnnotations;

namespace API_HotelBooking.Models
{
    public class KhuyenMai
    {
        [Key]
        public int MaKM { get; set; }

        public string TenKM { get; set; }
        public float PhanTramKM { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string TrangThai { get; set; }

        public ICollection<DatPhong> DatPhongs { get; set; }
    }
}
