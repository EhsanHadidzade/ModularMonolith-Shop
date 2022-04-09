using _01_TajhizMiningQuery.Contracts.Article;
using _01_TajhizMiningQuery.Contracts.ArticleCategory;
using _01_TajhizMiningQuery.Query;
using ArticleManagement.Application;
using ArticleManagement.Application.Contract.Article;
using ArticleManagement.Application.Contract.ArticleCategory;
using ArticleManagement.Domain.ArticleAgg;
using ArticleManagement.Domain.ArticleCategoryAgg;
using ArticleManagement.Infrastructure.EFCore;
using ArticleManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ArticleManagement.Configuration
{
    public class ArticleManagementBootstrapper
    {
        public static void Configure(IServiceCollection services,string connectionstring)
        {
            services.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();
            services.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();

            services.AddTransient<IArticleApplication, ArticleApplication>();
            services.AddTransient<IArticleRepository,  ArticleRepository>();

            services.AddTransient<IArticleCategoryQuery, ArticleCategoryQuery>();
            services.AddTransient<IArticleQuery, ArticleQuery>();


            services.AddDbContext<BlogContext>(item => item.UseSqlServer(connectionstring));

        }
    }
}
