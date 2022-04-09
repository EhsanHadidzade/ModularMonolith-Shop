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
        List<ProductQueryModel> GetFilteredProducts(string categorySlug, double initPrice, double finalPrice, string searchValue,string keyword);

        //To Get Products With special discount amount  To Show In Home Page
        List<ProductQueryModel> GetSpecialProducts();

        //To Get New Products To Show In Home Page
        List<ProductQueryModel> GetNewProducts();

        //To Get The Details of an Specific Product For Product Single Page
        ProductQueryModel GetProductDetailsByProductSlug(string ProductSlug);

        //to get related Products For product single page
        List<ProductQueryModel> GetRelatedProductsByCategorySlug(string categorySlug);
    }
}
