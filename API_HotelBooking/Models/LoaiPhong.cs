using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace API_HotelBooking.Models
{
    public class LoaiPhong
    {
        [Key]
        public int MaLP { get; set; }
        public string LoaiPhongName { get; set; }
<<<<<<< HEAD

        public ICollection<Phong>? Phongs { get; set; }
=======
		[JsonIgnore]
		public ICollection<Phong> Phongs { get; set; }
>>>>>>> 77cef63b639585e4ccc394b4dd4fe8bd8701ebbd
    }
}
