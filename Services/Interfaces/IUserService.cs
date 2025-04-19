using Clinic_Management_system.Models;

namespace Clinic_Management_system.Services.Interfaces
{
    public interface IUserService : ICRUDService<User>, IManagerReportService
    {
        string MakeReport();

        bool IsUserExist(int id);

        bool IsEmailExist(string email);
    }
}