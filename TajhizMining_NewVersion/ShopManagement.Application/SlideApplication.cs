using _01_Framework.Application;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.Slide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository _slideRepository;
        private readonly IFileUploader _fileUploader;

        public SlideApplication(ISlideRepository slideRepository, IFileUploader fileUploader)
        {
            _slideRepository = slideRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateSlide command)
        {
            var operation = new OperationResult();
            //if(_slideRepository.IsExists(x=>x.Picture==command.Picture))
            //    return operation.Failed(ApplicationMessage.DuplicatedRecord);

            //AboutUploading
            var picturePath = _fileUploader.Upload(command.Picture, "Slides");

            //Creating
            var slide = new Slide(picturePath, command.PictureAlt, command.PictureTitle,
                command.Heading, command.Title, command.Text, command.BtnText);
            _slideRepository.Create(slide);
            _slideRepository.Save();

            return operation.Succedded();
        }

        public OperationResult Edit(EditSlide command)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.Get(command.Id);
            if(slide==null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            //AboutUploading
            var picturePath = _fileUploader.Upload(command.Picture, "Slides");

            //Editing
            slide.Edit(picturePath, command.PictureAlt, command.PictureTitle,
                command.Heading, command.Title, command.Text, command.BtnText);
            _slideRepository.Save();
            return operation.Succedded();
        }

        public EditSlide GetDetail(long id)
        {
            return _slideRepository.GetDetails(id);
        }

        public List<SlideViewModel> Getlist()
        {
            return _slideRepository.Getlist();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.Get(id);
            if (slide == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            slide.Remove();
            _slideRepository.Save();
            return operation.Succedded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.Get(id);
            if (slide == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            slide.Restore();
            _slideRepository.Save();
            return operation.Succedded();
        }
    }
}
