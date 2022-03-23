using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Application.Contract.CustomerDIscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ColleagueDiscountSearchModel = DiscountManagement.Application.Contract.ColleagueDiscount.ColleagueDiscountSearchModel;

namespace ServiceHost.Areas.Administrator.Pages.Discount.ColleagueDiscount
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }


        [BindProperty]
        public ColleagueDiscountSearchModel searchmodel { get; set; }


        public List<ColleagueDiscountViewModel> ColleagueDiscounts { get; set; }

        public SelectList Products { get; set; }

        private readonly IProductApplication _productApplication;
        private readonly IColleagueDiscountApplication _ColleagueDiscountApplication;

        public IndexModel(IProductApplication productApplication, IColleagueDiscountApplication colleagueDiscountApplication)
        {
            _productApplication = productApplication;
            _ColleagueDiscountApplication = colleagueDiscountApplication;
        }

        public void OnGet(ColleagueDiscountSearchModel searchmodel)
        {
            
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            ColleagueDiscounts = _ColleagueDiscountApplication.Search(searchmodel);
        }
        public IActionResult OnGetCreate()
        {
            var command = new CreateColleagueDiscount() { Products = _productApplication.GetProducts() };

            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateColleagueDiscount command)
        {
           var result= _ColleagueDiscountApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var colleaguediscount= _ColleagueDiscountApplication.GetDetails(id);
            colleaguediscount.Products = _productApplication.GetProducts();
            return Partial("./Edit", colleaguediscount);
        }
        public JsonResult OnPostEdit(EditColleagueDiscount command)
        {
            var result = _ColleagueDiscountApplication.Edit(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetRemove(long id)
        {
            var result = _ColleagueDiscountApplication.Remove(id);
            return RedirectToPage("./index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result=_ColleagueDiscountApplication.Restore(id);
            return RedirectToPage("./index");
        }

        
    }
}
