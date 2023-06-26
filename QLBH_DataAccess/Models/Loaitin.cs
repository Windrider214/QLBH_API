using System;
using System.Collections.Generic;

namespace QLBH_DataAccess.Models
{
    public partial class Loaitin
    {
        public Loaitin()
        {
            Tintucs = new HashSet<Tintuc>();
        }

        public string MaDm { get; set; } = null!;
        public string? TenDm { get; set; }

        public virtual ICollection<Tintuc> Tintucs { get; set; }
    }
}
