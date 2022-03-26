using _01_Framework.Domain;
using InventoryManegement.Application.Contract.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.Inventory
{
    public interface IInventoryRepository:IRepository<long,Inventory>
    {
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchmodel);
        List<InventoryOperationViewModel> GetOperationLog(long inventoryId);
    }
}
