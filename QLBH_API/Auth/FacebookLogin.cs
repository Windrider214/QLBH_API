namespace QLBH_API.Auth
{
    public class FacebookLogin
    {
        public class FacebookLoginModel
        {
            public string? token { get; set; }
            public string? username { get; set; }
            public string? userid { get; set; }
        }   

        public class FacebookUserViewModel
        {
            public string? id { get; set; }
            public string? first_name { get; set; }
            public string? last_name { get; set; }
            public string? username { get; set; }
            public string? email { get; set; }
        }
    }
}
