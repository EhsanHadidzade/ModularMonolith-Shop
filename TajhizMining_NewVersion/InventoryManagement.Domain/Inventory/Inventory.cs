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

    public class InventoryOperation
    {
        public long Id { get; set; }
        public bool Operation { get; set; }
        public long Count { get; set; }
        public long OperatorId { get; set; }
        public DateTime OperationDate { get; set; }
        public long CurrentCount { get; set; }
        public string Description { get; set; }
        public long OrderId { get; set; }
        public long InventoryId { get; set; }
        public Inventory Inventory { get; set; }

        protected InventoryOperation()
        {

        }
        public InventoryOperation(bool operation, long count, long operatorId,
            long currentCount, string description, long orderId, long inventoryId)
        {
            Operation = operation;
            Count = count;
            OperatorId = operatorId;
            CurrentCount = currentCount;
            Description = description;
            OrderId = orderId;
            InventoryId = inventoryId;
            OperationDate=DateTime.Now;
        }
    }
}
