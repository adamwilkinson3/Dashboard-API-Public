using AzDataAccess.Models;

namespace MovieAppAPI.Api;

public static class Staff
{
    public static void ConfigureApiBusiness(this WebApplication app)
    {
        app.MapGet("/Staff", GetAllStaff);
        app.MapGet("/Staff/{id}", GetStaff);
        app.MapPut("/Staff", UpdateStaff);
        app.MapDelete("/Staff", DeleteStaff);
        app.MapPost("/StaffAddress", InsertStaffAddress);

    }

    private static async Task<IResult> GetAllStaff(IBusinessData data)
    {
        try
        {
            var results = await data.GetAllStaff();

            return Results.Ok(results);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    private static async Task<IResult> GetStaff(int id, IBusinessData data)
    {
        try
        {
            var results = await data.GetStaff(id);
            if (results == null) return Results.NotFound();
            return Results.Ok(results);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    private static async Task<IResult> UpdateStaff(StaffModel staff, IBusinessData data)
    {
        try
        {
            await data.UpdateStaff(staff);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    private static async Task<IResult> DeleteStaff(int id, IBusinessData data)
    {
        try
        {
            await data.DeleteStaff(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    private static async Task<IResult> InsertStaffAddress(StaffModel staff, IBusinessData businessData)
    {
        try
        {
            await businessData.InsertStaffAddress(staff);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
