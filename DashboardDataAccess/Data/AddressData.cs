using AzDataAccess.DbAccess;
using AzDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzDataAccess.Data;

public class AddressData : IAddressData
{
    private readonly ISqlDataAccess _db;

    public AddressData(ISqlDataAccess db)
    {
        _db = db;
    }
    //GetOne
    public async Task<AddressModel?> GetAddress(int id)
    {
        var results = await _db.LoadData<AddressModel, dynamic>(storedProcedure: "dbo.spAddress_Get", new { id = id });
        return results.FirstOrDefault();
    }
    //Insert New Address
    public Task InsertAddress(AddressModel address) => _db.SaveData(storedProcedure: "dbo.spAddress_Insert", new { address.address1, address.address2, address.state_id, address.city, address.postal_code, address.phone });
    //Update User
    public Task UpdateAddress(AddressModel address) => _db.SaveData(storedProcedure: "dbo.spAddress_Update", address);
    //Delete User
    public Task DeleteAddress(int id) => _db.SaveData(storedProcedure: "dbo.spAddress_Delete", new { id = id });
}
