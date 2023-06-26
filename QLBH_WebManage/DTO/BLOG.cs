using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_WebManage.DTO
{
    public class BLOG
    {
        public string   maTinTuc { get; set; }
        public string   tieuDe { get; set; }
        public string   anhTt { get; set; }
        public string   moTa { get; set; }
        public string   noiDung { get; set; }
        public DateTime? ngayDang { get; set; }
        public int luotXem { get; set; }
        public string maDm { get; set; }
        public string userId { get; set; }
        public BLOGTYPE maDmNavigation { get; set; }
        public USER user { get; set; }
    }
}