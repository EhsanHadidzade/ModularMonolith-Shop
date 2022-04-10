using AccountManagement.Application.Contract.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administrator.Pages.Account.User
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        public UserSearchModel searchmodel;
        public List<UserViewModel> Users;
        public SelectList ProductCategories;

        private readonly IUserAppplication _userAppplication;
        public IndexModel(IUserAppplication userAppplication)
        {
            _userAppplication = userAppplication;
        }
        public void OnGet(UserSearchModel searchmodel)
        {
            Users = _userAppplication.Search(searchmodel);
        }
        public IActionResult OnGetCreate()
        {
            var command = new CreateUser { };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateUser command)
        {
            var result = _userAppplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var user = _userAppplication.GetDetails(id);
            return Partial("./Edit", user);
        }
        public JsonResult OnPostEdit(EditUser command)
        {
            var result = _userAppplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetChangePassword(long id)
        {
            var changePassword = new ChangePassword { Id = id };
            return Partial("./ChangePassword", changePassword);
        }
        public JsonResult OnPosChangePassword(ChangePassword command)
        {
            var result = _userAppplication.ChangePassword(command);
            return new JsonResult(result);
        }


    }
}
