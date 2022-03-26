﻿using ShopManagement.Application.Contracts.Product;

namespace InventoryManegement.Application.Contract.Inventory
{
    public class CreateInventory
    {
        public long ProductId { get; set; }
        public double UnitPrice { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
