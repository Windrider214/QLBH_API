using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_WebClient.DTO
{
    public class MEDIUM
    {
        public string mediaId { get; set; }
        public string image { get; set; }
        public int mediaTypeId { get; set; }
        public bool isActive { get; set; }

        public MEDIATYPE mediaType { get; set;}
    }
}