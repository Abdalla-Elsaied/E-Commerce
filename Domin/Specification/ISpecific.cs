using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Specification
{
    public interface ISpecific<T> where T : BaseModel
    {
        public Expression<Func<T,bool>> Criteria { get; set; }
        public List<Expression<Func<T,object>>> Includes { get; set; }
        public Expression<Func<T,object>> OrderBy { get; set; }
        public Expression<Func<T,object>> OrderByDes { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnable { get; set; }
    }
}
