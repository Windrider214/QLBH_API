using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QLBH_DataAccess;
using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
using QLBH_Services.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Services.SANPHAM
{
    public class SanPhamRepository : GenericRepository<Sanpham>, ISanPhamRepository
    {
        public SanPhamRepository(QLBH_ONLINEContext context) : base(context) { }

        List<Sanpham> ISanPhamRepository.GetListSP() {

            return _context.Sanphams.
                Include(x => x.MaLoaiNavigation).
                Include(z => z.MaThNavigation).
                OrderByDescending(y => y.NgayThem).
                ToList();
        }

        List<Sanpham> ISanPhamRepository.SearchSP(string tenSp)
        {
            tenSp = Encoding.UTF8.GetString(Encoding.Default.GetBytes(tenSp));
            return _context.Sanphams.
                Include(x => x.MaLoaiNavigation).
                Include(z => z.MaThNavigation).
                Where( x => x.TenSp.Contains(tenSp.Trim())).
                ToList();
        }

        List<ProcSuggest> ISanPhamRepository.SearchSuggest(string tenSp)
        {
            List<ProcSuggest> sgtList = new List<ProcSuggest>();
            tenSp = Encoding.UTF8.GetString(Encoding.Default.GetBytes(tenSp));
            List<Sanpham> lst = _context.Sanphams.
                Include(x => x.MaLoaiNavigation).
                Include(z => z.MaThNavigation).
                Where(x => x.TenSp.Contains(tenSp)).
                ToList();
            foreach(var item in lst)
            {
                ProcSuggest s = new ProcSuggest();
                s.TenSp = item.TenSp;
                s.MaSp = item.MaSp;
                sgtList.Add(s);
            }
            return sgtList;
        }


        int ISanPhamRepository.GetTotalRec()
        {
            return _context.Sanphams.
               Include(x => x.MaLoaiNavigation).
               Include(z => z.MaThNavigation).
               ToList().Count();
        }

        List<Sanpham> ISanPhamRepository.GetListPaging(int page, int pageSize)
        {

            return _context.Sanphams.
                Include(x => x.MaLoaiNavigation).
                Include(z => z.MaThNavigation).
                OrderByDescending(y => y.NgayThem).
                Skip((page - 1) * pageSize).Take(pageSize).
                ToList();
        }


        List<Sanpham> ISanPhamRepository.GetListPagingActive(int page, int pageSize)
        {

            return _context.Sanphams.
                Include(x => x.MaLoaiNavigation).
                Include(z => z.MaThNavigation).
                Where(x => x.TinhTrang == true).
                OrderByDescending(y => y.NgayThem).
                Skip((page - 1) * pageSize).Take(pageSize).
                ToList();
        }

        List<Sanpham> ISanPhamRepository.GetListPagingByType(int page, int pageSize, string maloai)
        {

            return _context.Sanphams.
                Include(x => x.MaLoaiNavigation).
                Include(z => z.MaThNavigation).
                Where(x => x.TinhTrang == true && x.MaLoai == maloai).
                OrderByDescending(y => y.NgayThem).
                Skip((page - 1) * pageSize).Take(pageSize).
                ToList();
        }


        int ISanPhamRepository.GetTotalRecByType(string maloai)
        {

            return _context.Sanphams.
                Where(x => x.TinhTrang == true && x.MaLoai == maloai).
                ToList().Count();
        }

        List<Sanpham> ISanPhamRepository.GetListPagingByBrand(int page, int pageSize, string math)
        {

            return _context.Sanphams.
                Include(x => x.MaLoaiNavigation).
                Include(z => z.MaThNavigation).
                Where(x => x.TinhTrang == true && x.MaTh == math).
                OrderByDescending(y => y.NgayThem).
                Skip((page - 1) * pageSize).Take(pageSize).
                ToList();
        }


        int ISanPhamRepository.GetTotalRecByBrand(string math)
        {

            return _context.Sanphams.
                Where(x => x.TinhTrang == true && x.MaTh == math).
                ToList().Count();
        }

        List<Sanpham> ISanPhamRepository.GetTop5()
        {

            return _context.Sanphams.
                Include(x => x.MaLoaiNavigation).
                Include(z => z.MaThNavigation).
                OrderByDescending(y => y.NgayThem).
                Take(5).
                AsNoTracking().
                ToList();
        }

        Sanpham ISanPhamRepository.GetByIdClient(string maSp)
        {

            return _context.Sanphams.
                Include(x => x.MaLoaiNavigation).
                Include(z => z.MaThNavigation).
                Where( x => x.MaSp == maSp ).
                FirstOrDefault();
        }


        #region oldRepos
        //private readonly QLBH_ONLINEContext _dbContext;

        //public SanPhamRepository(QLBH_ONLINEContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        //public void SANPHAM_Add(Sanpham sp)
        //{
        //    _dbContext.Add(sp);            
        //}

        //public int SANPHAM_Delete(string id)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<Sanpham> SANPHAM_GetAll()
        //{
        //    return _dbContext.Sanphams.ToList();
        //}

        //public Sanpham SANPHAM_GetById(string id)
        //{
        //    throw new NotImplementedException();
        //}

        //public int SANPHAM_Update(Sanpham sp)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion
    }
}
