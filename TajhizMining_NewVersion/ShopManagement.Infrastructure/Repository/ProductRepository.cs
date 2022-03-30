using _01_Framework.Application;
using _01_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.Product;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductRepository : RepositoryBase<long, Product>, IProductRepository
    {
        private readonly TajhizMiningContext _context;
        public ProductRepository(TajhizMiningContext context) : base(context)
        {
            _context = context;
        }

      

        public EditProduct GetDetails(long id)
        {
            return _context.Products.Select(p => new EditProduct
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Code = p.Code,
                KeyWords = p.KeyWords,
                MetaDescription = p.MetaDescription,
                Slug = p.Slug,
                CategoryId = p.CateforyId,
                //Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                ShortDescription = p.ShortDescription,
                UnitPrice = p.UnitPrice
            }).FirstOrDefault(p => p.Id == id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _context.Products.Select(x => new ProductViewModel
            {
                Id=x.Id,
                Name=x.Name
            }).ToList();
        }

        public Product GetProductWithCategoryByProductId(long productId)
        {
            return _context.Products.Include(x=>x.ProductCategory).FirstOrDefault(x=>x.Id==productId);
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var query = _context.Products.Include(x => x.ProductCategory).Select(p => new ProductViewModel
            {
                Id = p.Id,
                CategoryId = p.CateforyId,
                Code = p.Code,
                Name = p.Name,
                Picture = p.Picture,
                ProductCategory = p.ProductCategory.Name,
                UnitPrice = p.UnitPrice,
                IsInStock=p.IsInStock,
                CreationDate=p.CretionDate.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(p => p.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Code))
                query = query.Where(p => p.Code.Contains(searchModel.Code));

            if (searchModel.CategoryId != 0)
                query = query.Where(p => p.CategoryId == searchModel.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
