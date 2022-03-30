using _01_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.Product;
using ShopManagement.Domain.ProductPicture;
using SM.Domain.ShopManagement.Domain.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;
        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IProductRepository productRepository, IFileUploader fileUploader)
        {
            _productPictureRepository = productPictureRepository;
            _productRepository = productRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();
            //if (_productPictureRepository.IsExists(x => x.Picture == command.Picture && x.ProductId==command.ProductId))
            //    return operation.Failed(ApplicationMessage.DuplicatedRecord);

            //About Uploading
            var product=_productRepository.GetProductWithCategoryByProductId(command.ProductId);
            var filepath = $"{product.ProductCategory.Slug}//{product.Slug}";
            var picturePath=_fileUploader.Upload(command.Picture,filepath);

            //creating
            var productpicture=new ProductPicture(picturePath,command.PictureAlt,command.PictureTitle,command.ProductId);
            _productPictureRepository.Create(productpicture);
            _productPictureRepository.Save();

            return operation.Succedded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productpicture=_productPictureRepository.GetProductPictureWithProductAndCategoryById(command.Id);
            if(productpicture==null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            //About Uploading
            var filepath = $"{productpicture.Product.ProductCategory.Slug}//{productpicture.Product.Slug}";
            var picturepath=_fileUploader.Upload(command.Picture,filepath);

            //Editing
            productpicture.Edit(picturepath, command.PictureAlt, command.PictureTitle, command.ProductId);
            _productPictureRepository.Save();
            return operation.Succedded();


        }

        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetails(id);
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var productpicture = _productPictureRepository.Get(id);
            if (productpicture == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            productpicture.Remove();
            _productPictureRepository.Save();
            return operation.Succedded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var productpicture = _productPictureRepository.Get(id);
            if (productpicture == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            productpicture.Restore();
            _productPictureRepository.Save();
            return operation.Succedded();
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchmodel)
        {
            return _productPictureRepository.Search(searchmodel);
        }
    }
}
