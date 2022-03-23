using DiscountManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore.Repository;
using DiscountManagement.Application;
using DiscountManagement.Application.Contract.CustomerDIscount;
using DiscountManagement.Domain.CustomerDiscount;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DiscountManagement.Domain.ColleagueDiscount;
using DiscountManagement.Application.Contract.ColleagueDiscount;

namespace DiscountManagement.Configuration
{
    public class DiscountManagementBootstrapper
    {
        public static void Configure(IServiceCollection service, string connectionstring)
        {
            service.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();
            service.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();


            service.AddTransient<IColleagueDiscountRepository, ColleagueDiscountRepository>();
            service.AddTransient<IColleagueDiscountApplication, ColleagueDiscountApplication>();


            service.AddDbContext<DiscountContext>(x=>x.UseSqlServer(connectionstring));

        }
    }
}