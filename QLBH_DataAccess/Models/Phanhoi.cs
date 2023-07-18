using System;
using System.Collections.Generic;

namespace QLBH_DataAccess.Models
{
    public partial class Phanhoi
    {
        public string MaPh { get; set; } = null!;
        public string? TieuDe { get; set; }
        public string? NoiDung { get; set; }
        public string? MaKh { get; set; }
        public string? TenKh { get; set; }
        public string? Email { get; set; }
        public DateTime? NgayGui { get; set; }
        public DateTime? NgayTraLoi { get; set; }
        public string? PhanHoi1 { get; set; }
        public bool? TinhTrang { get; set; }

        public virtual Khachhang? MaKhNavigation { get; set; }
    }
}
