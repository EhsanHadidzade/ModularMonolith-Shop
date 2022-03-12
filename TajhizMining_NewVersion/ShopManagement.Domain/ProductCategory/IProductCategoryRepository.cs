using _01_Framework.Domain;
using SM.Application.ShopManagement.Application.Contracts.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SM.Domain.ShopManagement.Domain.ProductCategory
{
    public interface IProductCategoryRepository:IRepository<long,ProductCategory>
    {
     
        EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchmodel);
    }
}
