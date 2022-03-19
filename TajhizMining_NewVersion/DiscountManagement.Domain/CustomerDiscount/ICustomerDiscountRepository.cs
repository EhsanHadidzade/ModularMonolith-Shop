using _01_Framework.Domain;
using DiscountManagement.Application.Contract.CustomerDIscount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Domain.CustomerDiscount
{
    public interface ICustomerDiscountRepository:IRepository<long, CustomerDiscount>
    {
        List<CustomerDiscountViewModel> search(CustomerDiscountSearchModel searchmodel);
        EditCustomerDiscount GetDetails(long id);
    }
}
