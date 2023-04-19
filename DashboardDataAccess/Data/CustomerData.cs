using AzDataAccess.DbAccess;
using AzDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace AzDataAccess.Data;

public class CustomerData : ICustomerData
{
    private readonly ISqlDataAccess _db;
    private readonly IConfiguration _config;

    public CustomerData(ISqlDataAccess db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }
    //GetAll
    //public Task<IEnumerable<Customer_AddressModel>> GetCustomers() => _db.LoadData<Customer_AddressModel, dynamic>(storedProcedure: "dbo.spCustomer_GetAll", new { });
    //GetAll CustomerAddressState
    public async Task<IEnumerable<CustomerModel?>> GetCustomerAddress()
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default"));

            return await connection.QueryAsync<CustomerModel, AddressModel, StateModel, CustomerModel>("dbo.spCustomer_GetAll",
                (customer, address, state) => { if (address != null) { customer.Address = address; }; if (address != null && state != null) { address.State = state; }; return customer; },
                splitOn: "address1, state",
                commandType: CommandType.StoredProcedure);
        }
    //GetOne
    public async Task<CustomerModel?> GetCustomer(int id)
    {
        //var results = await _db.LoadData<CustomerModel, dynamic>(storedProcedure: "dbo.spCustomer_Get", new { id = id });
        CustomerModel cust = null;

        using IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default"));

        await connection.QueryAsync<CustomerModel, AddressModel, CustomerModel>("dbo.spCustomer_Get",
                map: (customer, address) => 
                {
                    if (address != null) 
                    { 
                        customer.Address = address; 
                    }
                    cust = customer;
                    return cust; 
                },
                splitOn: "address1",
                commandType: CommandType.StoredProcedure,
                param: new { id = id });
        return cust;
    }
    //Insert New User
    public Task InsertCustomer(CustomerModel customer) => _db.SaveData(storedProcedure: "dbo.spCustomer_Insert", new { customer.first_name, customer.last_name, customer.email });
    //Update User
    public Task UpdateCustomer(CustomerModel customer) => _db.SaveData(storedProcedure: "dbo.spCustomer_Update", new { customer.id, customer.first_name, customer.last_name, customer.email, customer.Address.address1, customer.Address.address2, customer.Address.state_id, customer.Address.city, customer.Address.postal_code, customer.Address.phone });
    //Delete User
    public Task DeleteCustomer(int id) => _db.SaveData(storedProcedure: "dbo.spCustomer_Delete", new { id = id });

    //Insert Customer and Address
    //public Task InsertCustomerAddress(Customer_AddressModel custaddr) => _db.SaveData(storedProcedure: "dbo.spCustomerAddress_Insert", new { custaddr.customer.first_name, custaddr.customer.last_name, custaddr.customer.email, custaddr.address.address1, custaddr.address.address2, custaddr.address.state_id, custaddr.address.city, custaddr.address.postal_code, custaddr.address.phone });
    public Task InsertCustomerAddress(CustomerModel customer) => _db.SaveData(storedProcedure: "dbo.spCustomerAddress_Insert", new { customer.first_name, customer.last_name, customer.email, customer.Address.address1, customer.Address.address2, customer.Address.state_id, customer.Address.city, customer.Address.postal_code, customer.Address.phone });


}