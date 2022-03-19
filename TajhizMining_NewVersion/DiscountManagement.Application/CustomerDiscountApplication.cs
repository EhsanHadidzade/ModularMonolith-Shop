using _01_Framework.Application;
using DiscountManagement.Application.Contract.CustomerDIscount;
using DiscountManagement.Domain.CustomerDiscount;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Create(CreateCustomerDiscount command)
        {
            var operation=new OperationResult();
            if (_customerDiscountRepository.IsExists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);


            var startdate=command.StartDate.ToGeorgianDateTime();
            var enddate=command.EndDate.ToGeorgianDateTime();

            var customerdiscount = new CustomerDiscount(command.ProductId, command.DiscountRate, startdate, enddate, command.Reason);
            _customerDiscountRepository.Create(customerdiscount);
            _customerDiscountRepository.Save();

            return operation.Succedded();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operation = new OperationResult();
            var customerdiscount=_customerDiscountRepository.Get(command.Id);

            if(customerdiscount==null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            var startdate = command.StartDate.ToGeorgianDateTime();
            var enddate = command.EndDate.ToGeorgianDateTime();
            customerdiscount.Edit(command.ProductId, command.DiscountRate, startdate, enddate, command.Reason);
            _customerDiscountRepository.Save();

            return operation.Succedded();

        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _customerDiscountRepository.GetDetails(id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            return _customerDiscountRepository.search(searchModel);
        }
    }
}