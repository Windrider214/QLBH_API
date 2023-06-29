using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_WebManage.DTO
{
    public class HOADON
    {
        public string maHd { get; set; } 
        public string maKh { get; set; }
        public string hoTenKh { get; set; }
        public string sdtKh { get; set; }
        public string diaChiKh { get; set; }
        public string emailKh { get; set; }
        public DateTime? ngayDat { get; set; }
        public DateTime? ngayGiao { get; set; }
        public string loaiThanhToan { get; set; }
        public string maVanDon { get; set; }
        public int phiVanChuyen { get; set; }
        public int tongTien { get; set; }
        public bool trangThai { get; set; }
        public string ghiChu { get; set; }

        public virtual KHACHHANG MaKhNavigation { get; set; }
    }
}