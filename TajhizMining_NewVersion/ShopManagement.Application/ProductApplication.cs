using _01_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;

        public ProductApplication(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            OperationResult operation = new OperationResult();
            if (_productRepository.IsExists(x => x.Name == command.Name))
               return operation.Failed(ApplicationMessage.DuplicatedRecord);


            var slug=command.Slug.Slugify();
            var product = new Product(command.Name, command.UnitPrice, command.Code, command.ShortDescription,
                command.Description, command.Picture, command.PictureAlt,command.PictureTitle, slug, 
                command.KeyWords, command.MetaDescription, command.CateforyId);

            _productRepository.Create(product);
            _productRepository.Save();
            return operation.Succedded();

        }

        public OperationResult Edit(EditProduct command)
        {
            OperationResult operation=new OperationResult();

            var product = _productRepository.Get(command.Id);
            if(product == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);


            if (_productRepository.IsExists(x => x.Name == command.Name && x.Id!=command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            product.Edit(command.Name, command.UnitPrice, command.Code, command.ShortDescription,
                command.Description, command.Picture, command.PictureAlt, command.PictureTitle, slug,
                command.KeyWords, command.MetaDescription, command.CateforyId);
            _productRepository.Save();
            return operation.Succedded();

            
        }

        public EditProduct GetDetails(long id)
        {
           return _productRepository.GetDetails(id);
        }

        public OperationResult InStock(long id)
        {
            OperationResult operation = new OperationResult();
            var product = _productRepository.Get(id);
            if (product == null)
            {
                return operation.Failed(ApplicationMessage.RecordNotFound);
            }
            product.InStock();
            _productRepository.Save();

            return operation.Succedded();
        }

        public OperationResult NotInStock(long id)
        {
            OperationResult operation = new OperationResult();
            var product = _productRepository.Get(id);
            if (product == null)
            {
                return operation.Failed(ApplicationMessage.RecordNotFound);
            }
            product.NotInStock();
            _productRepository.Save();

            return operation.Succedded();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchmodel)
        {
            return _productRepository.Search(searchmodel);
        }
    }
}
