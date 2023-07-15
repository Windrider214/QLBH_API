using Microsoft.EntityFrameworkCore;
using QLBH_DataAccess;
using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
using QLBH_Services.SANPHAM;
using QLBH_Services.USER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Services.KHACHHANG
{
    public class KhachHangRepository : GenericRepository<Khachhang>, IKhachHangRepository
    {
        public KhachHangRepository(QLBH_ONLINEContext context) : base(context) { }

        Khachhang IKhachHangRepository.GetByUserID(string userID)
        {
            return _context.Khachhangs.
                   Include(x => x.Login).
                   Where(x => x.LoginId == userID).
                   AsNoTracking().
                   FirstOrDefault();
                   
        }

        List<Khachhang> IKhachHangRepository.GetListPaging(int page, int pageSize)
        {
            return _context.Khachhangs.
                   Include(x => x.Login).
                   Include(x => x.Hoadons).
                   Include(x => x.Phanhois).
                   OrderByDescending(y => y.TenKh).
                   Skip((page - 1) * pageSize).Take(pageSize).
                   ToList();

        }

        List<Khachhang> IKhachHangRepository.SearchKH(string TenKH)
        {
            return _context.Khachhangs.
                   Include(x => x.Login).
                   Include(x => x.Hoadons).
                   Include(x => x.Phanhois).
                   OrderByDescending(y => y.TenKh).
                   Where(y => y.TenKh.Contains(TenKH.Trim())).
                   ToList();
        }

        int IKhachHangRepository.GetTotalRec()
        {
            return _context.Khachhangs.
                   Include(x => x.Login).
                   Include(x => x.Hoadons).
                   Include(x => x.Phanhois).
                   ToList().Count();
        }
    }
}
