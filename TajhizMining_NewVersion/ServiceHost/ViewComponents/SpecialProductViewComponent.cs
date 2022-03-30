using _01_TajhizMiningQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class SpecialProductViewComponent: ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public SpecialProductViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public IViewComponentResult Invoke()
        {
            return View(_productQuery.GetSpecialProducts());
        }
    }
}
