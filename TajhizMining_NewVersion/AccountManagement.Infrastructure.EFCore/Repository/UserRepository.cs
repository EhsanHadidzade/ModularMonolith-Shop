using _01_Framework.Application;
using _01_Framework.Infrastructure;
using AccountManagement.Application.Contract.User;
using AccountManagement.Domain.UserAgg;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class UserRepository : RepositoryBase<long, User>, IUserRepository
    {
        private readonly AccountContext _context;

        public UserRepository(AccountContext context):base(context)
        {
            _context = context;
        }

        public EditUser GetDetails(long id)
        {
            var user=_context.Users.Select(u => new EditUser
            {
                Id = id,
                Fullname = u.Fullname,
                Username = u.Username,
                Address = u.Address,
                Mobile = u.Mobile,
                RoleId = u.RoleId,
                Email=u.Email
            }).FirstOrDefault(x=>x.Id == id);

            if (user == null)
                throw new ArgumentNullException();

            return user;
        }

        public List<UserViewModel> Search(UserSearchModel searchModel)
        {
            var query = _context.Users.Select(u => new UserViewModel
            {
                Id = u.Id,
                Fullname = u.Fullname,
                UserName = u.Username,
                Address=u.Address,
                ProfilePhoto = u.ProfilePhoto,
                Mobile = u.Mobile,
                Email=u.Email,
                CreationDate=u.CretionDate.ToFarsi(),
                Role="نقش"
            });

            if(!string.IsNullOrWhiteSpace(searchModel.UserName))
                query=query.Where(x=>x.UserName.Contains(searchModel.UserName));

            if (!string.IsNullOrWhiteSpace(searchModel.FullName))
                query = query.Where(x => x.Fullname.Contains(searchModel.FullName));

            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
                query = query.Where(x => x.Mobile.Contains(searchModel.Mobile));

            if (!string.IsNullOrWhiteSpace(searchModel.Address))
                query = query.Where(x => x.Address.Contains(searchModel.Address));

            return query.OrderByDescending(x=>x.Id).ToList();
        }
    }
}
