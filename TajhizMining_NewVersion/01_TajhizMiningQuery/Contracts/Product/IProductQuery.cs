using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_TajhizMiningQuery.Contracts.Product
{
    public interface IProductQuery
    {
        //To Get All Products In Shop Or Filter Them By Price Or CategorySlug
        List<ProductQueryModel> GetFilteredProducts(string slug, double initPrice, double finalPrice, string searchValue);
        List<ProductQueryModel> GetSpecialProducts();
        List<ProductQueryModel> GetNewProducts();
    }
}
