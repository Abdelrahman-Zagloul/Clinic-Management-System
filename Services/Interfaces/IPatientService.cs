using Clinic_Management_system.Models;

namespace Clinic_Management_system.Services.Interfaces
{
    public interface IPatientService : ICRUDService<Patient>
    {
        public (bool isSuccess, string message) GetAll(int doctorId);

        public (bool isSuccess, string message) SearchByName(string name);
    }
}