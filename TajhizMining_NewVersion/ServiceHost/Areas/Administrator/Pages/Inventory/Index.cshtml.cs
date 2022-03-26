using InventoryManegement.Application.Contract.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administrator.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }


        [BindProperty]
        public InventorySearchModel searchmodel { get; set; }


        public List<InventoryViewModel> Inventories { get; set; }

        public SelectList Products { get; set; }

        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;

        public IndexModel(IProductApplication productApplication, IInventoryApplication inventoryApplication)
        {
            _productApplication = productApplication;
            _inventoryApplication = inventoryApplication;
        }

        public void OnGet(InventorySearchModel searchmodel)
        {

            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            Inventories = _inventoryApplication.Search(searchmodel);
        }
        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory() { Products = _productApplication.GetProducts() };

            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateInventory command)
        {
            var result = _inventoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var Inventory = _inventoryApplication.GetDetails(id);
            Inventory.Products = _productApplication.GetProducts();
            return Partial("./Edit", Inventory);
        }
        public JsonResult OnPostEdit(EditInventory command)
        {
            var result = _inventoryApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetIncrease(long id)
        {
            var command = new IncreaseInventory()
            {
                InventoryId = id
            };
            return Partial("Increase",command);
        }
        public JsonResult OnPostIncrease(IncreaseInventory command)
        {
          var result= _inventoryApplication.Increase(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetReduce(long id)
        {
            var command = new ReduceInventory()
            {
                InventoryId = id
            };
            return Partial("Reduce", command);
        }
        public JsonResult OnPostReduce(ReduceInventory command)
        {
            var result = _inventoryApplication.Reduce(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetOperationLog(long id)
        {
            var operations=_inventoryApplication.GetOperationLog(id);
            return Partial("OperationLog", operations);
        }


        //public IActionResult OnGetRemove(long id)
        //{
        //    var result = _ColleagueDiscountApplication.Remove(id);
        //    return RedirectToPage("./index");
        //}

        //public IActionResult OnGetRestore(long id)
        //{
        //    var result = _ColleagueDiscountApplication.Restore(id);
        //    return RedirectToPage("./index");
        //}


    }
}
