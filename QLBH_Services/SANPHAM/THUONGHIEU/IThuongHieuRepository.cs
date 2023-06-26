using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Services.SANPHAM.THUONGHIEU
{
    public interface IThuongHieuRepository : IGenericRepository<Thuonghieu>
    {

        #region oldRepos
        //List<Thuonghieu> Thuonghieu_GetAll();
        //List<Thuonghieu> SearchThuonghieu(string TenThuonghieu);
        //Thuonghieu Thuonghieu_GetById(string id);
        //void Thuonghieu_Add(Thuonghieu th);
        //void Thuonghieu_Update(Thuonghieu th);
        //void Thuonghieu_Delete(string id);
        #endregion
    }
}
