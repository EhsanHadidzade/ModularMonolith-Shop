using _01_Framework.Application;
using SM.Application.ShopManagement.Application.Contracts.ProductCategory;
using SM.Domain.ShopManagement.Domain.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Application.ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.IsExists(x => x.Name == command.Name))
            {
                operation.Failed("امکان ثبت رکورد تکراری وجود ندارد");
            }
            var slug = command.Slug.Slugify();
            var productcategory = new ProductCategory(command.Name, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, command.Keyword, command.MetaDescription,
               slug);

            _productCategoryRepository.Create(productcategory);
            _productCategoryRepository.Save();

            return operation.Succedded();

        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productcategory=_productCategoryRepository.Get(command.Id);

            //in order to check if this record exists
            if (productcategory == null)
            {
                return operation.Failed("اطلاعات یافت نشد");
            }

            //in order to avoid duplication
            if (_productCategoryRepository.IsExists(x => x.Name == command.Name && x.Id != command.Id))
            {
                return operation.Failed("امکان ثبت رکورد تکراری وجود ندارد");
            }

            var slug=command.Slug.Slugify();
            productcategory.Edit(command.Name, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, command.Keyword, command.MetaDescription,
               slug);
            _productCategoryRepository.Save();

            return operation.Succedded();

        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchmodel)
        {
            return _productCategoryRepository.Search(searchmodel);
        }
    }
}
