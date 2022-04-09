using _01_TajhizMiningQuery.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class HeaderMenuViewComponent :ViewComponent
    {
        private readonly IArticleCategoryQuery _articleCategoryQuery;

        public HeaderMenuViewComponent (IArticleCategoryQuery articleCategoryQuery)
        {
            _articleCategoryQuery = articleCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var categories=_articleCategoryQuery.GetArticleCategoryNames();
            return View(categories);
        }
    }
}
