using _01_Framework.Domain;
using ShopManagement.Application.Contracts.Slide;

namespace ShopManagement.Domain.Slide
{
    public interface ISlideRepository:IRepository<long,Slide>
    {
        List<SlideViewModel> Getlist();
        EditSlide GetDetails(long id);  
    }
}
