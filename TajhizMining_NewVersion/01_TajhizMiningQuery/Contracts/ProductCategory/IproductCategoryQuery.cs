using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_TajhizMiningQuery.Contracts.ProductCategory
{
    public interface IproductCategoryQuery
    {
        List<ProductCategoryForHeaderQueryModel> GetProductCategoriesForHeader();
        List<ProductCategoryForBodyQueryModel> GetProductCategoriesForBody();
    }
}
