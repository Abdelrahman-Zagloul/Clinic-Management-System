using Clinic_Management_system.Models;

namespace Clinic_Management_system.Services.Interfaces
{
    public interface IReceptionistService : ICRUDService<Receptionist>
    {
        bool IsReceptionistExist(int id);

        string MakeReport();

        (bool isSuccess, string message) GetReceptionistAccountInfo(int userId);

        int GetReceptionistIDByEmail(string email);
    }
}