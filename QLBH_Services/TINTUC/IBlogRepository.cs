using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Services.TINTUC
{
    public interface IBlogRepository : IGenericRepository<Tintuc> 
    {
        List<Tintuc> SearchBlog(string tenSp);
        List<Tintuc> GetBlogPaging(int page, int pageSize);
        Tintuc GetBlogSaleNews();
        int GetTotalRec();
    }
}
