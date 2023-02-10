
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cresimed.Core.Data
{
    public partial interface IGenericRepository<T> where T:class
    {
        Task<List<T>> GetAll(object id);
        Task<T> GetById(object id);
        Task<T> Insert(T entity);
        Task<T> Update(T entity);
        Task  Delete(T entity);
        //IQueryable<T> Table { get;}


    }
}
