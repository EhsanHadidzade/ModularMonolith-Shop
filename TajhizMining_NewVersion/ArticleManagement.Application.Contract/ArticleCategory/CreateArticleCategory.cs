using _01_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ArticleManagement.Application.Contract.ArticleCategory
{
    public class CreateArticleCategory
    {
        [Required(ErrorMessage =ValidationMessage.IsRequired)]
        public string Name { get; set; }

        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSize)]
        [FileExtentionLimitation(new string[] { ".jpg", ".jpeg", "png" }, ErrorMessage = ValidationMessage.InvalidFileFormat)]
        public IFormFile? Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Description { get; set; }

        public int ShowOrder { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Slug { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string KeyWords { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string MetaDescription { get; set; }
        public string? CanonicalAddress { get; set; }

    }
}
