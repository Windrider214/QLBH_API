using System;
using System.Collections.Generic;

namespace QLBH_DataAccess.Models
{
    public partial class Khachhang
    {
        public Khachhang()
        {
            Hoadons = new HashSet<Hoadon>();
            Phanhois = new HashSet<Phanhoi>();
        }

        public string MaKh { get; set; } = null!;
        public string? TenKh { get; set; }
        public string? Sdt { get; set; }
        public string? DiaChi { get; set; }
        public string? LoginId { get; set; }

        public virtual AspNetUser? Login { get; set; }
        public virtual ICollection<Hoadon> Hoadons { get; set; }
        public virtual ICollection<Phanhoi> Phanhois { get; set; }
    }
}
