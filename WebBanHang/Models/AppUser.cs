using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanHang.Models
{
    [Table("AppUsers")]
    public class AppUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string ?Name { get; set; }

        [Required(ErrorMessage="Tên tài khoản không được để trống")]
        [Column(TypeName = "varchar")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Tên tài khoản phải có từ 6 đến 30 ký tự")]
        public string ?UserName {  get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [Column(TypeName = "varchar")]
        [StringLength(100,MinimumLength = 6, ErrorMessage = "Password phải có từ 6 đến 100 kí tự")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Mật khẩu chỉ được chứa ký tự chữ và số")]
        public string ? Password { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        public string ? Email {  get; set; }

        [Phone]
        [MaxLength(30)]
        public string? PhoneNumber {  get; set; }

        [Required]
        [DisplayName("Trạng thái")]
        public bool IsLock {  get; set; }

        [Required]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        [Required]
        public Role? Role { get; set; }
    }
}
