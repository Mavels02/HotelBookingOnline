using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_HotelBooking.Models
{
    public class DatDichVu
    {
        [Key]
        public int MaDDV { get; set; }

        public int MaDV { get; set; }

        [ForeignKey("MaDV")]
        public DichVu? DichVu { get; set; }  // API sẽ tự động tìm và gán đối tượng DichVu

        public int? MaDP { get; set; }

        [ForeignKey("MaDP")]
        public DatPhong? DatPhong { get; set; }

        public int SoLuong { get; set; }
        public decimal TongTien { get; set; }
        public DateTime Ngay { get; set; }
    }
}