using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_TajhizMiningQuery.Contracts.Article
{
    public interface IArticleQuery
    {
        //To Get 4 Latest Articles to use  In home page and in blogIndex
        List<ArticleQueryModel> GetLatestArticles();


        //To get all articles in blog/index also can be filtered by categoryslug or searchvalue
        List<ArticleQueryModel> GetFilteredArticles(string categorySlug, string searchValue, string Keyword);


        //To Get The Article Details in article single page
        ArticleQueryModel GetArticleDetailsByArticleSlug(string articleSlug);

        //to get related articles with category slug to show in article single page
        List<ArticleQueryModel> GetRelatedArticlesByCategorySlug(string categorySlug);

        


    }
}
