using ArticleManagement.Application.Contract.Article;
using ArticleManagement.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administrator.Pages.Blog.Article
{
    public class IndexModel : PageModel
    {
        public ArticleSearchModel searchModel;
        public SelectList ArticleCategories;
        public List<ArticleViewModel> Articles;
        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;


        public IndexModel(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
        {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
        }

        public void OnGet(ArticleSearchModel searchmodel)
        {
            ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
            Articles = _articleApplication.Search(searchmodel);
        }
    }
}
