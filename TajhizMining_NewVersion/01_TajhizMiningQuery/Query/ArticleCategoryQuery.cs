using _01_Framework.Application;
using _01_TajhizMiningQuery.Contracts.Article;
using _01_TajhizMiningQuery.Contracts.ArticleCategory;
using ArticleManagement.Domain.ArticleAgg;
using ArticleManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_TajhizMiningQuery.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly BlogContext _blogContext;

        public ArticleCategoryQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public List<ArticleCategoryQueryModel> GetArticleCategoriesForBlog()
        {
            var categories = _blogContext.ArticleCategories.Select(x => new ArticleCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                KeyWords=x.KeyWords
            }).ToList();

            categories.ForEach(x => x.KeywordsList = x.KeyWords.Split("-").Take(3).ToList());

            return categories;
        }

        public List<ArticleCategoryQueryModel> GetArticleCategoryNames()
        {
            var categories= _blogContext.ArticleCategories.Select(x => new ArticleCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
            }).ToList();

            return categories;
        }

        public ArticleCategoryQueryModel GetCategoryByCategorySlug(string categorySlug)
        {
            var category= _blogContext.ArticleCategories.Select(x=>new ArticleCategoryQueryModel
            {
                Id = x.Id,
                Name=x.Name,
                Slug = x.Slug,
                KeyWords = x.KeyWords,
                MetaDescription= x.MetaDescription,
            }).FirstOrDefault(x=>x.Slug == categorySlug);
            category.KeywordsList = category.KeyWords.Split("-").ToList();

            if (category == null)
                throw new Exception();

            return category;
        }


    }
}
