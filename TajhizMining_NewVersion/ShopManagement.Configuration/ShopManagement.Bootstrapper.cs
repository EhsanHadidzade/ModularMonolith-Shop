using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddDbContext<TajhizMiningContext>(x => x.UseSqlServer(connectionstring));
        }
    }
}
