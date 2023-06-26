using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_WebClient.Models
{
    public class Cart
    {
        public string maSp { get; set; }
        public string tenSp { get; set; }
        public string donViTinh { get; set; }
        public int donGia { get; set; }
        public int soLuong { get; set; }
        public string seoImage { get; set; }
        //public double ThanhTien => soLuong * donGia;
    }
}