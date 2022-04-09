using ArticleManagement.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administrator.Pages.Blog.ArticleCategory
{
    public class IndexModel : PageModel
    {
        public ArticleCategorySearchModel SearchModel;
        public List<ArticleCategoryViewModel> ArticleCategories { get; set; }

        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public IndexModel(IArticleCategoryApplication articleCategoryApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
        }

        public void OnGet(ArticleCategorySearchModel searchmodel)
        {
            ArticleCategories = _articleCategoryApplication.Search(searchmodel);
        }
        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateArticleCategory());
        }

        public JsonResult OnPostCreate(CreateArticleCategory command)
        {
            var result = _articleCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        public PartialViewResult OnGetEdit(long id)
        {
            var articleCategory = _articleCategoryApplication.GetDetails(id);
            return Partial("./Edit", articleCategory);
        }
        public JsonResult OnPostEdit(EditArticleCategory command)
        {
            var result = _articleCategoryApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
