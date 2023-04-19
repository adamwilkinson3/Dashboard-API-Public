using AzDataAccess.Models;

namespace AzDataAccess.Data
{
    public interface IBusinessData
    {
        Task<IEnumerable<StaffModel?>> GetAllStaff();
        Task<StaffModel?> GetStaff(int id);
        Task InsertStaffAddress(StaffModel staff);
        Task UpdateStaff(StaffModel staff);
        Task DeleteStaff(int id);
    }
}