﻿namespace InventoryManagement.Domain.Inventory
{
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
