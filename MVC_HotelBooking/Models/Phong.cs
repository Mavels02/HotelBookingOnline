<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
=======
﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
>>>>>>> 77cef63b639585e4ccc394b4dd4fe8bd8701ebbd

namespace MVC_HotelBooking.Models
{
    public class Phong
    {
        public int MaP { get; set; }

        [Display(Name = "Loại phòng")]
        [Required(ErrorMessage = "Vui lòng chọn loại phòng")]
        public int? MaLP { get; set; }

        [Display(Name = "Tên phòng")]
        [Required(ErrorMessage = "Vui lòng nhập tên phòng")]
        [StringLength(100)]
        public string? TenPhong { get; set; }

        [Display(Name = "Giá phòng")]
        [Required(ErrorMessage = "Vui lòng nhập giá phòng")]
        public decimal GiaPhong { get; set; }

        [Display(Name = "Trạng thái")]
        public string? TrangThai { get; set; }

        [Display(Name = "Chọn ảnh")]
        public IFormFile? ImageFile { get; set; }

<<<<<<< HEAD
        public string? ImageUrl { get; set; }  // Trường hợp cần hiển thị lại ảnh khi sửa

        public string? LoaiPhongName { get; set; }  // Để nhận tên loại phòng từ API nếu cần

        // Dùng cho dropdown list
        public List<SelectListItem>? LoaiPhongs { get; set; }
    }
}
=======
		public LoaiPhong? LoaiPhong { get; set; }
	}
   

}
>>>>>>> 77cef63b639585e4ccc394b4dd4fe8bd8701ebbd
