using Microsoft.EntityFrameworkCore;
using QLBH_DataAccess;
using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Services.HOADON.HOADONCT
{
    public class HoaDonCTRepository : GenericRepository<Hoadonct>, IHoaDonCTRepository
    {
        public HoaDonCTRepository(QLBH_ONLINEContext context) : base(context) { }

        List<Hoadonct> IHoaDonCTRepository.GetListOrderProcduct(string mahd)
        {
            return _context.Hoadoncts.
                    Include(x => x.MaSpNavigation).
                    Where(x => x.MaHd == mahd).
                    ToList();
        }

        int IHoaDonCTRepository.GetTotalRec()
        {
            return _context.Hoadoncts.
                    Sum(x => x.SoLuong * x.DonGiaBan).Value;
        }
    }
}
