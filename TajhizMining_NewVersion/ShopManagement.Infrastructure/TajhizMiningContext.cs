using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.Product;
using ShopManagement.Infrastructure.EFCore.Mapping;
using SM.Domain.ShopManagement.Domain.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore
{
    public class TajhizMiningContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public TajhizMiningContext(DbContextOptions<TajhizMiningContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(ProductCategoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
