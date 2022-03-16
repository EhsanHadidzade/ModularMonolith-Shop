using _01_TajhizMiningQuery.Contracts.ProductCategory;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_TajhizMiningQuery.Query
{
    public class productCategoryQuery : IproductCategoryQuery
    {
        private readonly TajhizMiningContext _context;

        public productCategoryQuery(TajhizMiningContext context)
        {
            _context = context;
        }

        public List<ProductCategoryForBodyQueryModel> GetProductCategoriesForBody()
        {
            return _context.ProductCategories.Select(x => new ProductCategoryForBodyQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Picture=x.Picture,
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle
            }).OrderByDescending(x=>x.Id).Take(5).ToList();
        }

        public List<ProductCategoryForHeaderQueryModel> GetProductCategoriesForHeader()
        {
            return _context.ProductCategories.Select(x=>new ProductCategoryForHeaderQueryModel
            {
                Id=x.Id,
                Name = x.Name,
                Slug=x.Slug
            }).OrderByDescending(x=>x.Id).ToList();
        }
    }
}
