using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_WebClient.DTO
{
    public class MEDIATYPE
    {
        public MEDIATYPE()
        {
            Mediums = new HashSet<MEDIUM>();
        }

        public int mediaTypeId { get; set; }
        public string mediaTypeName { get; set; }

        public ICollection<MEDIUM> Mediums { get; set; }
    }
}