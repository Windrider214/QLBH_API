using System.ComponentModel.DataAnnotations;

namespace QLBH_WebClient.Models
{
    public class En2FA
    {
        [Required]
        public string userID { get; set; }
        [Required]
        public bool enable { get; set; }
    }
}
