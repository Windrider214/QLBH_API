using Microsoft.AspNetCore.Identity;

namespace QLBH_API.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }  
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
