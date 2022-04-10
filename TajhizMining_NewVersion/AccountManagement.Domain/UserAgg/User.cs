using _01_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Domain.UserAgg
{
    public class User:EntityBase
    {
        public string? Fullname { get; private set; }
        public string? Username { get; private set; }
        public string? Email { get;private set; }
        public string? Password { get; private set; }
        public string? Mobile { get; private set; }
        public string? Address { get; private set; }
        public string? ProfilePhoto { get; private set; }
        public long RoleId { get; private set; }

        public User(string? fullname,string? email, string? username,string password,
            string? mobile, string? address, string? profilePhoto, long roleId)
        {
            Email = email;
            Fullname = fullname;
            Username = username;
            Password = password;
            Mobile = mobile;
            Address = address;

            if(!string.IsNullOrWhiteSpace(profilePhoto))
            ProfilePhoto = profilePhoto;

            RoleId = roleId;
        }
        
        public void Edit(string? fullname,string? email, string? username,
            string? mobile, string? address, string? profilePhoto, long roleId)
        {
            Email = email;
            Fullname = fullname;
            Username = username;
            Mobile = mobile;
            Address = address;

            if (!string.IsNullOrWhiteSpace(profilePhoto))
                ProfilePhoto = profilePhoto;

            RoleId = roleId;
        }

        public void ChangePassword(string password)
        {
            Password= password;
        }
    }
}
