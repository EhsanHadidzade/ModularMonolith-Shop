using _01_TajhizMiningQuery.Contracts.Comment;
using _01_TajhizMiningQuery.Contracts.ProductPicture;
using SM.Domain.ShopManagement.Domain.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_TajhizMiningQuery.Contracts.Product
{
    public class ProductQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public double Price { get; set; }
        public string PriceWithDiscount { get; set; }
        public double OldPrice { get; set; }
        public int DiscountRate { get; set; }
        public bool InStock { get; set; }

        public string Category { get; set; }
        public string CategorySlug { get; set; }
        public long CategoryId { get; set; }

        public bool HasDiscount { get; set; }
        public string DiscountExpireDate { get; set; }

        //SEO Operation
        public string Slug { get; set; }
        public string KeyWords { get; set; }
        public string MetaDescription { get; set; }

        //productpictures
        public List<ProductPictureQueryModel> ProductPictures { get; set; }

        //ProductComments
        public List<CommentQueryModel> Comments { get; set; }
    }
}
