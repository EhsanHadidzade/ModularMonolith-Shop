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
        public ColleagueDiscountSearchModel searchmodel { get; set; }


        public List<CustomerDiscountViewModel> CustomerDiscounts { get; set; }

        public SelectList Products { get; set; }

        private readonly IProductApplication _productApplication;
        private readonly ICustomerDiscountApplication _CustomerDiscountApplication;

        public IndexModel(IProductApplication productApplication, ICustomerDiscountApplication CustomerDiscountApplication)
        {
            _productApplication = productApplication;
            _CustomerDiscountApplication = CustomerDiscountApplication;
        }

        public void OnGet(ColleagueDiscountSearchModel searchmodel)
        {
            
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            CustomerDiscounts = _CustomerDiscountApplication.Search(searchmodel);
        }
        public IActionResult OnGetCreate()
        {
            var command = new CreateCustomerDiscount() { Products = _productApplication.GetProducts() };

            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateCustomerDiscount command)
        {
           var result= _CustomerDiscountApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var customerdiscount= _CustomerDiscountApplication.GetDetails(id);
            customerdiscount.Products = _productApplication.GetProducts();
            return Partial("./Edit", customerdiscount);
        }
        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            var result = _CustomerDiscountApplication.Edit(command);
            return new JsonResult(result);
        }

        
    }
}
