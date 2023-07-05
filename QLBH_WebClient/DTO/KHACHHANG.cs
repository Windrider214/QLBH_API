using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_WebClient.DTO
{
    public class KHACHHANG
    {
        public KHACHHANG()
        {
            hoadons = new HashSet<HOADON>();
            phanhois = new HashSet<PHANHOI>();
        }

        public string maKh { get; set; } 
        public string tenKh { get; set; }
        public string sdt { get; set; }
        public string diaChi { get; set; }
        public string loginId { get; set; }
        public string emailKh { get; set; }

        public virtual USER Login { get; set; }
        public virtual ICollection<HOADON> hoadons { get; set; }
        public virtual ICollection<PHANHOI> phanhois { get; set; }
    }
}