using DomainLayer.Entities;
using DomainLayer.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repositories.Contract
{
    public interface IUniteOfWork:IAsyncDisposable  
    {
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseModel;
       
        Task<int> ComplateAsync();
    }
}
