using _01_Framework.Infrastructure;
using InventoryManagement.Domain.Inventory;
using InventoryManegement.Application.Contract.Inventory;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        public readonly InventoryContext _inventoryContext;
        private readonly TajhizMiningContext _tajhizMiningContext;

        public InventoryRepository(InventoryContext inventoryContext,TajhizMiningContext tajhizMiningContext) : base(inventoryContext)
        {
            _inventoryContext = inventoryContext;
            _tajhizMiningContext = tajhizMiningContext;
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryContext.Inventories.Select(x => new EditInventory
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.ProductId
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchmodel)
        {
            var products = _tajhizMiningContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _inventoryContext.Inventories.Select(x => new InventoryViewModel
            {
                CurrentCount = x.CalculateInventoryStock(),
                Id = x.Id,
                InStock = x.InStock,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice
            });

            if (searchmodel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchmodel.ProductId);

            if(!searchmodel.InStock)
                query=query.Where(x=>!x.InStock);

            var inventories = query.OrderByDescending(x => x.Id).ToList();

            //to get each product name of every inventories
            inventories.ForEach(inventory => inventory.Product = products.FirstOrDefault(x => x.Id == inventory.ProductId)?.Name);

            return inventories;

        }
    }
}
