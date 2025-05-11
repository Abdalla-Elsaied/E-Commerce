using DomainLayer.Entities;
using DomainLayer.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repositories.Contract
{
	public interface IGenericRepository<T> where T : BaseModel
	{
		Task<T?> GetAsync(int id);

		Task<IReadOnlyList<T>> GetAllAsync();

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecific<T> spec );
        Task<T?> GetWithSpecAsync(ISpecific<T> spec);
		Task<int> GetCountSpecAsync(ISpecific<T> spec);

		Task AddAsync(T entity);
		void Remove(T entity);
		void UpdateAsync(T entity);
    }
}
