using Microsoft.EntityFrameworkCore;
using QLBH_DataAccess;
using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Services.SANPHAM.LOAISP
{
    public class LoaiSPRepository : GenericRepository<Loaisp>, ILoaiSPRepository
    {
        public LoaiSPRepository(QLBH_ONLINEContext context) : base(context) { }

        #region oldRepos
        //private readonly QLBH_ONLINEContext _dbContext;

        //public LoaiSPRepository(QLBH_ONLINEContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        //public void LoaiSP_Add(Loaisp  lsp)
        //{
        //    _dbContext.Add(lsp);
        //}

        //public void LoaiSP_Delete(string id)
        //{
        //    Loaisp lsp = _dbContext.Loaisps.AsNoTracking().Where(s => s.MaLoai == id).FirstOrDefault();
        //    if(lsp != null)
        //    {
        //        _dbContext.Remove(lsp);
        //    }
        //    else
        //    {
        //        return;
        //    }    
        //}

        //public List<Loaisp> LoaiSP_GetAll()
        //{
        //    return  _dbContext.Loaisps.ToList();
        //}

        //public Loaisp LoaiSP_GetById(string id)
        //{
        //    Loaisp lsp = _dbContext.Loaisps.AsNoTracking().Where(s => s.MaLoai == id).FirstOrDefault();
        //    if(lsp != null)
        //    {
        //        return lsp;
        //    }
        //    return null;
        //}

        //public void LoaiSP_Update(Loaisp lsp)
        //{
        //    var check = _dbContext.Loaisps.AsNoTracking().Where(s => s.MaLoai == lsp.MaLoai).FirstOrDefault();
        //    if(check != null)
        //    {
        //        //_dbContext.Update(lsp);
        //        _dbContext.Entry(lsp).State = EntityState.Modified;

        //    }
        //    return;
        //}

        //public List<Loaisp> SearchLoaiSP(string TenLoaiSP)
        //{
        //    var lst = _dbContext.Loaisps.ToList().FindAll(s => s.TenLoaiSp.Contains(TenLoaiSP));
        //    if (lst .Count != 0)
        //    {
        //        return lst;
        //    }
        //    return null;
        //}
        #endregion
    }
}
