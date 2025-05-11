using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Specification
{
    public class ProductWithFilterationForCountSpec:BaseSpecification<Product>
    {
        public ProductWithFilterationForCountSpec(ProductSpecParams productParams) :base(
                    p=>
                    (string.IsNullOrEmpty(productParams.Search) || p.Name.ToLower().Contains(productParams.Search)) 
                    &&(!productParams.BrandId.HasValue || p.BrandId == productParams.BrandId) 
                    && (!productParams.CategoryId.HasValue || p.CategoryId == productParams.CategoryId)     
                 )
        {
            
        }

    }
}
