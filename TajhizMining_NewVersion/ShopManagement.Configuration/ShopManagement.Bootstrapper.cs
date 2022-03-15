using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.Product;
using ShopManagement.Domain.ProductPicture;
using ShopManagement.Domain.Slide;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repository;
using SM.Application.ShopManagement.Application;
using SM.Application.ShopManagement.Application.Contracts.ProductCategory;
using SM.Domain.ShopManagement.Domain.ProductCategory;

namespace ShopManagement.Configuration
{
    public class ShopManagement
    { 
        public static void Configure(IServiceCollection services,string connectionstring)
        {
            services.AddTransient<IProductCategoryRepository,ProductCategoryRepository>();
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductApplication, ProductApplication>();

            services.AddTransient<IProductPictureRepository,ProductPictureRepository>();
            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();

            services.AddTransient<ISlideRepository, SlideRepository>();
            services.AddTransient<ISlideApplication, SlideApplication>();


            services.AddDbContext<TajhizMiningContext>(x => x.UseSqlServer(connectionstring));
        }
    }
}
