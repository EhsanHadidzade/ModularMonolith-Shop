using _01_TajhizMiningQuery.Contracts.Product;
using _01_TajhizMiningQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages.Product
{
    public class IndexModel : PageModel
    {
        public List<ProductCategoryForShopQueryModel> AllCategories { get; set; }
        public List<ProductQueryModel> AllProducts { get; set; }

        private readonly IproductCategoryQuery _productCategoryQuery;
        private readonly IProductQuery _productQuery;

        public IndexModel(IproductCategoryQuery productCategoryQuery, IProductQuery productQuery)
        {
            _productCategoryQuery = productCategoryQuery;
            _productQuery = productQuery;
        }

        //we do filter products by category that is this "id" that we get in this method,by Price or by searchvalue
        public void OnGet(string id,double initPrice,double finalPrice,string searchValue)
        {
            //this id is the CategoryId that we get to filter products

            AllCategories = _productCategoryQuery.GetAllCategoriesForShop();
            AllProducts = _productQuery.GetFilteredProducts(id,initPrice,finalPrice, searchValue);

            if (searchValue != null)
            {
                ViewData["search"]=searchValue;
            }
        }
        
       

    }
}
