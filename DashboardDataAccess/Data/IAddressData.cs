using AzDataAccess.Models;

namespace AzDataAccess.Data
{
    public interface IAddressData
    {
        Task DeleteAddress(int id);
        Task<AddressModel?> GetAddress(int id);
        Task InsertAddress(AddressModel address);
        Task UpdateAddress(AddressModel address);
    }
}