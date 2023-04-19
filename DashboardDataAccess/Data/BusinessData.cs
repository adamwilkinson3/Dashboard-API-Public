using AzDataAccess.DbAccess;
using AzDataAccess.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.Common;

namespace AzDataAccess.Data;

public class BusinessData : IBusinessData
{
    private readonly ISqlDataAccess _db;
    private readonly IConfiguration _config;

    public BusinessData(ISqlDataAccess db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    public async Task<IEnumerable<StaffModel?>> GetAllStaff()
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default"));

        return await connection.QueryAsync<StaffModel, AddressModel, StateModel, StaffModel>("dbo.spStaff_GetAll",
            (staff, address, state) => { if (address != null) { staff.Address = address; }; if (address != null && state != null) { address.State = state; }; return staff; },
            splitOn: "address1, state",
            commandType: CommandType.StoredProcedure);
    }
    //GetOne
    public async Task<StaffModel?> GetStaff(int id)
    {
        StaffModel staffData = null;

        using IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default"));

        await connection.QueryAsync<StaffModel, AddressModel, StaffModel>("dbo.spStaff_Get",
                map: (staff, address) =>
                {
                    if (address != null)
                    {
                        staff.Address = address;
                    }
                    staffData = staff;
                    return staffData;
                },
                splitOn: "address1",
                commandType: CommandType.StoredProcedure,
                param: new { id = id });
        return staffData;
    }
    //Update Staff
    public Task UpdateStaff(StaffModel staff) => _db.SaveData(storedProcedure: "dbo.spStaff_Update", new { staff.id, staff.first_name, staff.last_name, staff.email, staff.username, staff.Address.address1, staff.Address.address2, staff.Address.state_id, staff.Address.city, staff.Address.postal_code, staff.Address.phone });
    //Delete Staff
    public Task DeleteStaff(int id) => _db.SaveData(storedProcedure: "dbo.spStaff_Delete", new { id = id });

    //Insert Staff and Address
    public Task InsertStaffAddress(StaffModel staff) => _db.SaveData(storedProcedure: "dbo.spStaffAddress_Insert", new { staff.first_name, staff.last_name, staff.email, staff.username, staff.password, staff.Address.address1, staff.Address.address2, staff.Address.state_id, staff.Address.city, staff.Address.postal_code, staff.Address.phone });

}
