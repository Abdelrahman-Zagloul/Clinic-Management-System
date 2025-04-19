using Clinic_Management_system.DTO;
using Clinic_Management_system.Models;

namespace Clinic_Management_system.Services.Interfaces
{
    public interface IDoctorService : ICRUDService<Doctor>
    {
        string MakeReport();

        bool IsDoctorExist(int id);

        int GetDoctorIDByEmail(string email);

        (bool isSuccess, string message) GetDoctorAccountInfo(int userId);

        (bool isSuccess, string message) GetDoctorsToReceptionist();
    }
}