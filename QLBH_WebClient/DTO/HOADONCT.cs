using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_WebClient.DTO
{
    public class HOADONCT
    {
        public string maHd { get; set; }
        public string maSp { get; set; }
        public int donGiaBan { get; set; }
        public int soLuong { get; set; }
        public int giamGia { get; set; }

        public virtual HOADON maHdNavigation { get; set; }
        public virtual SANPHAM MaSpNavigation { get; set; }
    }
}