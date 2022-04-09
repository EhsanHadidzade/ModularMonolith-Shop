using _01_TajhizMiningQuery.Contracts.Comment;
using _01_TajhizMiningQuery.Query;
using CommentManagement.Application;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.Comment;
using CommentManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommentManagement.Configuration
{
    public class CommentManagementBootstrapper
    {
        public static void Configure(IServiceCollection service,string conntectionstring)
        {
            service.AddTransient<ICommentApplication, CommentApplication>();
            service.AddTransient<ICommentRepository, CommentRepository>();


            service.AddTransient<ICommentQuery, CommetQuery>();


            service.AddDbContext<CommentContext>(item => item.UseSqlServer(conntectionstring));
        }
    }
}
