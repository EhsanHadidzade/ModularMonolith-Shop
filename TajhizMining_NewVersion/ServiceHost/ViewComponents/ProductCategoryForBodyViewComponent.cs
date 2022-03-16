using _01_TajhizMiningQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryForBodyViewComponent:ViewComponent
    {
        private IproductCategoryQuery _prodcutcategoryquery;

        public ProductCategoryForBodyViewComponent(IproductCategoryQuery prodcutcategoryquery)
        {
            _prodcutcategoryquery = prodcutcategoryquery;
        }

        public IViewComponentResult Invoke()
        {
            return View(_prodcutcategoryquery.GetProductCategoriesForBody());
        }
    }
}
