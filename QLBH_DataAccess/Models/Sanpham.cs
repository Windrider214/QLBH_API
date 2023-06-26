using System;
using System.Collections.Generic;

namespace QLBH_DataAccess.Models
{
    public partial class Sanpham
    {
        public Sanpham()
        {
            Hoadoncts = new HashSet<Hoadonct>();
        }

        public string MaSp { get; set; } = null!;
        public string? TenSp { get; set; }
        public string? DonViTinh { get; set; }
        public int? DonGia { get; set; }
        public string? MaLoai { get; set; }
        public string? MaTh { get; set; }
        public string? SeoImage { get; set; }
        public string? Image { get; set; }
        public string? MoTa { get; set; }
        public string? ChiTietSp { get; set; }
        public int? SoLanXem { get; set; }
        public int? GiamGia { get; set; }
        public int? SoLuong { get; set; }
        public bool? TinhTrang { get; set; }
        public DateTime? NgayThem { get; set; }

        public virtual Loaisp? MaLoaiNavigation { get; set; }
        public virtual Thuonghieu? MaThNavigation { get; set; }
        public virtual ICollection<Hoadonct> Hoadoncts { get; set; }
    }
}
