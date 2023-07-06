using System;
using System.Collections.Generic;

namespace QLBH_DataAccess.Models
{
    public partial class Medium
    {
        public string MediaId { get; set; } = null!;
        public string? Image { get; set; }
        public int? MediaTypeId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual Mediatype? MediaType { get; set; }
    }
}
