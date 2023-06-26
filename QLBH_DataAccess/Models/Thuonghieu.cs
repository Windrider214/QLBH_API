using System;
using System.Collections.Generic;

namespace QLBH_DataAccess.Models
{
    public partial class Thuonghieu
    {
        public Thuonghieu()
        {
            Sanphams = new HashSet<Sanpham>();
        }

        public string MaTh { get; set; } = null!;
        public string? TenTh { get; set; }
        public string? XuatXu { get; set; }
        public string? AnhTh { get; set; }

        public virtual ICollection<Sanpham> Sanphams { get; set; }
    }
}
