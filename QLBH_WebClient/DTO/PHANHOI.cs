using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_WebClient.DTO
{
    public class PHANHOI
    {
        public string maPh { get; set; } 
        public string tieuDe { get; set; }
        public string noiDung { get; set; }
        public string phanHoi1 { get; set; }
        public string maKh { get; set; }
        public string tenKh { get; set; }
        public string email { get; set; }
        public DateTime? NgayGui { get; set; }
        public DateTime? NgayTraLoi { get; set; }
        public bool? TinhTrang { get; set; }

        public virtual KHACHHANG maKhNavigation { get; set; }
    }
}