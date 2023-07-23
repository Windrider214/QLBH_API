using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLBH_WebManage.DTO
{
    public class LOAISP
    {
        public LOAISP() {
            Sanphams = new HashSet<SANPHAM>();

        }

            public string maLoai { get; set; }
        [Required]
        public string tenLoaiSp { get; set; }

        public string image { get; set; }
        public string moTa { get; set; }


        public ICollection<SANPHAM> Sanphams { get; set; }

    }
}