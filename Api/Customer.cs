using AzDataAccess.Models;

namespace MovieAppAPI.Api;

public static class Customer
{
    public static void ConfigureApiCustomer(this WebApplication app)
    {
        app.MapGet("/Customers", GetCustomers);
        app.MapGet("/Customers/{id}", GetCustomer);
        app.MapPost("/Customers", InsertCustomer);
        app.MapPut("/Customers", UpdateCustomer);
        app.MapDelete("/Customers", DeleteCustomer);
        app.MapPost("/CustomersAddress", InsertCustomerAddress);

    }

    private static async Task<IResult> GetCustomers(ICustomerData data)
    {
        try
        {
            var results = await data.GetCustomerAddress();

            return Results.Ok(results);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    private static async Task<IResult> GetCustomer(int id, ICustomerData data)
    {
        try
        {
            var results = await data.GetCustomer(id);
            if (results == null) return Results.NotFound();
            return Results.Ok(results);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    private static async Task<IResult> InsertCustomer(CustomerModel customer, ICustomerData data)
    {
        try
        {
            await data.InsertCustomer(customer);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    private static async Task<IResult> UpdateCustomer(CustomerModel customer, ICustomerData data)
    {
        try
        {
            await data.UpdateCustomer(customer);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    private static async Task<IResult> DeleteCustomer(int id, ICustomerData data)
    {
        try
        {
            await data.DeleteCustomer(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    private static async Task<IResult> InsertCustomerAddress(CustomerModel customer, ICustomerData data, IAddressData data2)
    {
        //CustomerModel customer = custaddr.customer;
        //AddressModel address = custaddr.address;
        try
        {
            //await data.InsertCustomer(customer);
            //await data2.InsertAddress(address);
            await data.InsertCustomerAddress(customer);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
