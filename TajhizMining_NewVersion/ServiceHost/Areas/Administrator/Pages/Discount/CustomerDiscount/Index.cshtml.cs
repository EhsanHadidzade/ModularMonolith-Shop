using DiscountManagement.Application.Contract.CustomerDIscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using SM.Application.ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administrator.Pages.Discount.CustomerDiscount
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }


        [BindProperty]
        public CustomerDiscountSearchModel searchmodel { get; set; }


        public List<CustomerDiscountViewModel> CustomerDiscounts { get; set; }

        public SelectList Products { get; set; }

        private readonly IProductApplication _productApplication;
        private readonly ICustomerDiscountApplication _CustomerDiscountApplication;

        public IndexModel(IProductApplication productApplication, ICustomerDiscountApplication CustomerDiscountApplication)
        {
            _productApplication = productApplication;
            _CustomerDiscountApplication = CustomerDiscountApplication;
        }

        public void OnGet(CustomerDiscountSearchModel searchmodel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            CustomerDiscounts = _CustomerDiscountApplication.Search(searchmodel);
        }
        public IActionResult OnGetCreate()
        {
            //var command = new CreateProduct() { Categories = _productCategoryApplication.GetAllProductCategories() };
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");

            return Partial("./Create");
        }

        public JsonResult OnPostCreate(CreateCustomerDiscount command)
        {
           var result= _CustomerDiscountApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var customerdiscount= _CustomerDiscountApplication.GetDetails(id);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            //product.Categories= _productCategoryApplication.GetAllProductCategories();
            return Partial("./Edit");
        }
        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            var result = _CustomerDiscountApplication.Edit(command);
            return new JsonResult(result);
        }

        //public RedirectToPageResult OnGetNotInStock(long id)
        //{
        //    var result=_productApplication.NotInStock(id);
        //    if (result.IsSuccedded)
        //    {
        //        return RedirectToPage("./index");
        //    }

        //    Message=result.Message;
        //    return RedirectToPage("./index");

        //}
        //public RedirectToPageResult OnGetISInStock(long id)
        //{
        //    var result = _productApplication.InStock(id);
        //    if (result.IsSuccedded)
        //    {
        //        return RedirectToPage("./index");
        //    }

        //    Message = result.Message;
        //    return RedirectToPage("./index");

        //}
    }
}
