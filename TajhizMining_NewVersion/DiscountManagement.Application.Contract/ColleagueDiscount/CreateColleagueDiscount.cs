using _01_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Application.Contract.ColleagueDiscount
{
    public class CreateColleagueDiscount
    {
        [Range(0, 100000000, ErrorMessage = ValidationMessage.IsRequired)]
        public long ProductId { get; set; }


        [Range(0, 99, ErrorMessage = ValidationMessage.IsRequired)]
        public int DiscountRate { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }


}
