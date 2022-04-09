using ArticleManagement.Application.Contract.Article;
using ArticleManagement.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administrator.Pages.Blog.Article
{
    public class CreateModel : PageModel
    {
        public SelectList ArticleCategories;
        public CreateArticle Command;
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        private readonly IArticleApplication _articleApplication;

        public CreateModel(IArticleCategoryApplication articleCategoryApplication, IArticleApplication articleApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
            _articleApplication = articleApplication;
        }

        public void OnGet()
        {
            ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
        }
        public IActionResult OnPost(CreateArticle command)
        {
            _articleApplication.Create(command);
            return RedirectToPage("./index");
        }
    }
}
