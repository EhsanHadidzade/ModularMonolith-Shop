using _01_TajhizMiningQuery.Contracts.Article;
using _01_TajhizMiningQuery.Contracts.ArticleCategory;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages.Blog
{
    public class BlogDetailsModel : PageModel
    {
        public List<ArticleCategoryQueryModel> Categories;
        public ArticleQueryModel Article;
        public List<ArticleQueryModel> LatestArticles;
        public List<ArticleQueryModel> RelatedArticles;
        public List<string> Tags;

        [BindProperty]
        public AddComment Command { get; set; }
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly IArticleQuery _articleQuery;
        private readonly ICommentApplication _commentApplication;

        public BlogDetailsModel(IArticleCategoryQuery articleCategoryQuery, IArticleQuery articleQuery, ICommentApplication commentApplication)
        {
            _articleCategoryQuery = articleCategoryQuery;
            _articleQuery = articleQuery;
            _commentApplication = commentApplication;
        }
        public void OnGet(string id)
        {

            //id in here is the categorySlug that we want use to filter specific articles

            Categories = _articleCategoryQuery.GetArticleCategoriesForBlog();

            Article = _articleQuery.GetArticleDetailsByArticleSlug(id);

            LatestArticles = _articleQuery.GetLatestArticles();
            
            RelatedArticles=_articleQuery.GetRelatedArticlesByCategorySlug(Article.CategorySlug).Where(x=>x.Id!=Article.Id).ToList();
        }

        public IActionResult OnPost( string articleslug)
        {

            Command.Type = CommentType.Article;
            var result = _commentApplication.Add(Command);

            if (!result.IsSuccedded)
                throw new Exception();

            return RedirectToPage("./blogdetails", new { id = articleslug });
        }
    }
}
