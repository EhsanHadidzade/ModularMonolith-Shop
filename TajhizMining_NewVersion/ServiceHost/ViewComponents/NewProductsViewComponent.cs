using _01_TajhizMiningQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class NewProductsViewComponent:ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public NewProductsViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public IViewComponentResult Invoke()
        {
            return View(_productQuery.GetNewProducts());
        }
    }
}
