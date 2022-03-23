using _01_Framework.Application;

namespace DiscountManagement.Application.Contract.CustomerDIscount
{
    public interface ICustomerDiscountApplication
    {
        OperationResult Create(CreateCustomerDiscount command);
        OperationResult Edit(EditCustomerDiscount command);
        EditCustomerDiscount GetDetails(long id);
        List<CustomerDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);

    }
}
