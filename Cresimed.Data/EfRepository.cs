using Cresimed.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Cresimed.Data
{
    public partial class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private IGenericRepository<T> _genericRepository;
        public CresimedDBContext context;

        public GenericRepository(IGenericRepository<T> genericRepository) 
        {
            this._genericRepository = genericRepository;
        
        }

        public GenericRepository(CresimedDBContext context)
        {
            this.context = context;
        }

        public async Task Delete(T entity)
        {
            await _genericRepository.Delete(entity);
        }

        public async Task<List<T>> GetAll(object id)
        {
            return await _genericRepository.GetAll(id);
        }

        public async Task<T> GetById(object id)
        {
            return await _genericRepository.GetById(id);
        }

        public async Task<T> Insert(T entity)
        {
            return await _genericRepository.Insert(entity);
        }

        public async Task<T> Update(T entity)
        {
            return await _genericRepository.Update(entity);
        }
    }

}