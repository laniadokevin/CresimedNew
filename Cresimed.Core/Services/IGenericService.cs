using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;

namespace Cresimed.Core.Services
{
    public interface IGenericService<TEntity> where TEntity:class
    {
        Task<List<TEntity>> GetAll(object id);
        Task<TEntity> GetById(object id);
        Task<TEntity> Insert(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}
