using _01_Framework.Application;

namespace ShopManagement.Application.Contracts.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct command);
        EditProduct GetDetails(long id);
        List<ProductViewModel> Search(ProductSearchModel searchmodel);
        List<ProductViewModel> GetProducts();
        OperationResult InStock(long id);
        OperationResult NotInStock(long id);

    }
}
