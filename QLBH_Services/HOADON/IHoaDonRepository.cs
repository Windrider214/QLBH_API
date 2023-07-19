using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
using QLBH_Services.Helper;
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
        List<Hoadon> GetListPagingByDate(int page, int pageSize, DateTime startDate, DateTime endDate);

        List<Hoadon> GetListByDate(DateTime startDate, DateTime endDate);

        List<Hoadon> GetListByCustomerID(string customerID);

        Hoadon GetOrderByID(string  maHd);
        int GetTotalRec();

        int GetCusTotalOrder(string MaKH);

        List<TKDOANHTHU> ThongKeDoanhThuThang(int year);

        List<TKKINHDOANH> ThongKeKinhDoanh();

    }
}
