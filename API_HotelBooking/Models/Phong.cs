using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_HotelBooking.Models
{
    public class Phong
    {
        [Key]
        public int MaP { get; set; }

        public int MaLP { get; set; }

        [ForeignKey("MaLP")]
        public LoaiPhong LoaiPhong { get; set; }

        public decimal GiaPhong { get; set; }
        public string TrangThai { get; set; }

        public ICollection<DatPhong> DatPhongs { get; set; }
    }
}
