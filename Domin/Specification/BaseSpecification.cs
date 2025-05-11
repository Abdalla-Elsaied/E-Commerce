using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Specification
{
    public class BaseSpecification<T> : ISpecific<T> where T : BaseModel
    {
        public Expression<Func<T, bool>> Criteria { get; set; } = null;
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; } = null;
        public Expression<Func<T, object>> OrderByDes { get; set; } = null;
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnable { get; set; }

        public BaseSpecification()
        {
            //
        }
        public BaseSpecification(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
        }
        public void AddOrderBy(  Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }
        public void AddOrderByDes(Expression<Func<T, object>> orderByDes)
        {
            OrderByDes = orderByDes;
        }
        public void ApplyPagination(int skip ,int take)
        {
            IsPaginationEnable = true;
            Skip = skip;
            Take = take;
        }

    }
}
