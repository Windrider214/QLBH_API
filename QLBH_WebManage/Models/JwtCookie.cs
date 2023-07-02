using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_WebManage.Models
{
    public class JwtCookie
    {
        public string id { get; set; }
        public string token { get; set; }
        public string refreshToken { get; set; }
        public DateTime? expiration { get; set; }
}
}