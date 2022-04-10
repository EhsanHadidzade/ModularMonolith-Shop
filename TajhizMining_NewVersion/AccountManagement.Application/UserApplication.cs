using _01_Framework.Application;
using AccountManagement.Application.Contract.User;
using AccountManagement.Domain.UserAgg;

namespace AccountManagement.Application
{
    public class UserApplication : IUserAppplication
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IFileUploader _fileUploader;

        public UserApplication(IUserRepository userRepository, IPasswordHasher passwordHasher, IFileUploader fileUploader)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();
            var user=_userRepository.Get(command.Id);
            if(user == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (command.Password != command.Password)
                return operation.Failed(ApplicationMessage.PasswordNotMatch);

            var password=_passwordHasher.Hash(command.Password);
            user.ChangePassword(password);
            _userRepository.Save();
            return operation.Succedded();
            

        }

        public OperationResult Create(CreateUser command)
        {
            var operation = new OperationResult();
            if (_userRepository.IsExists(x => x.Username == command.Username || x.Mobile == command.Mobile))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var path = $"UserProfilePics";
            var picturePath=_fileUploader.Upload(command.ProfilePhoto,path);
            var password = _passwordHasher.Hash(command.Password);
            var user=new User(command.Fullname,command.Email,command.Username,password,command.Mobile,command.Address,picturePath,command.RoleId);
            _userRepository.Create(user);
            _userRepository.Save();
            return operation.Succedded();
               
        }

        public OperationResult Edit(EditUser command)
        {
            var operation = new OperationResult();
            var user = _userRepository.Get(command.Id);
            if(user == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (_userRepository.IsExists(x => (x.Username == command.Username || x.Mobile == command.Mobile) && x.Id!=command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var path = $"UserProfilePics";
            var picturePath = _fileUploader.Upload(command.ProfilePhoto, path);
            user.Edit(command.Fullname,command.Email,command.Username,command.Mobile,command.Address,picturePath, command.RoleId);
            _userRepository.Save();
            return operation.Succedded();

        }

        public EditUser GetDetails(long id)
        {
            return _userRepository.GetDetails(id);
        }
        public List<UserViewModel> Search(UserSearchModel searchModel)
        {
            return _userRepository.Search(searchModel);
        }
    }
}