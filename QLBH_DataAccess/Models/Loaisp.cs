using System;
using System.Collections.Generic;

namespace QLBH_DataAccess.Models
{
    public partial class Loaisp
    {
        public Loaisp()
        {
            Sanphams = new HashSet<Sanpham>();
        }

        public string MaLoai { get; set; } = null!;
        public string? TenLoaiSp { get; set; }
        public string? Image { get; set; }
        public string? MoTa { get; set; }

        public virtual ICollection<Sanpham> Sanphams { get; set; }
    }
}
