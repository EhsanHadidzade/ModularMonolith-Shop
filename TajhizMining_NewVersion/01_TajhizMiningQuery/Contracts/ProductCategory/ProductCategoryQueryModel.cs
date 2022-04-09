using _01_TajhizMiningQuery.Contracts.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_TajhizMiningQuery.Contracts.ProductCategory
{
    public class ProductCategoryForHeaderQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }
    public class ProductCategoryForBodyQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }

    }
    public class ProductCategoryForShopQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public List<string> KeywordsList { get; set; }
    }
}
