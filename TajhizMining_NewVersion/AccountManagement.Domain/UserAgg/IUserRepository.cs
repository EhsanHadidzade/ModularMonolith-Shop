using _01_Framework.Domain;
using AccountManagement.Application.Contract.User;

namespace AccountManagement.Domain.UserAgg
{
    public interface IUserRepository:IRepository<long,User>
    {
        List<UserViewModel> Search(UserSearchModel searchModel);
        EditUser GetDetails(long id);
    }
}
