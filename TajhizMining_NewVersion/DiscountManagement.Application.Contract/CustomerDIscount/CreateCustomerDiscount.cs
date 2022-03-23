using _01_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Application.Contract.CustomerDIscount
{
    public class CreateCustomerDiscount
    {
        [Range(0, 100000000, ErrorMessage = ValidationMessage.IsRequired)]
        public long ProductId { get; set; }

        [Range(0, 99, ErrorMessage = ValidationMessage.IsRequired)]
        public int DiscountRate { get; set; }

        [Required(ErrorMessage =ValidationMessage.IsRequired)]
        public string StartDate { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)] 
        public string EndDate { get; set; }

        public string Reason { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
