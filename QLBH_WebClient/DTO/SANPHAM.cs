using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_WebClient.DTO
{
    public class SANPHAM
    {
        public string maSp { get; set; }
        public string tenSp { get; set; }
        public string donViTinh { get; set; }
        public int donGia { get; set; }
        public string maLoai { get; set; }
        public string maTh { get; set; }
        public string seoImage { get; set; }
        public string image { get; set; }
        public string moTa { get; set; }
        public string chiTietSp { get; set; } 
        public int soLanXem { get; set; }
        public int giamGia { get; set; }
        public int soLuong { get; set; }
        public bool tinhTrang { get; set; }
        public DateTime ngayThem { get; set; }  

        public LOAISP maLoaiNavigation { get; set; }
        public THUONGHIEU maThNavigation { get; set; }
    }
}