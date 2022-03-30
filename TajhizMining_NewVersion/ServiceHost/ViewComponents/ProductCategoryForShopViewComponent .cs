using _01_TajhizMiningQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryForShopViewComponent : ViewComponent
    {
        private IproductCategoryQuery _prodcutcategoryquery;

        public ProductCategoryForShopViewComponent(IproductCategoryQuery prodcutcategoryquery)
        {
            _prodcutcategoryquery = prodcutcategoryquery;
        }

        public IViewComponentResult Invoke()
        {
            return View(_prodcutcategoryquery.GetProductCategoriesForHeader());
        }
    }
}
