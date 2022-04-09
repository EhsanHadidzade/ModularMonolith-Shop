using _01_Framework.Domain;
using ArticleManagement.Application.Contract.ArticleCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository:IRepository<long,ArticleCategory>
    {
        
        EditArticleCategory GetDetails(long id);
        string GetCategorySlugByCategoryId(long categoryId);
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);

        //To use for CreateArticle Form
        List<ArticleCategoryViewModel> GetArticleCategories();
    }
}
