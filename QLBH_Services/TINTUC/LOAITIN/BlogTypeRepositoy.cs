using QLBH_DataAccess;
using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Services.TINTUC.LOAITIN
{
    public class BlogTypeRepositoy : GenericRepository<Loaitin> , IBlogTypeRepository
    {
        public BlogTypeRepositoy(QLBH_ONLINEContext context) : base(context) { }

    }
}
