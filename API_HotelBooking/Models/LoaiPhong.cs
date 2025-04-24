using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace API_HotelBooking.Models
{
    public class LoaiPhong
    {
        [Key]
        public int MaLP { get; set; }
        public string LoaiPhongName { get; set; }

        public ICollection<Phong>? Phongs { get; set; }
    }
}
