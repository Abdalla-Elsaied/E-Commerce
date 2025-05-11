using DomainLayer.Entities;
using DomainLayer.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RepositoryLayer.Repository
{
    internal static class SpecificationEvaluator<TEntity> where TEntity : BaseModel
    {
        public static  IQueryable<TEntity>  GetQuery(IQueryable<TEntity> QueryInput ,ISpecific<TEntity> spec )
        {
           var query = QueryInput;  //dbcontxset<T>();

           if(spec.Criteria != null) 
                query=query.Where(spec.Criteria);  // p=>p.id==id
            if (spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);
            else if (spec.OrderByDes != null)
                query = query.OrderByDescending(spec.OrderByDes);
            if(spec.IsPaginationEnable)
                query = query.Skip(spec.Skip).Take(spec.Take);
           query=spec.Includes.Aggregate(query,(Currentquerry,Expression)=>Currentquerry.Include(Expression));

           return query;
        }
    }
}
