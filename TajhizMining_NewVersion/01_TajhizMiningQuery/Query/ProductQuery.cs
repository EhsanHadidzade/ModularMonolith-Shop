using _01_Framework.Application;
using _01_TajhizMiningQuery.Contracts.Comment;
using _01_TajhizMiningQuery.Contracts.Product;
using _01_TajhizMiningQuery.Contracts.ProductPicture;
using CommentManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductPicture;
using ShopManagement.Infrastructure.EFCore;

namespace _01_TajhizMiningQuery.Queryt
{
    public class ProductQuery : IProductQuery
    {
        private readonly TajhizMiningContext _context;
        private readonly InventoryContext _inventorycontex;
        private readonly DiscountContext _discountContext;
        private readonly CommentContext _commentContext;

        public ProductQuery(TajhizMiningContext context, InventoryContext inventorycontext, DiscountContext discountcontext, CommentContext commentContext)
        {
            _context = context;
            _inventorycontex = inventorycontext;
            _discountContext = discountcontext;
            _commentContext = commentContext;
        }

        public List<ProductQueryModel> GetFilteredProducts(string categorySlug, double initPrice, double finalPrice, string searchValue,string keyword)
        {
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();

            var query = _context.Products.Include(x => x.ProductCategory).Select(p => new ProductQueryModel
            {
                Id = p.Id,
                Name = p.Name,
                CategoryId = p.CateforyId,
                Category = p.ProductCategory.Name,
                CategorySlug = p.ProductCategory.Slug,
                ShortDescription = p.ShortDescription,
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                Slug = p.Slug,
                Price = p.UnitPrice,
                OldPrice = p.UnitPrice,
                KeyWords = p.KeyWords,
                HasDiscount = false
            }).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(categorySlug))
                query = query.Where(x => x.CategorySlug == categorySlug);


            if (!string.IsNullOrWhiteSpace(searchValue))
                query = query.Where(x => x.Name.Contains(searchValue) || x.ShortDescription.Contains(searchValue));


            if (!string.IsNullOrWhiteSpace(keyword))
                query = query.Where(x => x.KeyWords.Contains(keyword));


            var products = query.ToList();

            foreach (var product in products)
            {
                var productdiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (productdiscount != null)
                {

                    product.HasDiscount = true;
                    product.DiscountRate = productdiscount.DiscountRate;
                    var discountamount = Math.Round((product.Price * productdiscount.DiscountRate) / 100);
                    product.Price = product.OldPrice - discountamount;
                }
            }
            if (initPrice > 0 && finalPrice > 0)
            {
                return products.Where(x => x.Price > initPrice && x.Price < finalPrice).ToList();
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
            var discounts = _discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();
            var products = _context.Products.Include(x => x.ProductCategory).Select(p => new ProductQueryModel
            {
                Id = p.Id,
                Name = p.Name,
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                Slug = p.Slug,
                Price = p.UnitPrice,
                OldPrice=p.UnitPrice
            }).AsNoTracking().OrderByDescending(x => x.Id).Take(5).ToList();

            foreach (var product in products)
            {
                var productdiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (productdiscount != null)
                {
                    product.HasDiscount = true;
                    product.DiscountRate = productdiscount.DiscountRate;
                    var discountamount = Math.Round((product.Price * productdiscount.DiscountRate) / 100);
                    product.Price = product.OldPrice - discountamount;

                }
            }
            return products;
        }

        public ProductQueryModel GetProductDetailsByProductSlug(string ProductSlug)
        {
            var product = _context.Products.Include(x => x.ProductCategory).Include(x => x.ProductPictures).Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.ProductCategory.Name,
                CategoryId = x.ProductCategory.Id,
                CategorySlug = x.ProductCategory.Slug,
                Description = x.Description,
                ShortDescription = x.ShortDescription,
                MetaDescription = x.MetaDescription,
                KeyWords = x.KeyWords,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Price = x.UnitPrice,
                OldPrice = x.UnitPrice,
                Slug = x.Slug,
                HasDiscount = false,
                ProductPictures = MapProductPicture(x.ProductPictures),

            }).FirstOrDefault(x => x.Slug == ProductSlug);

            if (product == null)
            {
                throw new ArgumentNullException();
            }

            //ToBring Comments With Products
            product.Comments = _commentContext.Comments.Where(c => c.IsConfirmed)
                .Where(c => c.Type == CommentType.Product)
                .Where(c => c.OwenerRecordId == product.Id)
                .Select(c => new CommentQueryModel
                {
                    Id = c.Id,
                    Message = c.Message,
                    Name = c.Name,

                }).ToList();

            
            var productdiscount = _discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Select(x => new { x.DiscountRate, x.EndDate, x.ProductId }).FirstOrDefault(x => x.ProductId == product.Id);
            var productinventory = _inventorycontex.Inventories.Select(x => new { x.InStock, x.ProductId }).FirstOrDefault(x => x.ProductId == product.Id);

            if (productinventory != null)
            {
                product.InStock = productinventory.InStock;
            }

            if (productdiscount != null)
            {
                
                product.DiscountRate = productdiscount.DiscountRate;
                product.HasDiscount = true;
                product.DiscountExpireDate = productdiscount.EndDate.ToDiscountFormat();

                //Calculating Discount Amount
                var discountamount = Math.Round((product.Price * productdiscount.DiscountRate) / 100);
                product.Price = product.OldPrice - discountamount;

            }

            return product;

        }
        private static List<ProductPictureQueryModel> MapProductPicture(List<ProductPicture> pictures)
        {
            return pictures.Select(x => new ProductPictureQueryModel
            {
                IsRemoved = x.IsRemoved,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId,
            }).ToList();
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
                Price = p.UnitPrice,
                OldPrice=p.UnitPrice
            }).AsNoTracking().ToList();

            foreach (var product in products)
            {
                var productdiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (productdiscount != null)
                {
                    
                    product.HasDiscount = true;
                    product.DiscountRate = productdiscount.DiscountRate;
                    product.DiscountExpireDate = productdiscount.EndDate.ToDiscountFormat();
                    var discountamount = Math.Round((product.Price * productdiscount.DiscountRate) / 100);
                    product.Price = product.Price - discountamount;

                }
            }
            return products.Where(x => x.HasDiscount).OrderByDescending(x => x.Id).Take(5).ToList();

        }

        public List<ProductQueryModel> GetRelatedProductsByCategorySlug(string categorySlug)
        {
            var discounts = _discountContext.CustomerDiscounts
               .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
               .Select(x => new { x.DiscountRate, x.ProductId, x.EndDate }).ToList();

            var products = _context.Products.Include(x => x.ProductCategory).Where(x=>x.ProductCategory.Slug==categorySlug).Select(p => new ProductQueryModel
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.ProductCategory.Name,
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                Slug = p.Slug,
                Price = p.UnitPrice,
                OldPrice = p.UnitPrice
            }).AsNoTracking().ToList();


            foreach (var product in products)
            {
                var productdiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (productdiscount != null)
                {

                    product.HasDiscount = true;
                    product.DiscountRate = productdiscount.DiscountRate;
                    product.DiscountExpireDate = productdiscount.EndDate.ToDiscountFormat();
                    var discountamount = Math.Round((product.Price * productdiscount.DiscountRate) / 100);
                    product.Price = product.OldPrice - discountamount;

                }
            }

            return products;
        }
    }
}
