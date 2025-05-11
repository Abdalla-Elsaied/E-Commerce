using DomainLayer.Entities;
using DomainLayer.Repositories.Contract;
using DomainLayer.Specification;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
	{
		private readonly StoreContext _dbContext;

		public GenericRepository(StoreContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			return await _dbContext.Set<T>().ToListAsync();
		}
        public async Task<T?> GetAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}


        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecific<T> spec)
        {

            return await SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec).ToListAsync();
        }

        public async Task<T?> GetWithSpecAsync(ISpecific<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountSpecAsync(ISpecific<T> spec)
        {
          return  await SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec).CountAsync();
        }

        public async Task AddAsync(T entity)=>await _dbContext.AddAsync(entity);
        public void Remove(T entity)
        => _dbContext.Remove(entity);
        public void UpdateAsync(T entity)
        =>_dbContext.Update(entity);
    }
}
