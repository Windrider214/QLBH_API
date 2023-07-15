using Microsoft.EntityFrameworkCore;
using QLBH_DataAccess;
using QLBH_DataAccess.GenericRepository;
using QLBH_DataAccess.Models;
using QLBH_Services.SANPHAM;
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
            return  _context.AspNetUsers.
                    Include(x => x.Khachhangs).
                    Include(x => x.Roles).
                    Where(x => x.Id == userID).
                    AsNoTracking().
                    FirstOrDefault();
        }

        List<AspNetUser> IUserRepository.GetAllUsersPaging(int page, int pageSize)
        {
            return _context.AspNetUsers.
                  Include(x => x.Khachhangs).
                  Include(x => x.Roles).
                  Skip((page - 1) * pageSize).Take(pageSize).
                  OrderBy(s => s.UserName).
                  AsNoTracking().
                  ToList();
        }

        List<AspNetUser> IUserRepository.SearchUser(string userName)
        {
            return _context.AspNetUsers.
                  Include(x => x.Khachhangs).
                  Include(x => x.Roles).
                  Where( x => x.UserName.Contains(userName.Trim())).
                  OrderBy(s => s.UserName).
                  AsNoTracking().
                  ToList();
        }

        int IUserRepository.GetTotalRec()
        {
            return _context.AspNetUsers.
                        Include(x => x.Khachhangs).
                  Include(x => x.Roles).
               ToList().Count();
        }
    }
}
