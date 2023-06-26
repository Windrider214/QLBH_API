using System;
using System.Collections.Generic;

namespace QLBH_DataAccess.Models
{
    public partial class Tintuc
    {
        public string MaTinTuc { get; set; } = null!;
        public string? TieuDe { get; set; }
        public string? AnhTt { get; set; }
        public string? MoTa { get; set; }
        public string? NoiDung { get; set; }
        public DateTime? NgayDang { get; set; }
        public int? LuotXem { get; set; }
        public string? MaDm { get; set; }
        public string? UserId { get; set; }

        public virtual Loaitin? MaDmNavigation { get; set; }
        public virtual AspNetUser? User { get; set; }
    }
}
