using _01_Framework.Application;
using _01_Framework.Infrastructure;
using ArticleManagement.Application.Contract.ArticleCategory;
using ArticleManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;

namespace ArticleManagement.Infrastructure.EFCore.Repository
{
    public class ArticleCategoryRepository : RepositoryBase<long, ArticleCategory>, IArticleCategoryRepository
    {
        private readonly BlogContext _context;

        public ArticleCategoryRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _context.ArticleCategories.Select(x => new ArticleCategoryViewModel 
            { Id = x.Id, Name = x.Name
            }).ToList();
        }

        public string GetCategorySlugByCategoryId(long categoryId)
        {
            var category= _context.ArticleCategories.Select(x => new { x.Id, x.Slug }).FirstOrDefault(x => x.Id == categoryId);
            if (category == null)
                return ApplicationMessage.RecordNotFound;

            return category.Slug;

        }

        public EditArticleCategory GetDetails(long id)
        {
            var articlecategory = _context.ArticleCategories.Select(x => new EditArticleCategory
            {
                Id = id,
                CanonicalAddress = x.CanonicalAddress,
                Description = x.Description,
                KeyWords = x.KeyWords,
                MetaDescription = x.MetaDescription,
                Name = x.Name,
                ShowOrder = x.ShowOrder,
                Slug = x.Slug,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
            }).FirstOrDefault(x => x.Id == id);

            if (articlecategory == null)
                throw new ArgumentNullException();

            return articlecategory;
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            var query = _context.ArticleCategories.Include(x=>x.Articles).Select(x => new ArticleCategoryViewModel
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
                Picture = x.Picture,
                ShowOrder = x.ShowOrder,
                CreationDate = x.CretionDate.ToFarsi(),
                ArticleCount=x.Articles.Count,
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
