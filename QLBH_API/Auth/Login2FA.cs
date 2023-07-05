using QLBH_DataAccess.Models;

namespace QLBH_API.Auth
{
    public class Login2FA
    {
        public string userName { get; set; }
        public string confirmCODE { get; set; }
    }
}
