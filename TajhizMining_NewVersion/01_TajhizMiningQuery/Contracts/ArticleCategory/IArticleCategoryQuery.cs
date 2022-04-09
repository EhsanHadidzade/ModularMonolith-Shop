namespace _01_TajhizMiningQuery.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        List<ArticleCategoryQueryModel> GetArticleCategoryNames();

        //To use Categories For bog/index With Keywords
        List<ArticleCategoryQueryModel> GetArticleCategoriesForBlog();
        ArticleCategoryQueryModel GetCategoryByCategorySlug(string categorySlug);   




    }
}
