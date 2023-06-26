using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Services.SANPHAM.LOAISP
{
    public interface ILoaiSPRepository : IGenericRepository <Loaisp>
    {
        #region oldIRepos
        //List<Loaisp> LoaiSP_GetAll();
        //List<Loaisp> SearchLoaiSP(string TenLoaiSP);
        //Loaisp LoaiSP_GetById(string id);
        //void LoaiSP_Add(Loaisp lsp);
        //void LoaiSP_Update(Loaisp lsp);
        //void LoaiSP_Delete(string id);
        #endregion
    }
}
