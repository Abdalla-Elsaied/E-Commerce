using DomainLayer.Entities;
using DomainLayer.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Products_Spec
{
    public class ProductWithBrandandCategorySpec :BaseSpecification<Product>
    {
        public ProductWithBrandandCategorySpec(ProductSpecParams productParams) 
            :base(
                    p=>(string.IsNullOrEmpty(productParams.Search) || p.Name.ToLower().Contains(productParams.Search)) 
                    &&(!productParams.BrandId.HasValue || p.BrandId == productParams.BrandId) 
                    && (!productParams.CategoryId.HasValue || p.CategoryId == productParams.CategoryId)     
                 )
        {
            Includes.Add(p=>p.Brand);
            Includes.Add(p=>p.Category);
            if(!string.IsNullOrEmpty(productParams.Sort))
            {
                switch(productParams.Sort)
                {
                    case "priceAsc":
                        //OrderBy = p => p.Price;
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        //OrderByDes = p => p.Price;
                        AddOrderByDes(p => p.Price);
                        break;
                    default:
                        //OrderBy = p => p.Name;
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }
            // 20    pagesize 5     index 3 
            ApplyPagination(((productParams.PageIndex - 1) * productParams.PageSize), productParams.PageSize);
        }
        public ProductWithBrandandCategorySpec(int Id) : base(p => p.Id == Id)
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}
