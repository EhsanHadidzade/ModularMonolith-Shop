using _01_Framework.Application;
using _01_TajhizMiningQuery.Contracts.Article;
using _01_TajhizMiningQuery.Contracts.Comment;
using ArticleManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_TajhizMiningQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _blogContext;
        private readonly CommentContext _commentContext;

        public ArticleQuery(BlogContext blogContext, CommentContext commentContext)
        {
            _blogContext = blogContext;
            _commentContext = commentContext;
        }

        public ArticleQueryModel GetArticleDetailsByArticleSlug(string articleSlug)
        {
            var article = _blogContext.Articles.Select(x => new ArticleQueryModel
            {
                Id = x.Id,
                Title = x.Title,
                Picture = x.Picture,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                PublishDate = x.PublishDate.ToFarsi(),
                Slug = x.Slug,
                CategoryName = x.Category.Name,
                CategorySlug = x.Category.Slug,
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
            }).FirstOrDefault(x => x.Slug == articleSlug);

            if (article == null)
                throw new ArgumentNullException();

            //ToBring Comments With Products
            article.Comments = _commentContext.Comments
                .Where(c => c.IsConfirmed)
                .Where(c => c.Type == CommentType.Article)
                .Where(c => c.OwenerRecordId == article.Id)
                .Select(c => new CommentQueryModel
                {
                    Id = c.Id,
                    Message = c.Message,
                    Name = c.Name,
                    CreationDate = c.CretionDate.ToFarsi(),
                    ParentId = c.ParentId,

                }).ToList();

            article.KeywordsList = article.Keywords.Split("-").ToList();

            return article;
        }


        public List<ArticleQueryModel> GetFilteredArticles(string categorySlug, string searchValue, string Keyword)
        {
            var query = _blogContext.Articles.Where(x => x.PublishDate < DateTime.Now).OrderByDescending(x => x.PublishDate).Select(x => new ArticleQueryModel
            {
                Title = x.Title,
                Picture = x.Picture,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                PublishDate = x.PublishDate.ToFarsi(),
                Slug = x.Slug,
                CategoryName = x.Category.Name,
                CategorySlug = x.Category.Slug,
                ShortDescription = x.ShortDescription,
                Keywords = x.Keywords,

            });

            if (!string.IsNullOrWhiteSpace(categorySlug))
                query = query.Where(x => x.CategorySlug == categorySlug);

            if (!string.IsNullOrWhiteSpace(searchValue))
                query = query.Where(x => x.Title.Contains(searchValue) || x.ShortDescription.Contains(searchValue));


            //here we search between artiles by categoryKeyword that we get
            if (!string.IsNullOrWhiteSpace(Keyword))
                query = query.Where(x => x.Keywords.Contains(Keyword));

            var articles = query.ToList();

            return articles;
        }

        public List<ArticleQueryModel> GetLatestArticles()
        {
            return _blogContext.Articles.Where(x => x.PublishDate < DateTime.Now).Select(x => new ArticleQueryModel
            {
                Id = x.Id,
                Title = x.Title,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                PublishDate = x.PublishDate.ToFarsi(),
                Slug = x.Slug
            }).OrderByDescending(x => x.Id).Take(4).ToList();


        }

        public List<ArticleQueryModel> GetRelatedArticlesByCategorySlug(string categorySlug)
        {
            return _blogContext.Articles.Where(x => x.Category.Slug == categorySlug).Select(x => new ArticleQueryModel
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                Picture = x.Picture,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                PublishDate = x.PublishDate.ToFarsi()
            }).ToList();
        }
    }
}
