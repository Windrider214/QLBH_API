using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLBH_WebManage.DTO
{
    public class THUONGHIEU
    {
        public THUONGHIEU()
        {
            Sanphams = new HashSet<SANPHAM>();
        }

        public string maTh { get; set; }
        [Required]
        public string tenTh { get; set; }
        [Required]
        public string xuatXu { get; set; }
        public string anhTh { get; set; }

        public ICollection<SANPHAM> Sanphams { get; set; }
    }
}