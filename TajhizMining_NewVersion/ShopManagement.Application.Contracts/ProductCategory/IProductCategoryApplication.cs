using _01_Framework.Application;

namespace SM.Application.ShopManagement.Application.Contracts.ProductCategory
{
    public interface IProductCategoryApplication
    {
        OperationResult Create(CreateProductCategory command);
        OperationResult Edit(EditProductCategory command);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchmodel);
        EditProductCategory GetDetails(long id);
    }
}
