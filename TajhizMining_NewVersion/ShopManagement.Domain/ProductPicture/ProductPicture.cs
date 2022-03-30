using _01_Framework.Domain;
using ShopManagement.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.ProductPicture
{
    public class ProductPicture:EntityBase
    {
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public bool IsRemoved { get; set; }

        //Relations
        public long ProductId { get; private set; }
        public Product.Product Product { get;private set; }

        public ProductPicture(string picture, string pictureAlt, string pictureTitle, long productId)
        {
            if (!string.IsNullOrEmpty(picture))
                Picture = picture;

            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ProductId = productId;
            IsRemoved = false;
        }
        public void Edit(string picture, string pictureAlt, string pictureTitle, long productId)
        {
            if(!string.IsNullOrEmpty(picture))
            Picture = picture;

            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ProductId = productId;
        }
        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved=false;
        }
    }
}
