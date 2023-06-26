using System;
using System.Collections.Generic;

namespace QLBH_DataAccess.Models
{
    public partial class Mediatype
    {
        public Mediatype()
        {
            Media = new HashSet<Medium>();
        }

        public int MediaTypeId { get; set; }
        public string? MediaTypeName { get; set; }

        public virtual ICollection<Medium> Media { get; set; }
    }
}
