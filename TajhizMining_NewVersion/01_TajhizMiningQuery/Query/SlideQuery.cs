using _01_TajhizMiningQuery.Contracts.Slide;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_TajhizMiningQuery.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly TajhizMiningContext _context;

        public SlideQuery(TajhizMiningContext context)
        {
            _context = context;
        }

        public List<SlideQueryModel> GetSlides()
        {
            return _context.Slides.Where(s=>!s.IsRemoved)
                .Select(s => new SlideQueryModel
                {
                    BtnText = s.BtnText,
                    Heading=s.Heading,
                    Picture=s.Picture,
                    PictureAlt=s.PictureAlt,
                    PictureTitle=s.PictureTitle,
                    Text=s.Text,
                    Title=s.Title
                }).ToList();
        }
    }
}
