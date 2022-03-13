using _01_Framework.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Application.ShopManagement.Application.Contracts.ProductCategory
{
    public class CreateProductCategory
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Name { get;  set; }

        public string? Description { get;  set; }
        public string? Picture { get;  set; }
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
