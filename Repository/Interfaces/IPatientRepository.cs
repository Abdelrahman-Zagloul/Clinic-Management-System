using Clinic_Management_system.Models;

namespace Clinic_Management_system.Repository.Interfaces
{
    public interface IPatientRepository : ICRUDRepository<Patient>
    {
        List<Patient> SearchByName(string name);
    }
}