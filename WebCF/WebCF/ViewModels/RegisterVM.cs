﻿using System.ComponentModel.DataAnnotations;

namespace WebCF.ViewModels
{
	public class RegisterVM
	{
		[Key]
		[Display(Name = "Tên đăng nhập")]
		[Required(ErrorMessage = "*")]
		[MaxLength(20, ErrorMessage = "Tối đa 20 kí tự")]
		public string MaKh { get; set; }


		[Display(Name ="Mật khẩu")]
		[Required(ErrorMessage = "*")]
		[DataType(DataType.Password)]
		public string MatKhau { get; set; }

		[Display(Name ="Họ tên")]
		[Required(ErrorMessage = "*")]
		[MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
		public string HoTen { get; set; }

		public bool GioiTinh { get; set; } = true;

		[Display(Name ="Ngày sinh")]
		[DataType(DataType.Date)]
		public DateTime? NgaySinh { get; set; }

		[Display(Name ="Địa chỉ")]
		[MaxLength(60, ErrorMessage = "Tối đa 60 kí tự")]
		public string DiaChi { get; set; }

		[Display(Name = "Điện thoại")]
		[StringLength(10, MinimumLength = 10, ErrorMessage = "Điện thoại phải có đúng 10 kí tự")]
		public string DienThoai { get; set; }


		[EmailAddress(ErrorMessage ="Chưa đúng định dạng email")]
		public string Email { get; set; }

		public string? Hinh { get; set; }
	}
}
