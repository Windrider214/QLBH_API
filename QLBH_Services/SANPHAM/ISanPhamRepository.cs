using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Services.SANPHAM
{
    public interface ISanPhamRepository : IGenericRepository<Sanpham>
    {
        List<Sanpham> GetListSP();
        List<Sanpham> SearchSP(string tenSp);
        List<Sanpham> GetListPaging(int page, int pageSize);
        int GetTotalRec();

        Sanpham GetByIdClient(string maSp);

        List<Sanpham> GetTop5();

        

        #region oldRepos
        //List<Sanpham> SANPHAM_GetAll();
        //Sanpham SANPHAM_GetById(string id);
        //void SANPHAM_Add(Sanpham sp);
        //int SANPHAM_Update(Sanpham sp);
        //int SANPHAM_Delete(string id);
        #endregion
    }
}
