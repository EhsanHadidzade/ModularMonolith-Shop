using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using SM.Application.ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administrator.Pages.Shop.Product
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }


        [BindProperty]
        public ProductSearchModel searchmodel { get; set; }


        public List<ProductViewModel> Products { get; set; }

        public SelectList ProductCategories { get; set; }

        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ProductSearchModel searchmodel)
        {
            ProductCategories = new SelectList(_productCategoryApplication.GetAllProductCategories(), "Id", "Name");
            Products = _productApplication.Search(searchmodel);
        }
        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct() { Categories = _productCategoryApplication.GetAllProductCategories() };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateProduct command)
        {
           var result= _productApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var product=_productApplication.GetDetails(id);
            product.Categories= _productCategoryApplication.GetAllProductCategories();
            return Partial("./Edit",product);
        }
        public JsonResult OnPostEdit(EditProduct command)
        {
            var result = _productApplication.Edit(command);
            return new JsonResult(result);
        }

        public RedirectToPageResult OnGetNotInStock(long id)
        {
            var result=_productApplication.NotInStock(id);
            if (result.IsSuccedded)
            {
                return RedirectToPage("./index");
            }

            Message=result.Message;
            return RedirectToPage("./index");

        }
        public RedirectToPageResult OnGetISInStock(long id)
        {
            var result = _productApplication.InStock(id);
            if (result.IsSuccedded)
            {
                return RedirectToPage("./index");
            }

            Message = result.Message;
            return RedirectToPage("./index");

        }
    }
}
