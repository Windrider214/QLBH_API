using System;
using System.Collections.Generic;

namespace QLBH_DataAccess.Models
{
    public partial class Hoadonct
    {
        public string? MaHd { get; set; }
        public string? MaSp { get; set; }
        public int? DonGiaBan { get; set; }
        public int? SoLuong { get; set; }
        public int? GiamGia { get; set; }
        public int Stt { get; set; }

        public virtual Hoadon? MaHdNavigation { get; set; }
        public virtual Sanpham? MaSpNavigation { get; set; }
    }
}
