using _01_Framework.Application;
using _01_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPicture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductPictureRepository : RepositoryBase<long, ProductPicture>, IProductPictureRepository
    {
        private readonly TajhizMiningContext _context;

        public ProductPictureRepository(TajhizMiningContext context):base(context)
        {
            _context = context;
        }

        public EditProductPicture GetDetails(long id)
        {
            return _context.ProductPictures.Select(p => new EditProductPicture
            {
                Id=p.Id,
                //Picture=p.Picture,
                PictureAlt=p.PictureAlt,
                PictureTitle=p.PictureTitle,
                ProductId=p.ProductId,
            }).FirstOrDefault(x=>x.Id==id);
        }

        public ProductPicture GetProductPictureWithProductAndCategoryById(long id)
        {
            return _context.ProductPictures.Include(c=>c.Product).ThenInclude(x=>x.ProductCategory).FirstOrDefault(x=>x.Id==id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchmodel)
        {
            var query = _context.ProductPictures.Include(x => x.Product).Select(x => new ProductPictureViewModel
            {
                CreationDate = x.CretionDate.ToFarsi(),
                Id = x.Id,
                Picture = x.Picture,
                Product = x.Product.Name,
                ProductId = x.ProductId,
                IsRemoved=x.IsRemoved
            });

            if(searchmodel.ProductId!=0)
                query=query.Where(x=>x.ProductId==searchmodel.ProductId);

            return query.OrderByDescending(x=>x.Id).ToList();
        }
    }
}
