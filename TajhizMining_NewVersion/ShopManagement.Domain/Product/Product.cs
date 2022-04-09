using _01_Framework.Domain;
using SM.Domain.ShopManagement.Domain.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.Product
{
    public class Product:EntityBase
    {
        public string Name { get; private set; }
        public double UnitPrice { get; private set; }
        public string Code { get; private set; }
        public bool IsInStock { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }

        //SEO Operation
        public string Slug { get; private set; }
        public string KeyWords { get; private set; }
        public string MetaDescription { get; private set; }

        //Relation with productcategory
        public long CateforyId { get; private set; }
        public ProductCategory ProductCategory { get; private set; }

        //relation with ProductPicture
        public List<ProductPicture.ProductPicture> ProductPictures { get; private set; }


        public Product(string name, double unitPrice, string code, string shortDescription, string description,
            string picture, string pictureAlt, string pictureTitle, string slug, string keyWords, 
            string metaDescription, long cateforyId)
        {
            Name = name;
            UnitPrice = unitPrice;
            Code = code;
            ShortDescription = shortDescription;
            Description = description;

            if(!string.IsNullOrEmpty(picture))
            Picture = picture;

            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            KeyWords = keyWords;
            MetaDescription = metaDescription;
            CateforyId = cateforyId;
            IsInStock = true;
        }

        public void Edit(string name, double unitPrice, string code, string shortDescription, string description,
            string picture, string pictureAlt, string pictureTitle, string slug, string keyWords,
            string metaDescription, long cateforyId)
        {
            Name = name;
            UnitPrice = unitPrice;
            Code = code;
            ShortDescription = shortDescription;
            Description = description;

            if(!string.IsNullOrWhiteSpace(picture))
            Picture = picture;

            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            KeyWords = keyWords;
            MetaDescription = metaDescription;
            CateforyId = cateforyId;
        }

        public void InStock()
        {
            IsInStock = true;
        }
        public void NotInStock()
        {
            IsInStock = false;
        }
    }
}
