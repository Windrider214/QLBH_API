using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_WebManage.DTO
{
    public class BLOGTYPE
    {
        public BLOGTYPE()
        {
            Blogs = new HashSet<BLOG>();  
        }

        public string maDm { get; set; }
        public string tenDm { get; set; }

        public ICollection<BLOG> Blogs { get; set; }
    }
}