using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_WebManage.DTO
{
    public class ROLE
    {
        public ROLE()
        {
            Users = new HashSet<USER>();
        }

        public string Id { get; set; } = null;
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }

        public virtual ICollection<USER> Users { get; set; }
    }
}