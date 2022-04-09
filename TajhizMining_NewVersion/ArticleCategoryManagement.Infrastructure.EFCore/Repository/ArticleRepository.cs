using _01_Framework.Application;
using _01_Framework.Infrastructure;
using ArticleManagement.Application.Contract.Article;
using ArticleManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleManagement.Infrastructure.EFCore.Repository
{
    public class ArticleRepository : RepositoryBase<long, Article>, IArticleRepository
    {
        private readonly BlogContext _context;

        public ArticleRepository(BlogContext context):base(context)
        {
            _context = context;
        }

        public Article GetArticleWithCategory(long ArticleId)
        {
            var article=_context.Articles.Include(c=>c.Category).FirstOrDefault(x=>x.Id == ArticleId);
            if (article == null)
                throw new ArgumentNullException();

            return article;
        }

        public EditArticle GetDetails(long id)
        {
            var Article=_context.Articles.Select(x=>new EditArticle
            {
                Id=x.Id,
                CategoryId=x.CategoryId,
                Title=x.Title,
                ShortDescription=x.ShortDescription,
                MetaDescription=x.MetaDescription,
                Description=x.Description,
                Slug=x.Slug,
                Keywords=x.Keywords,
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle,
                CanonicalAddress=x.CanonicalAddress,
                PublishDate=x.PublishDate.ToFarsi(),
            }).FirstOrDefault(x=>x.Id==id);

            if (Article == null)
                throw new ArgumentNullException();

            return Article;
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var query = _context.Articles.Include(x => x.Category).Select(x => new ArticleViewModel
            {
                Id = x.Id,
                Category = x.Category.Name,
                CategoryId = x.CategoryId,
                Title = x.Title,
                Picture = x.Picture,
                ShortDescription = x.ShortDescription.Substring(0,Math.Min(x.ShortDescription.Length,80))+"....",
                PublishDate = x.PublishDate.ToFarsi()
            });

            if(!string.IsNullOrWhiteSpace(searchModel.Title))
                query=query.Where(x=>x.Title.Contains(searchModel.Title));

            if(searchModel.CategoryId>0)
                query=query.Where(x=>x.CategoryId==searchModel.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }

    }
}
