using _01_Framework.Infrastructure;
using SM.Application.ShopManagement.Application.Contracts.ProductCategory;
using SM.Domain.ShopManagement.Domain.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductCategoryRepository : RepositoryBase<long,ProductCategory>, IProductCategoryRepository
    {
        private readonly TajhizMiningContext _Context;

        public ProductCategoryRepository(TajhizMiningContext context):base(context)
        {
            _Context = context;
        }

        public EditProductCategory GetDetails(long id)
        {
            return _Context.ProductCategories.Select(x => new EditProductCategory()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Keyword = x.Keyword,
                MetaDescription = x.MetaDescription,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchmodel)
        {
            var query = _Context.ProductCategories.Select(x => new ProductCategoryViewModel()
            {
                CreationDate = x.CretionDate.ToString(),
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,

            });
            if(!string.IsNullOrWhiteSpace(searchmodel.Name))
            {
                query=query.Where(x=>x.Name == searchmodel.Name);
            }

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
