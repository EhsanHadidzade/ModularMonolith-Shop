using _01_Framework.Application;
using _01_Framework.Infrastructure;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscount;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class ColleagueDiscountRepository : RepositoryBase<long, ColleagueDiscount>, IColleagueDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly TajhizMiningContext _shopcontext;


        public ColleagueDiscountRepository(DiscountContext context, TajhizMiningContext shopcontext) :base(context)
        {
            _context = context;
            _shopcontext = shopcontext;
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _context.ColleagueDiscounts.Select(d => new EditColleagueDiscount
            {
                Id =d.Id,
                DiscountRate = d.DiscountRate,
                ProductId=d.ProductId,
                
            }).FirstOrDefault(d=>d.Id==id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchmodel)
        {
            var products = _shopcontext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _context.ColleagueDiscounts.Select(x => new ColleagueDiscountViewModel
            {
                Id = x.Id,
                CreationDate = x.CretionDate.ToFarsi(),
                ProductId = x.ProductId,
                DiscountRate = x.DiscountRate,
                IsRemoved = x.IsRemoved,
                
            });
            if(searchmodel.ProductId > 0)
                query=query.Where(x=>x.ProductId==searchmodel.ProductId);

            
            var discounts= query.OrderByDescending(x=>x.Id).ToList();
            discounts.ForEach(discount => discount.Product = products.FirstOrDefault(x => x.Id == discount.ProductId).Name);
            return discounts;
            
        }
    }
}
