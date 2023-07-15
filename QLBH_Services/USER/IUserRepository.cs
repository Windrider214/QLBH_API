using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Services.USER
{
    public interface IUserRepository : IGenericRepository<AspNetUser>
    {
        AspNetUser GetUserByID(string userID);

        List<AspNetUser> GetAllUsersPaging(int page, int pageSize);

        List<AspNetUser> SearchUser(string userName);

        int GetTotalRec();

    }
}
