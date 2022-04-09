using _01_TajhizMiningQuery.Contracts.Product;

namespace _01_TajhizMiningQuery.Contracts.ProductCategory
{
    public interface IproductCategoryQuery
    {
        List<ProductCategoryForHeaderQueryModel> GetProductCategoriesForHeader();
        List<ProductCategoryForBodyQueryModel> GetProductCategoriesForBody();

        //To Use Product Categories In Shop Page
        List<ProductCategoryForShopQueryModel> GetAllCategoriesForShop();

         ProductCategoryForShopQueryModel GetCategoryByCategorySlug(string categorySlug);




    }
}
