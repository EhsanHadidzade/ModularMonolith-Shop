using _01_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPicture;
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

        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            _productPictureRepository = productPictureRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();
            if (_productPictureRepository.IsExists(x => x.Picture == command.Picture && x.ProductId==command.ProductId))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var productpicture=new ProductPicture(command.Picture,command.PictureAlt,command.PictureTitle,command.ProductId);
            _productPictureRepository.Create(productpicture);
            _productPictureRepository.Save();

            return operation.Succedded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productpicture=_productPictureRepository.Get(command.Id);
            if(productpicture==null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (_productPictureRepository.IsExists(x => x.Picture == command.Picture && x.ProductId == command.Id && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);
            productpicture.Edit(command.Picture, command.PictureAlt, command.PictureTitle, command.ProductId);
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
