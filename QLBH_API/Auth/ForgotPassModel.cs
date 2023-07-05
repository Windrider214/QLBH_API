using System.ComponentModel.DataAnnotations;

namespace QLBH_API.Auth
{
    public class ForgotPassModel
    {
        [Required, DataType(DataType.Password), Display(Name = "Mật khẩu mới")]
        public string? Password { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Xác nhận mật khẩu mới")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu chưa đúng !!!")]
        public string? ConfirmNewPassword { get; set; }

        [Required]
        public string? token { get; set; }

        [Required]
        public string? email { get; set; }
    }
}
