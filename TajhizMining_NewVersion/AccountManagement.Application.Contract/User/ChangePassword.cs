using _01_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contract.User
{
    public class ChangePassword
    {
        public long Id { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Password { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? RePassword { get; set; }
    }
}
