using CommentManagement.Application.Contract.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administrator.Pages.Comment
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<CommentViewModel> Comments { get; set; }

        public CommentSearchModel SearchModel;

        private readonly ICommentApplication _commentApplication;

        public IndexModel(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }

        public void OnGet(CommentSearchModel searchModel)
        {
            Comments = _commentApplication.Search(searchModel);
        }

        //here we get the comment id to confirm it
        public RedirectToPageResult OnGetConfirm(long id)
        {
            var result = _commentApplication.Confirm(id);
            if (result.IsSuccedded)
                return RedirectToPage("./index");

            Message = result.Message;
            return RedirectToPage("./index");
        }
        public RedirectToPageResult OnGetCancel(long id)
        {
            var result = _commentApplication.Cancel(id);
            if (result.IsSuccedded)
                return RedirectToPage("./index");

            Message = result.Message;
            return RedirectToPage("./index");
        }

    }
}
