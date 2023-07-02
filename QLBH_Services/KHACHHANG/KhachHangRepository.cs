using Microsoft.EntityFrameworkCore;
using QLBH_DataAccess;
using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
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
    }
}
