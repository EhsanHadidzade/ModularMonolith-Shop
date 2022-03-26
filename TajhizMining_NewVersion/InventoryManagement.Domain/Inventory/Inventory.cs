using _01_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.Inventory
{
    public class Inventory:EntityBase
    {
        public long ProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public bool InStock { get; private set; }
        public List<InventoryOperation> Operations { get; private set; }

        public Inventory(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            InStock = false;
        }

        public long CalculateInventoryStock()
        {
            var plus = Operations.Where(o => o.Operation).Sum(o => o.Count);
            var minus=Operations.Where(o=>!o.Operation).Sum(o => o.Count);
            return plus - minus;
        }
        public void Increase(long count,long operatorId,string description)
        {
            var currentcout = CalculateInventoryStock() + count;
            var operation = new InventoryOperation(true, count, operatorId, currentcout, description, 0, Id);
            Operations.Add(operation);
            InStock = currentcout > 0;
        }

        public void Reduce(long count, long operatorId, string description, long orderId)
        {
            var currentcount=CalculateInventoryStock() - count;
            var operation = new InventoryOperation(false, count, operatorId, currentcount, description, orderId, Id);
            Operations.Add(operation);
            InStock=currentcount > 0;
        }

        public void Edit(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
        }


    }
}
