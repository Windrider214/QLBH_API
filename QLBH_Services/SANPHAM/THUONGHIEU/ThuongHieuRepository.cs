using Microsoft.EntityFrameworkCore;
using QLBH_DataAccess;
using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Services.SANPHAM.THUONGHIEU
{
    public class ThuongHieuRepository : GenericRepository<Thuonghieu>, IThuongHieuRepository
    {
        public ThuongHieuRepository(QLBH_ONLINEContext context) : base(context) { }

        #region oldRepos
        //private readonly QLBH_ONLINEContext _dbContext;

        //public ThuongHieuRepository(QLBH_ONLINEContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        //public List<Thuonghieu> SearchThuonghieu(string TenThuonghieu)
        //{
        //    var lst = _dbContext.Thuonghieus.ToList().FindAll(s => s.TenTh.Contains(TenThuonghieu));
        //    if (lst.Count != 0)
        //    {
        //        return lst;
        //    }
        //    return null;
        //}

        //public void Thuonghieu_Add(Thuonghieu th)
        //{
        //    _dbContext.Add(th);

        //}

        //public void Thuonghieu_Delete(string id)
        //{
        //    Thuonghieu th = _dbContext.Thuonghieus.AsNoTracking().Where(s => s.MaTh == id).FirstOrDefault();
        //    if (th != null)
        //    {
        //        _dbContext.Remove(th);
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}

        //public List<Thuonghieu> Thuonghieu_GetAll()
        //{
        //    return _dbContext.Thuonghieus.ToList();
        //}

        //public Thuonghieu Thuonghieu_GetById(string id)
        //{
        //    Thuonghieu th = _dbContext.Thuonghieus.AsNoTracking().Where(s => s.MaTh == id).FirstOrDefault();
        //    if (th != null)
        //    {
        //        return th;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //public void Thuonghieu_Update(Thuonghieu th)
        //{
        //    var check = _dbContext.Thuonghieus.AsNoTracking().Where(s => s.MaTh == th.MaTh).FirstOrDefault();
        //    if (check != null)
        //    {
        //        //_dbContext.Update(lsp);
        //        _dbContext.Entry(th).State = EntityState.Modified;

        //    }
        //    return;
        //}
        #endregion
    }
}
