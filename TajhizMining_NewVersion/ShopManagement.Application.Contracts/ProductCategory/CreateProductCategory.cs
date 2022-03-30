using _01_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SM.Application.ShopManagement.Application.Contracts.ProductCategory
{
    public class CreateProductCategory
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Name { get;  set; }

        public string? Description { get;  set; }

        [MaxFileSize(5*1024*1024, ErrorMessage = ValidationMessage.MaxFileSize)]
        [FileExtentionLimitation(new string[] { ".jpeg", ".png", ".jpg" },ErrorMessage =ValidationMessage.InvalidFileFormat)]
        public IFormFile? Picture { get;  set; }

        public string? PictureAlt { get;  set; }
        public string? PictureTitle { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Keyword { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? MetaDescription { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Slug { get;  set; }
    }
}
