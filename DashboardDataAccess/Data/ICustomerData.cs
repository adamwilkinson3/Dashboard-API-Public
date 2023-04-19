using AzDataAccess.Models;

namespace AzDataAccess.Data
{
    public interface ICustomerData
    {
        Task DeleteCustomer(int id);
        Task<IEnumerable<CustomerModel?>> GetCustomerAddress();
        Task<CustomerModel?> GetCustomer(int id);
        Task InsertCustomer(CustomerModel customer);
        Task UpdateCustomer(CustomerModel customer);
        //Task InsertCustomerAddress(Customer_AddressModel custaddr);
        Task InsertCustomerAddress(CustomerModel customer);
    }
}