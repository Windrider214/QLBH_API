using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace QLBH_WebClient.Models
{
    public class ChangePasswordModel
    {
        [Required]
        public string userID { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Mật khẩu cũ")]
        public string CurrentPassword { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Xác nhận mật khẩu mới")]
        [Compare("NewPassword", ErrorMessage = "Xác nhận mật khẩu chưa đúng !!!")]
        public string ConfirmNewPassword { get; set; }
    }
}
