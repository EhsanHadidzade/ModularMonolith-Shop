using _01_Framework.Application;
using Microsoft.AspNetCore.Http;
using SM.Application.ShopManagement.Application.Contracts.ProductCategory;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.Product
{
    public class CreateProduct
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Name { get; set; }

        [Range(0,100000000,ErrorMessage = ValidationMessage.IsRequired)]
        public double UnitPrice { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Code { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Description { get; set; }

        [MaxFileSize(5*1024*1024,ErrorMessage =ValidationMessage.MaxFileSize)]
        [FileExtentionLimitation(new string[] {".jpg",".jpeg","png"},ErrorMessage =ValidationMessage.InvalidFileFormat)]
        public IFormFile? Picture { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureTitle { get; set; }

        //SEO Operation
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Slug { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string KeyWords { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string MetaDescription { get; set; }


        public long CategoryId { get; set; }
        public List<ProductCategoryViewModel>? Categories { get; set; }
    }
}
