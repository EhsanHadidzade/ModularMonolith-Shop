using _01_Framework.Application;
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

        public string Picture { get; set; }
        public string PictureAlt { get; set; }
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
