using _01_Framework.Application;
using ArticleManagement.Application.Contract.Article;
using ArticleManagement.Domain.ArticleAgg;
using ArticleManagement.Domain.ArticleCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ArticleApplication(IArticleRepository articleRepository, IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader)
        {
            _articleRepository = articleRepository;
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticle command)
        {
            var operation = new OperationResult();

            if (_articleRepository.IsExists(x => x.Title == command.Title))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var slug=command.Slug.Slugify();

            //Uploading Article Pic
            var categoryslug=_articleCategoryRepository.GetCategorySlugByCategoryId(command.CategoryId);
            var path = $"{categoryslug}//{slug}";
            var picturepath = _fileUploader.Upload(command.Picture, path);

            var publishdate = command.PublishDate.ToGeorgianDateTime();

            var article=new Article(command.Title,command.ShortDescription,command.Description,picturepath
                ,command.PictureAlt,command.PictureTitle, publishdate, slug
                ,command.Keywords,command.MetaDescription,command.CanonicalAddress,command.CategoryId);

            //Creating
            _articleRepository.Create(article);
            _articleRepository.Save();
            return operation.Succedded();
        }

        public OperationResult Edit(EditArticle command)
        {
            var operation = new OperationResult();

            if (_articleRepository.IsExists(x => x.Title == command.Title && x.Id!=command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var article=_articleRepository.GetArticleWithCategory(command.Id);
            if(article==null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            var categoryslug = article.Category.Slug;
            var slug = command.Slug.Slugify();

            //Uploading
            var path = $"{categoryslug}//{slug}";
            var picturepath = _fileUploader.Upload(command.Picture, path);

            var publishdate = command.PublishDate.ToGeorgianDateTime();

            //Editing
            article.Edit(command.Title, command.ShortDescription,command.Description, picturepath
                , command.PictureAlt, command.PictureTitle, publishdate, slug
                , command.Keywords, command.MetaDescription, command.CanonicalAddress, command.CategoryId);
            _articleRepository.Save();
            return operation.Succedded();

        }

        public EditArticle GetDetails(long id)
        {
           return _articleRepository.GetDetails(id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _articleRepository.Search(searchModel);
        }
    }
}
