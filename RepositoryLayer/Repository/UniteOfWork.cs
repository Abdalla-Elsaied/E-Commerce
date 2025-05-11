using DomainLayer.Entities;
using DomainLayer.Entities.Order_Aggregate;
using DomainLayer.Repositories.Contract;
using RepositoryLayer.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class UniteOfWork: IUniteOfWork
    {
        private readonly StoreContext _dbContext;
        private  Hashtable _repository;
       
        public UniteOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _repository= new Hashtable();
        }

        public IGenericRepository<Tentity> Repository<Tentity>() where Tentity : BaseModel
        {
           var Key= typeof(Tentity).Name;
           if(! _repository.ContainsKey(Key))
            {
                var repository =new GenericRepository<Tentity>(_dbContext);
                _repository.Add(Key, repository);
            }
           return _repository[Key] as IGenericRepository<Tentity>;
        }

        public async Task<int> ComplateAsync()
        => await _dbContext.SaveChangesAsync();
 

        public async ValueTask DisposeAsync()
       => await _dbContext.DisposeAsync();

    }
}
