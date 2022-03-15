using _01_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.Slide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
    {
        private readonly TajhizMiningContext _context;
        public SlideRepository(TajhizMiningContext context) : base(context)
        {
            _context = context;
        }

        public EditSlide GetDetails(long id)
        {
            return _context.Slides.Select(s => new EditSlide
            {
                Id=s.Id,
                BtnText=s.BtnText,
                Heading=s.Heading,
                Picture=s.Picture,
                PictureAlt=s.PictureAlt,
                PictureTitle=s.PictureTitle,
                Text=s.Text,
                Title=s.Title
            }).FirstOrDefault(s=>s.Id==id);
        }

        public List<SlideViewModel> Getlist()
        {
            var query = _context.Slides.Select(s => new SlideViewModel
            {
                CreationDate = s.CretionDate.ToString(),
                Heading = s.Heading,
                Id = s.Id,
                IsRemoved = s.IsRemoved,
                Picture = s.Picture,
                Title = s.Title
            });

            return query.OrderByDescending(s=>s.Id).ToList();
        }
    }
}
