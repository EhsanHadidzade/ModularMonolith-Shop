using _01_Framework.Application;
using _01_Framework.Infrastructure;
using DiscountManagement.Application.Contract.CustomerDIscount;
using DiscountManagement.Domain.CustomerDiscount;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CustomerDiscountRepository : RepositoryBase<long, DiscountManagement.Domain.CustomerDiscount.CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly DiscountContext _discountContext;
        private readonly TajhizMiningContext _Shopcontext;
        public CustomerDiscountRepository(DiscountContext discountContext, TajhizMiningContext shopcontext) : base(discountContext)
        {
            _discountContext = discountContext;
            _Shopcontext = shopcontext;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _discountContext.CustomerDiscounts.Select(x => new EditCustomerDiscount
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                EndDate = x.EndDate.ToString(),
                StartDate = x.StartDate.ToString(),
                ProductId = x.ProductId,
                Reason=x.Reason
            }).FirstOrDefault(x=>x.Id==id);
        }

        public List<CustomerDiscountViewModel> search(CustomerDiscountSearchModel searchmodel)
        {
            var products = _Shopcontext.Products;
            var query = _discountContext.CustomerDiscounts.Select(x => new CustomerDiscountViewModel
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                EndDate = x.EndDate.ToFarsi(),
                EndDateGr=x.EndDate,
                StartDate = x.StartDate.ToFarsi(),
                StartDateGr=x.StartDate,
                ProductId = x.ProductId,
                Reason = x.Reason

            });

            if (searchmodel.ProductId > 0)
            {
                query=query.Where(x=>x.ProductId==searchmodel.ProductId);
            }

            if (!string.IsNullOrWhiteSpace(searchmodel.StartDate))
            {
               
                query = query.Where(x => x.StartDateGr > searchmodel.StartDate.ToGeorgianDateTime());
            }
            if (!string.IsNullOrWhiteSpace(searchmodel.EndDate))
            {
                var dateNow = DateTime.Now;
                query = query.Where(x => x.EndDateGr < searchmodel.EndDate.ToGeorgianDateTime());
            }

            var discounts=query.OrderByDescending(x=>x.Id).ToList();

            discounts.ForEach(discount => discount.Product = products.FirstOrDefault(x => x.Id == discount.ProductId).Name);

            return discounts;

        }
    }
}
