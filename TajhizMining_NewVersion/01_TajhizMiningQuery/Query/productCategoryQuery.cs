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
            return _context.ProductCategories.Select(x => new ProductCategoryForShopQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Keywords = x.Keyword,
                Slug = x.Slug
            }).ToList();
        }

        //public List<ProductCategoryForShopQueryModel> GetCategoriesWithProducts()
        //{
        //    var discounts = _discountContext.CustomerDiscounts.Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();
        //    var inventories = _inventorycontext.Inventories.Select(x => new { x.ProductId, x.UnitPrice }).ToList();
        //    var products = _context.Products.Select(p => new ProductQueryModel
        //    {
        //        Id = p.Id,
        //        Name = p.Name,
        //        Category = p.ProductCategory.Name,
        //        Description = p.Description.Substring(2,180)+"....",
        //        Picture = p.Picture,
        //        PictureAlt = p.PictureAlt,
        //        PictureTitle = p.PictureTitle,
        //        Slug = p.Slug,
        //        CategoryId=p.CateforyId
        //    }).ToList();
        //    foreach (var product in products)
        //    {
        //        var productinventory = _inventorycontext.Inventories.FirstOrDefault(x => x.ProductId == product.Id);
        //        if (productinventory != null)
        //        {
        //            product.Price = productinventory.UnitPrice.ToMoney();
        //            var productdiscount = _discountContext.CustomerDiscounts.FirstOrDefault(x => x.ProductId == product.Id);
        //            if (productdiscount != null)
        //            {
        //                product.HasDiscount = true;
        //                product.DiscountRate = productdiscount.DiscountRate;
        //                var discountamount = (productinventory.UnitPrice * productdiscount.DiscountRate) / 100;
        //                product.PriceWithDiscount = (productinventory.UnitPrice - discountamount).ToMoney();
        //            }
        //        }
        //    }
        //    return _context.ProductCategories.Include(x => x.Products).Select(x => new ProductCategoryForShopQueryModel
        //    {
        //        Id = x.Id,
        //        Name = x.Name,
        //        Keywords = x.Keyword,
        //        Slug = x.Slug,
        //        Products = products
        //    }).ToList();

        //}
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
