using Microsoft.EntityFrameworkCore;
using QLBH_DataAccess;
using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
using QLBH_Services.HOADON;
using QLBH_Services.SANPHAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Services.PHANHOI
{
    public class PhanHoiRepository : GenericRepository<Phanhoi>, IPhanHoiRepository
    {
        public PhanHoiRepository(QLBH_ONLINEContext context) : base(context) { }

        List<Phanhoi> IPhanHoiRepository.SearchPH(string tieuDe)
        {

            return _context.Phanhois.
                Where(x => x.TieuDe.Contains(tieuDe.Trim())).
                ToList();
        }


        List<Phanhoi> IPhanHoiRepository.GetListPaging(int page, int pageSize)
        {

            return _context.Phanhois.
                OrderByDescending(y => y.NgayGui).
                Skip((page - 1) * pageSize).Take(pageSize).
                ToList();
        }

        int IPhanHoiRepository.GetTotalRec()
        {
            return _context.Phanhois.
               ToList().Count();
        }


        List<Phanhoi> IPhanHoiRepository.GetFeedbackByCustomerID( string MaKH)
        {

            return _context.Phanhois.
                OrderByDescending(y => y.NgayGui).
                    Where(x => x.MaKh == MaKH).
                    ToList();
        }

        List<Phanhoi> IPhanHoiRepository.GetListPagingByDate(int page, int pageSize, DateTime startDate, DateTime endDate)
        {
            if (startDate.Date == endDate.Date)
            {
                return _context.Phanhois.
                           Where(s => s.NgayGui.Value.Date == endDate.Date).
                           Skip((page - 1) * pageSize).Take(pageSize).
                           ToList();
            }
            return _context.Phanhois.
                      Where(s => s.NgayGui.Value.Date >= startDate.Date && s.NgayGui.Value.Date <= endDate.Date).
                      Skip((page - 1) * pageSize).Take(pageSize).
                      ToList();

        }
    }
}
