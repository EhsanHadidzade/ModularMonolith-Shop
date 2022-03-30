using _01_Framework.Application;
using _01_TajhizMiningQuery.Contracts.Product;
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
    public class ProductQuery : IProductQuery
    {
        private readonly TajhizMiningContext _context;
        private readonly InventoryContext _inventorycontext;
        private readonly DiscountContext _discountContext;


        public ProductQuery(TajhizMiningContext context, InventoryContext inventorycontext, DiscountContext discountcontext)
        {
            _context = context;
            _inventorycontext = inventorycontext;
            _discountContext = discountcontext;
        }

        public List<ProductQueryModel> GetFilteredProducts(string slug, double initPrice, double finalPrice,string searchValue)
        {
            var discounts = _discountContext.CustomerDiscounts.Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();
            var query = _context.Products.Include(x => x.ProductCategory).Select(p => new ProductQueryModel
            {
                Id = p.Id,
                Name = p.Name,
                CategoryId = p.CateforyId,
                Category = p.ProductCategory.Name,
                CategorySlug = p.ProductCategory.Slug,
                Description = p.Description.Substring(2, 180) + "....",
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                Slug = p.Slug,
                Price=p.UnitPrice
            }).AsNoTracking();

            if (slug != null)
            {
                query = query.Where(x => x.CategorySlug == slug);
            }
            if (searchValue != null)
            {
                query=query.Where(x=>x.Name.Contains(searchValue) || x.Description.Contains(searchValue));
            }
          
            var products=query.ToList();

            foreach (var product in products)
            {
                var productdiscount = _discountContext.CustomerDiscounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (productdiscount != null)
                {
                    product.OldPrice = product.Price;
                    product.HasDiscount = true;
                    product.DiscountRate = productdiscount.DiscountRate;
                    var discountamount = Math.Round((product.Price * productdiscount.DiscountRate) / 100);
                    product.Price = product.Price - discountamount;

                }
            }
            if (initPrice > 0 && finalPrice > 0)
            {
                return products.Where(x=>x.Price>initPrice && x.Price< finalPrice).ToList();
            }
            if (initPrice > 0)
            {
                return products.Where(x => x.Price > initPrice).ToList();
            }
            if (finalPrice > 0)
            {
                return products.Where(x => x.Price < finalPrice).ToList();

            }
            return products;

        }

        public List<ProductQueryModel> GetNewProducts()
        {
            var discounts = _discountContext.CustomerDiscounts.Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();
            var products = _context.Products.Include(x => x.ProductCategory).Select(p => new ProductQueryModel
            {
                Id = p.Id,
                Name = p.Name,
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                Slug = p.Slug,
                Price = p.UnitPrice
            }).AsNoTracking().OrderByDescending(x=>x.Id).Take(5).ToList();

            foreach (var product in products)
            {
                var productdiscount = _discountContext.CustomerDiscounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (productdiscount != null)
                {
                    product.OldPrice = product.Price;
                    product.HasDiscount = true;
                    product.DiscountRate = productdiscount.DiscountRate;
                    var discountamount = Math.Round((product.Price * productdiscount.DiscountRate) / 100);
                    product.Price = product.Price - discountamount;

                }
            }
            return products;
        }

        public List<ProductQueryModel> GetSpecialProducts()
        {
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId, x.EndDate }).ToList();

            var products = _context.Products.Include(x => x.ProductCategory).Select(p => new ProductQueryModel
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.ProductCategory.Name,
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                Slug = p.Slug,
                Price = p.UnitPrice
            }).AsNoTracking().ToList();

            foreach (var product in products)
            {
                var productdiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (productdiscount != null)
                {
                    product.OldPrice = product.Price;
                    product.HasDiscount = true;
                    product.DiscountRate = productdiscount.DiscountRate;
                    product.DiscountExpireDate=productdiscount.EndDate.ToDiscountFormat();
                    var discountamount = (product.Price * productdiscount.DiscountRate) / 100;
                    product.Price = product.Price - discountamount;

                }
            }
            return products.Where(x=>x.HasDiscount).OrderByDescending(x=>x.Id).Take(5).ToList();

        }
    }
}
