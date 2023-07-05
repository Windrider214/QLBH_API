using System.ComponentModel.DataAnnotations;

namespace QLBH_API.Auth
{
    public class En2FA
    {
        [Required]
        public string? userID { get; set; }
        [Required]
        public bool enable { get; set; }
    }
}
