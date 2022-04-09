using _01_Framework.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleManagement.Application.Contract.Article
{
    public class CreateArticle
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Title { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? ShortDescription { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Description { get; set; }

        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSize)]
        [FileExtentionLimitation(new string[] { ".jpg", ".jpeg", "png" }, ErrorMessage = ValidationMessage.InvalidFileFormat)]
        public IFormFile? Picture { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureAlt { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureTitle { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? PublishDate { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Slug { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Keywords { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? MetaDescription { get;  set; }
        public string? CanonicalAddress { get;  set; }


        [Range(1,long.MaxValue,ErrorMessage =ValidationMessage.IsRequired)]
        public long CategoryId { get; set; }

    }
}
