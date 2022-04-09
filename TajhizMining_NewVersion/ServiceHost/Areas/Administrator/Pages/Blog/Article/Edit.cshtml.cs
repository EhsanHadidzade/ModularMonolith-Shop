using ArticleManagement.Application.Contract.Article;
using ArticleManagement.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administrator.Pages.Blog.Article
{
    public class EditModel : PageModel
    {
        public EditArticle Command;
        public SelectList ArticleCategories;
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        private readonly IArticleApplication _articleApplication;

        public EditModel(IArticleCategoryApplication articleCategoryApplication, IArticleApplication articleApplication)
        {
            _articleCategoryApplication = articleCategoryApplication; 
            _articleApplication = articleApplication;
        }

        public void OnGet(long id)
        {
            ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
           Command=_articleApplication.GetDetails(id);
        }
        public IActionResult OnPost(EditArticle command)
        {
            _articleApplication.Edit(command);
            return RedirectToPage("./index");
        }
        
    }
}
