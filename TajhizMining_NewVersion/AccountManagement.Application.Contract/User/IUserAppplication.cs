using _01_Framework.Application;

namespace AccountManagement.Application.Contract.User
{
    public interface IUserAppplication
    {
        OperationResult Create(CreateUser command);
        OperationResult Edit(EditUser command);
        OperationResult ChangePassword(ChangePassword command);
        List<UserViewModel> Search(UserSearchModel searchModel);
        EditUser GetDetails(long id);
    }
}
