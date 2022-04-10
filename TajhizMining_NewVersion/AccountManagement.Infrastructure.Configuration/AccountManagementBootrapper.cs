using AccountManagement.Application;
using AccountManagement.Application.Contract.User;
using AccountManagement.Domain.UserAgg;
using AccountManagement.Infrastructure.EFCore;
using AccountManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountManagement.Infrastructure.Configuration
{
    public  class AccountManagementBootrapper
    {
        public static void Configure(IServiceCollection services, string Connectionstring)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserAppplication, UserApplication>();

            services.AddDbContext<AccountContext>(option=>option.UseSqlServer(Connectionstring));
        }
    }
}