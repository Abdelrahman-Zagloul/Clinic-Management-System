using Clinic_Management_system.DTO;
using Clinic_Management_system.Models;

namespace Clinic_Management_system.Repository.Interfaces
{
    public interface IReceptionistRepository : ICRUDRepository<Receptionist>
    {
        ReceptionistReportDto MakeReport();

        Receptionist? GetByEmail(string email);

        ReceptionistAccountInformationDto GetReceptionistAccountInfo(int receptionistId);
    }
}