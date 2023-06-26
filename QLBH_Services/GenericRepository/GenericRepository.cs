using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_DataAccess.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly QLBH_ONLINEContext _context;
        public GenericRepository(QLBH_ONLINEContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public List<T> GetAll()
        {
            List<T> lst = _context.Set<T>().ToList();
            return lst;
        }

        public T GetById(string id)
        {
            T lst =  _context.Set<T>().Find(id);
            _context.Entry(lst).State = EntityState.Detached;
            return lst;
        }

        public void Remove(T entity)
        {
           _context.Set<T>().Remove(entity);
        }


        public void Update(T entities)
        {
            _context.Update(entities);
        }

        public List<T> Search(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().AsNoTracking().Where(expression).ToList();
        }
    }
}
