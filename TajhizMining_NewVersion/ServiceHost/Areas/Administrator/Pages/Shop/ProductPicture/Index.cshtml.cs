using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;
using SM.Application.ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administrator.Pages.Shop.ProductPicture
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }


        [BindProperty]
        public ProductPictureSearchModel searchmodel { get; set; }


        public List<ProductPictureViewModel> ProductPictures { get; set; }

        public SelectList Products { get; set; }

        private readonly IProductPictureApplication _productPictureApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(IProductPictureApplication productPictureApplication, IProductApplication productApplication)
        {
            _productPictureApplication = productPictureApplication;
            _productApplication = productApplication;
        }

        public void OnGet(ProductPictureSearchModel searchmodel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            ProductPictures = _productPictureApplication.Search(searchmodel);
        }
        public IActionResult OnGetCreate()
        {
            var command = new CreateProductPicture() { Products = _productApplication.GetProducts() };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateProductPicture command)
        {
           var result= _productPictureApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var productPicture=_productPictureApplication.GetDetails(id);
            productPicture.Products= _productApplication.GetProducts();
            return Partial("./Edit", productPicture);
        }
        public JsonResult OnPostEdit(EditProductPicture command)
        {
            var result = _productPictureApplication.Edit(command);
            return new JsonResult(result);
        }

        public RedirectToPageResult OnGetRemove(long id)
        {
            var result=_productPictureApplication.Remove(id);
            if (result.IsSuccedded)
            {
                return RedirectToPage("./index");
            }

            Message=result.Message;
            return RedirectToPage("./index");

        }
        public RedirectToPageResult OnGetRestore(long id)
        {
            var result = _productPictureApplication.Restore(id);
            if (result.IsSuccedded)
            {
                return RedirectToPage("./index");
            }

            Message = result.Message;
            return RedirectToPage("./index");

        }
    }
}
