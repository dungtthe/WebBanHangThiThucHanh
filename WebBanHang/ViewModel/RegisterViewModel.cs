using System.ComponentModel.DataAnnotations;

namespace WebBanHang.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Tên không được để trống")]
        [MaxLength(500)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Tên tài khoản không được để trống")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Tên tài khoản phải có từ 6 đến 30 ký tự")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có từ 6 đến 100 kí tự")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Mật khẩu chỉ được chứa ký tự chữ và số")]
        public string? Password { get; set; }

        [Required(ErrorMessage ="Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [MaxLength(100)]
        public string? Email { get; set; }
    }
}
