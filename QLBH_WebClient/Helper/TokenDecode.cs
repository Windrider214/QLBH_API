using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace QLBH_WebClient.Helper
{
    public class TokenDecode
    {
        public static string GetID(string tokenString)
        {
            var token = new JwtSecurityToken(jwtEncodedString: tokenString);
            string id = token.Claims.First(c => c.Type == "UserID").Value;
            return id;
        }
    }
}