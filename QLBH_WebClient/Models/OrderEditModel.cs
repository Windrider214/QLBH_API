using QLBH_WebClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_WebClient.Models
{
    public class OrderEditModel : HOADON
    {
        public List<HOADONCT> OrderDetail { get; set; }
    }
}