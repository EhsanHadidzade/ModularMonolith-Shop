using _01_Framework.Application;
using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contract.User
{
    public class CreateUser
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Fullname { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Username { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Email { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Password { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Mobile { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Address { get;  set; }

        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSize)]
        [FileExtentionLimitation(new string[] { ".jpg", ".jpeg", "png" }, ErrorMessage = ValidationMessage.InvalidFileFormat)]
        public IFormFile? ProfilePhoto { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public long RoleId { get;  set; }
        public List<Roleviewmodel> Roles { get; set; }
    }
}
