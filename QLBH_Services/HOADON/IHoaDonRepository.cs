using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Services.HOADON
{
    public interface IHoaDonRepository : IGenericRepository<Hoadon>
    {
        List<Hoadon> GetListPaging(int page, int pageSize);
        List<Hoadon> SearchOrder(string maHd);
        List<Hoadon> GetListPagingByCusID(int page, int pageSize, string MaKH);
        int GetTotalRec();

    }
}
