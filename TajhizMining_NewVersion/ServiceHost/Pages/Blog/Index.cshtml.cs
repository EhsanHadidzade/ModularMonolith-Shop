using _01_TajhizMiningQuery.Contracts.Article;
using _01_TajhizMiningQuery.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages.Blog
{
    public class IndexModel : PageModel
    {
        public List<ArticleCategoryQueryModel> Categories;
        public List<ArticleQueryModel> Articles;
        public List<ArticleQueryModel> LatestArticles;
        public ArticleCategoryQueryModel? category;
        public List<string> Tags;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly IArticleQuery _articleQuery;

        public IndexModel(IArticleCategoryQuery articleCategoryQuery, IArticleQuery articleQuery)
        {
            _articleCategoryQuery = articleCategoryQuery;
            _articleQuery = articleQuery;
        }

        public void OnGet(string id, string searchValue, string Keyword)
        {
            //id in here is the categorySlug that we want use to filter specific articles
            Categories = _articleCategoryQuery.GetArticleCategoriesForBlog();
            Articles = _articleQuery.GetFilteredArticles(id, searchValue, Keyword);
            LatestArticles = _articleQuery.GetLatestArticles();

            if (!string.IsNullOrEmpty(id))
            {
                ViewData["category"] = _articleCategoryQuery.GetCategoryByCategorySlug(id).Name;
                category = _articleCategoryQuery.GetCategoryByCategorySlug(id);
            }
            
            //To make the keywords list of this page
            Tags = new List<string>();
            if (string.IsNullOrEmpty(id))
            {
                foreach (var item in Categories)
                    Tags.AddRange(item.KeywordsList);
            }
            else
            {
                Tags = _articleCategoryQuery.GetCategoryByCategorySlug(id).KeywordsList;
            }

            if (!string.IsNullOrWhiteSpace(searchValue))
                ViewData["searchValue"] = searchValue;

            if (!string.IsNullOrWhiteSpace(Keyword))
                ViewData["searchValue"] = Keyword;

        }
    }
}
