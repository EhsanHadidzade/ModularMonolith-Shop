using _01_Framework.Application;

namespace ArticleManagement.Application.Contract.ArticleCategory
{
    public interface IArticleCategoryApplication
    {
        OperationResult Create(CreateArticleCategory command);
        OperationResult Edit(EditArticleCategory command);
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
        EditArticleCategory GetDetails(long id);

        //To Use in ArticleCreate Form
        List<ArticleCategoryViewModel> GetArticleCategories();

    }
}
