using _01_Framework.Application;
using _01_TajhizMiningQuery.Contracts.Product;
using _01_TajhizMiningQuery.Contracts.ProductCategory;
using _01_TajhizMiningQuery.Contracts.Slide;
using _01_TajhizMiningQuery.Query;
using _01_TajhizMiningQuery.Queryt;
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
    public class ShopManagementBootstrapper
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

            services.AddTransient<ISlideQuery, SlideQuery>();
            services.AddTransient<IproductCategoryQuery, productCategoryQuery>();
            services.AddTransient<IProductQuery, ProductQuery>();




            services.AddDbContext<TajhizMiningContext>(x => x.UseSqlServer(connectionstring));
        }
    }
}
