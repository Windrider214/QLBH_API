using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace QLBH_WebClient.DTO
{
    public class USER
    {
        public USER () {
            tintucs = new HashSet<BLOG>();
            khachhangs = new HashSet<KHACHHANG>();
        }

        public string id { get; set; }
        public string userName { get; set; }
        public string normalizedUserName { get; set; }
        public string email { get; set; }
        public string normalizedEmail { get; set; }
        public bool emailConfirmed { get; set; }
        public string passwordHash { get; set; }
        public string securityStamp { get; set; }
        public string concurrencyStamp { get; set; }
        public string phoneNumber { get; set; }
        public bool phoneNumberConfirmed { get; set; }
        public bool twoFactorEnabled { get; set; }
        public DateTime? lockoutEnd { get; set; }
        public bool lockoutEnabled { get; set; }
        public int accessFailedCount { get; set; }
        public DateTime? exprDate { get; set; }
        public string refreshToken { get; set; }
        //public AspNetUserClaims aspNetUserClaims { get; set; }
        //public AspNetUserLogins aspNetUserLogins { get; set; }
        //public AspNetUserTokens aspNetUserTokens { get; set; }
        public ICollection<KHACHHANG> khachhangs { get; set; }
        public ICollection<BLOG> tintucs { get; set; }
        //public Roles roles { get; set; }
    }
}