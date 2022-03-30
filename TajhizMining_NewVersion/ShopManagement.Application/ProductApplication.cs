using _01_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.Product;
using SM.Domain.ShopManagement.Domain.ProductCategory;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ProductApplication(IProductRepository productRepository, IProductCategoryRepository productCategoryRepository, IFileUploader fileUploader)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProduct command)
        {
            OperationResult operation = new OperationResult();
            if (_productRepository.IsExists(x => x.Name == command.Name))
               return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var slug=command.Slug.Slugify();

            //About Uploading
            var categorySlug=_productCategoryRepository.GetCategorySlugByCategoryId(command.CategoryId);
            var filepath = $"{categorySlug}//{slug}";
            var PicturePath=_fileUploader.Upload(command.Picture,filepath); 

            //Creating
            var product = new Product(command.Name, command.UnitPrice, command.Code, command.ShortDescription,
                command.Description, PicturePath, command.PictureAlt,command.PictureTitle, slug, 
                command.KeyWords, command.MetaDescription, command.CategoryId);

            _productRepository.Create(product);
            _productRepository.Save();
            return operation.Succedded();

        }

        public OperationResult Edit(EditProduct command)
        {
            OperationResult operation=new OperationResult();

            var product = _productRepository.GetProductWithCategoryByProductId(command.Id);
            if(product == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);


            if (_productRepository.IsExists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var slug = command.Slug.Slugify();

            //About Uploading
            var filepath = $"{product.ProductCategory.Slug}//{slug}";
            var picturePath= _fileUploader.Upload(command.Picture, filepath);

            //Editing
            product.Edit(command.Name, command.UnitPrice, command.Code, command.ShortDescription,
                command.Description, picturePath, command.PictureAlt, command.PictureTitle, slug,
                command.KeyWords, command.MetaDescription, command.CategoryId);
            _productRepository.Save();
            return operation.Succedded();

        }

        public EditProduct GetDetails(long id)
        {
           return _productRepository.GetDetails(id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
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
