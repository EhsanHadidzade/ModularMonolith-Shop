using _01_Framework.Application;
using _01_TajhizMiningQuery.Contracts.Product;
using _01_TajhizMiningQuery.Contracts.ProductCategory;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
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
        private readonly DiscountContext _discountContext;
        private readonly InventoryContext _inventorycontext;

        public productCategoryQuery(TajhizMiningContext context, DiscountContext discountContext, InventoryContext inventorycontext)
        {
            _context = context;
            _discountContext = discountContext;
            _inventorycontext = inventorycontext;
        }

        public List<ProductCategoryForShopQueryModel> GetAllCategoriesForShop()
        {
            var categories = _context.ProductCategories.OrderByDescending(x => x.Id).Select(x => new ProductCategoryForShopQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Keywords=x.Keyword
            }).ToList();

            categories.ForEach(x => x.KeywordsList = x.Keywords.Split("-").Take(3).ToList());

            return categories;

        }

        public ProductCategoryForShopQueryModel GetCategoryByCategorySlug(string categorySlug)
        {
            var category = _context.ProductCategories.Select(x => new ProductCategoryForShopQueryModel
            {
                Id = x.Id,
                Slug = x.Slug,
                Name = x.Name,
                Keywords = x.Keyword,
                MetaDescription = x.MetaDescription,
            }).FirstOrDefault(x => x.Slug == categorySlug);

            if (category == null)
                throw new Exception();

            category.KeywordsList = category.Keywords.Split("-").ToList();

            return category;
        }

        public List<ProductCategoryForBodyQueryModel> GetProductCategoriesForBody()
        {
            return _context.ProductCategories.Select(x => new ProductCategoryForBodyQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle
            }).OrderByDescending(x => x.Id).Take(5).ToList();
        }
        public List<ProductCategoryForHeaderQueryModel> GetProductCategoriesForHeader()
        {
            return _context.ProductCategories.Select(x => new ProductCategoryForHeaderQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug
            }).OrderByDescending(x => x.Id).ToList();
        }

        
    }
}
