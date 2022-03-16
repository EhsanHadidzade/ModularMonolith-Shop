using _01_TajhizMiningQuery.Contracts.Slide;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class SlideViewComponent:ViewComponent
    {
        private ISlideQuery _slideQuery;

        public SlideViewComponent(ISlideQuery slideQuery)
        {
            _slideQuery = slideQuery;
        }

        public IViewComponentResult Invoke()
        {
            return View(_slideQuery.GetSlides());
        }
    }
}
