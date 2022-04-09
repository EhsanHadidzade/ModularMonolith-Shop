using _01_Framework.Domain;
using ArticleManagement.Application.Contract.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleManagement.Domain.ArticleAgg
{
    public interface IArticleRepository:IRepository<long,Article>
    {
        EditArticle GetDetails(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
        Article GetArticleWithCategory(long ArticleId);
    }
}
