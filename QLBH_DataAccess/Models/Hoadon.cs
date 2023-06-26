using System;
using System.Collections.Generic;

namespace QLBH_DataAccess.Models
{
    public partial class Hoadon
    {
        public Hoadon()
        {
            Hoadoncts = new HashSet<Hoadonct>();
        }

        public string MaHd { get; set; } = null!;
        public string? MaKh { get; set; }
        public string? HoTenKh { get; set; }
        public string? SdtKh { get; set; }
        public string? DiaChiKh { get; set; }
        public string? EmailKh { get; set; }
        public DateTime? NgayDat { get; set; }
        public DateTime? NgayGiao { get; set; }
        public string? LoaiThanhToan { get; set; }
        public string? MaVanDon { get; set; }
        public int? PhiVanChuyen { get; set; }
        public int? TongTien { get; set; }
        public bool? TrangThai { get; set; }
        public string? GhiChu { get; set; }

        public virtual Khachhang? MaKhNavigation { get; set; }
        public virtual ICollection<Hoadonct> Hoadoncts { get; set; }
    }
}
