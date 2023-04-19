namespace MovieAppAPI.Api;

public static class Address
{
    public static void ConfigureApiAddress(this WebApplication app)
    {
        app.MapGet("/Addresses/{id}", GetAddress);
        app.MapPost("/Addresses", InsertAddress);
        app.MapPut("/Addresses", UpdateAddress);
        app.MapDelete("/Addresses", DeleteAddress);

    }

    private static async Task<IResult> GetAddress(int id, IAddressData data)
    {
        try
        {
            var results = await data.GetAddress(id);
            if (results == null) return Results.NotFound();
            return Results.Ok(results);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    private static async Task<IResult> InsertAddress(AddressModel address, IAddressData data)
    {
        try
        {
            await data.InsertAddress(address);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    private static async Task<IResult> UpdateAddress(AddressModel address, IAddressData data)
    {
        try
        {
            await data.UpdateAddress(address);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    private static async Task<IResult> DeleteAddress(int id, IAddressData data)
    {
        try
        {
            await data.DeleteAddress(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
