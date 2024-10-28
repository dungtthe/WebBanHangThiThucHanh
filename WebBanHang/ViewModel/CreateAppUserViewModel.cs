using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebBanHang.ViewModel
{
    public class CreateAppUserViewModel
    {
        [Required(ErrorMessage = "Tên không được để trống")]
        [MaxLength(500)]
        [DisplayName("Tên")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Tên tài khoản không được để trống")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Tên tài khoản phải có từ 6 đến 30 ký tự")]
        [DisplayName("Tên tài khoản")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [MaxLength(100)]
        [DisplayName("Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [MaxLength(30)]
        [DisplayName("Số điện thoại")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có từ 6 đến 100 kí tự")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Mật khẩu chỉ được chứa ký tự chữ và số")]
        [DisplayName("Mật khẩu")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Quyền không được để trống")]
        [DisplayName("Vai trò")]
        public int RoleId { get; set; }
    }
}
