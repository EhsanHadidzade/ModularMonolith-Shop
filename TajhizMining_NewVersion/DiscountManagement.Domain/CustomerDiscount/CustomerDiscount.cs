using _01_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Domain.CustomerDiscount
{
    public class CustomerDiscount : EntityBase
    {
        public long ProductId { get; set; }
        public int DiscountRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }

        public CustomerDiscount(long productId, int discountRate, DateTime startDate, DateTime endDate, string reason)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
           
        }

        public void Edit(long productId, int discountRate, DateTime startDate, DateTime endDate, string reason)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
        }
    }



}
