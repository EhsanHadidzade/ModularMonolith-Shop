using _01_TajhizMiningQuery.Contracts.Product;
using _01_TajhizMiningQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages.Product
{
    public class IndexModel : PageModel
    {
        public List<ProductCategoryForShopQueryModel> AllCategories;
        public List<ProductQueryModel> AllProducts;
        public List<string> Tags;

        //we use this model to fill SEO view data values
        public ProductCategoryForShopQueryModel Category;

        private readonly IproductCategoryQuery _productCategoryQuery;
        private readonly IProductQuery _productQuery;

        public IndexModel(IproductCategoryQuery productCategoryQuery, IProductQuery productQuery)
        {
            _productCategoryQuery = productCategoryQuery; 
            _productQuery = productQuery;
        }

        //we do filter products by category that is this "id" that we get in this method,by Price or by searchvalue
        public void OnGet(string id, double initPrice, double finalPrice, string searchValue,string keyword)
        {
            //this id is the CategorySlug that we get to filter products

            AllCategories = _productCategoryQuery.GetAllCategoriesForShop();
            AllProducts = _productQuery.GetFilteredProducts(id, initPrice, finalPrice, searchValue,keyword);

            if (!string.IsNullOrWhiteSpace(id))
            {
                ViewData["category"] = _productCategoryQuery.GetCategoryByCategorySlug(id).Name;
                Category = _productCategoryQuery.GetCategoryByCategorySlug(id);
            }
               
            // here we find the keywords and extract them from categories and add them in Tags property to use in client
            Tags = new List<string>();
            if (string.IsNullOrWhiteSpace(id))
            {
                foreach (var item in AllCategories)
                    Tags.AddRange(item.KeywordsList);
            }
            else
            {
                Tags = _productCategoryQuery.GetCategoryByCategorySlug(id).KeywordsList;
            }

            if (!string.IsNullOrWhiteSpace(searchValue))
                ViewData["searchValue"] = searchValue;

            if (!string.IsNullOrWhiteSpace(keyword))
                ViewData["searchValue"] = keyword;
        }




    }
}
