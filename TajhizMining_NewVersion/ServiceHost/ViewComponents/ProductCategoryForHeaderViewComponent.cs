using _01_TajhizMiningQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryForHeaderViewComponent:ViewComponent
    {
        private IproductCategoryQuery _prodcutcategoryquery;

        public ProductCategoryForHeaderViewComponent(IproductCategoryQuery prodcutcategoryquery)
        {
            _prodcutcategoryquery = prodcutcategoryquery;
        }

        public IViewComponentResult Invoke()
        {
            return View(_prodcutcategoryquery.GetProductCategoriesForHeader());
        }
    }
}
