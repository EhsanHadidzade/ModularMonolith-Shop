using _01_Framework.Domain;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ShopManagement.Domain.ProductPicture
{
    public interface IProductPictureRepository:IRepository<long, ProductPicture>
    {
        EditProductPicture GetDetails(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchmodel);
        ProductPicture GetProductPictureWithProductAndCategoryById(long id);
    }
}
