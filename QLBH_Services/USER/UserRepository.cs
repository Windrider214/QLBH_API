using Microsoft.EntityFrameworkCore;
using QLBH_DataAccess;
using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Services.USER
{
    public class UserRepository : GenericRepository<AspNetUser>, IUserRepository
    {
        public UserRepository(QLBH_ONLINEContext context) : base(context) { }

        AspNetUser IUserRepository.GetUserByID(string userID)
        {
            return _context.AspNetUsers.
                    Include(x => x.Khachhangs).
                    Where(x => x.Id == userID).
                    AsNoTracking().
                    FirstOrDefault();
        }
    }
}
