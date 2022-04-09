using _01_Framework.Application;
using ArticleManagement.Application.Contract.ArticleCategory;
using ArticleManagement.Domain.ArticleCategoryAgg;

namespace ArticleManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticleCategory command)
        {
            var operation = new OperationResult();
            if (_articleCategoryRepository.IsExists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);


            var slug = command.Slug.Slugify();
            var picturepath = _fileUploader.Upload(command.Picture, slug);
            var articlecategory = new ArticleCategory(command.Name, picturepath,command.PictureAlt,command.PictureTitle, command.Description, command.ShowOrder, slug
                , command.KeyWords, command.MetaDescription, command.CanonicalAddress);

            _articleCategoryRepository.Create(articlecategory);
            _articleCategoryRepository.Save();
            return operation.Succedded();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operation = new OperationResult();
            var articlecategory = _articleCategoryRepository.Get(command.Id);

            if (_articleCategoryRepository.IsExists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            if (articlecategory == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            
            var slug = command.Slug.Slugify();
            var picturepath = _fileUploader.Upload(command.Picture, slug);


            articlecategory.Edit(command.Name, picturepath,command.PictureAlt,command.PictureTitle, command.Description, command.ShowOrder, slug
                , command.KeyWords, command.MetaDescription, command.CanonicalAddress);
            _articleCategoryRepository.Save();
            return operation.Succedded();
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _articleCategoryRepository.GetArticleCategories();
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _articleCategoryRepository.GetDetails(id);
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _articleCategoryRepository.Search(searchModel);
        }
    }
}