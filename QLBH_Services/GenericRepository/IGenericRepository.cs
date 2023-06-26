using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_DataAccess.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(string id);
        List<T> GetAll();
        void Add(T entity);
        void Update(T entities);
        void Remove(T entity);
        List<T> Search(Expression<Func<T, bool>> expression);
 
    }
 }

