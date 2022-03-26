using _01_Framework.Application;
using InventoryManagement.Domain.Inventory;
using InventoryManegement.Application.Contract.Inventory;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public OperationResult Create(CreateInventory command)
        {
            var operation=new OperationResult();
            if (_inventoryRepository.IsExists(x => x.ProductId == command.ProductId))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var inventory = new Inventory(command.ProductId, command.UnitPrice);
            _inventoryRepository.Create(inventory);
            _inventoryRepository.Save();
            return operation.Succedded();
        }

        public OperationResult Edit(EditInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.Id);
            if(inventory==null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            if (_inventoryRepository.IsExists(x => x.ProductId == command.ProductId && x.Id!=command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            inventory.Edit(command.ProductId,command.UnitPrice);
            _inventoryRepository.Save();
            return operation.Succedded();
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryRepository.GetDetails(id);
        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            return _inventoryRepository.GetOperationLog(inventoryId);
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operation=new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            //To DO Get The OperatorId To use There
            const long operatorid = 1;
            inventory.Increase(command.Count, operatorid, command.Description);
            _inventoryRepository.Save();
            return operation.Succedded();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            //To DO Get The OperatorId To use There
            //we put orderId=1 for this method of reduce cuz its operator is admin . there is no need for orderId
            const long operatorid = 1;
            inventory.Reduce(command.Count,operatorid,command.Description,0);
            _inventoryRepository.Save();
            return operation.Succedded();
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            var operation = new OperationResult();
            const long operatorid = 1;
            foreach (var item in command)
            {
                var inventory=_inventoryRepository.Get(item.InventoryId);
                inventory.Reduce(item.Count,operatorid,item.Description,item.OrderId);
            }
            _inventoryRepository.Save();
            return operation.Succedded(); 
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchmodel)
        {
            return _inventoryRepository.Search(searchmodel);
        }
    }
}