﻿using Microsoft.EntityFrameworkCore;
using QLBH_DataAccess;
using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
using QLBH_Services.SANPHAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Services.HOADON
{
    public class HoaDonRepository : GenericRepository<Hoadon>, IHoaDonRepository
    {
        public HoaDonRepository(QLBH_ONLINEContext context) : base(context) { }

        List<Hoadon> IHoaDonRepository.GetListPaging(int page, int pageSize)
        {

            return _context.Hoadons.
                    Include(x => x.MaKhNavigation).
                    OrderByDescending(y => y.NgayDat).
                    Skip((page - 1) * pageSize).Take(pageSize).
                    ToList();
        }

        int IHoaDonRepository.GetTotalRec()
        {
            return _context.Hoadons.
                     Include(x => x.MaKhNavigation).
                     ToList().Count();
        }

        List<Hoadon> IHoaDonRepository.GetListPagingByCusID(int page, int pageSize, string MaKH)
        {

            return _context.Hoadons.
                    Include(x => x.MaKhNavigation).
                    OrderByDescending(y => y.NgayDat).
                    Where(x => x.MaKh == MaKH).
                    Skip((page - 1) * pageSize).Take(pageSize).
                    ToList();
        }

        List<Hoadon> IHoaDonRepository.SearchOrder(string maHd)
        {
            return _context.Hoadons.
                Include(x => x.MaKhNavigation).
                Where(x => x.MaHd == maHd).
                ToList();
        }
    }
}
