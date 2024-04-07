using System.ComponentModel.DataAnnotations;

namespace WebCF.ViewModels
{
	public class LoginVM
	{
		[Display(Name = "Tên đăng nhập")]
		[Required(ErrorMessage ="Chưa nhập tên đăng nhập")]
		[MaxLength(20, ErrorMessage ="Tối đa 20 kí tự")]
		public string LoginName { get; set; }
		[Display(Name = "Mật Khẩu")]
		[Required(ErrorMessage = "Chưa nhập mật khẩu")]
		[MaxLength(20, ErrorMessage = "Tối đa 20 kí tự")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
