using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slide;
using SM.Application.ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administrator.Pages.Shop.Slide
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        public List<SlideViewModel> Slides { get; set; }
        private ISlideApplication _slideApplication;

        public IndexModel(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }

        public void OnGet()
        {
            Slides = _slideApplication.Getlist();
        }
        public IActionResult OnGetCreate()
        {
            var command = new CreateSlide();
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateSlide command)
        {
           var result= _slideApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var Slide=_slideApplication.GetDetail(id);
            return Partial("./Edit", Slide);
        }
        public JsonResult OnPostEdit(EditSlide command)
        {
            var result = _slideApplication.Edit(command);
            return new JsonResult(result);
        }

        public RedirectToPageResult OnGetRemove(long id)
        {
            var result=_slideApplication.Remove(id);
            if (result.IsSuccedded)
            {
                return RedirectToPage("./index");
            }

            Message=result.Message;
            return RedirectToPage("./index");

        }
        public RedirectToPageResult OnGetRestore(long id)
        {
            var result = _slideApplication.Restore(id);
            if (result.IsSuccedded)
            {
                return RedirectToPage("./index");
            }

            Message = result.Message;
            return RedirectToPage("./index");

        }
    }
}