using QLBH_WebClient.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_WebClient.Models
{
    public class GHN
    {
        public int payment_type_id { get; set; } = 1;
        public string note { get; set; }
        public string from_name { get; set; } = "ZAY SHOP";
        public string from_phone { get; set; } = "0981617791";
        public string from_address { get; set; } = "Số 6 ngõ Bảo Khánh";
        public string from_ward_name { get; set; } = "Phường Hàng Trống";
        public string from_district_name { get; set; } = "Quận Hoàn Kiếm";
        public string from_province_name { get; set; } = "Hà Nội";
        public string required_note { get; set; } = "CHOXEMHANGKHONGTHU";
        public string return_name { get; set; } = "ZAY SHOP";
        public string return_phone { get; set; } = "0981617791";
        public string return_address { get; set; } = "Số 6 ngõ Bảo Khánh";
        public string return_ward_name { get; set; } = "Phường Hàng Trống";
        public string return_district_name { get; set; } = "Quận Hoàn Kiếm";
        public string return_province_name { get; set; } = "Hà Nội";
        public string client_order_code { get; set; } = "GHN - " + Guid.NewGuid().ToString();
        public string to_name { get; set; }
        public string to_phone { get; set; }
        public string to_address { get; set; }
        public string to_ward_code { get; set; }
        public int to_district_id { get; set; }
        public int to_province_id { get; set; }
        public int cod_amount { get; set; } // Tiền hàng
        public int weight { get; set; }
        public int cod_failed_amount { get; set; } = 1000;
        public int pick_station_id { get; set; } = 1444;
        public string deliver_station_id { get; set; } = null;
        public int insurance_value { get; set; } // Tiền hàng
        public int service_id { get; set; } = 53320;
        public string coupon { get; set; } = null;
        public List<Item> items { get; set; }

       public class Item
       {
            public string name { get; set; }
            public int quantity { get; set; }
            public int price { get; set; }
       }
        
    }
}