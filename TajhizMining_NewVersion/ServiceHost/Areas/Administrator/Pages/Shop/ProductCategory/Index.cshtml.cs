using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SM.Application.ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administrator.Pages.Shop.ProductCategory
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public ProductCategorySearchModel searchmodel { get; set; }
        public List<ProductCategoryViewModel> ProductCategories { get; set; }

        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ProductCategorySearchModel searchmodel)
        {
            ProductCategories=_productCategoryApplication.Search(searchmodel);
        }
        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateProductCategory());
        }

        public JsonResult OnPostCreate(CreateProductCategory command)
        {
           var result= _productCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        public PartialViewResult OnGetEdit(long id)
        {
            var productcategory=_productCategoryApplication.GetDetails(id);
            return Partial("./Edit",productcategory);
        }
        public JsonResult OnPostEdit(EditProductCategory command)
        {
            var result = _productCategoryApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
