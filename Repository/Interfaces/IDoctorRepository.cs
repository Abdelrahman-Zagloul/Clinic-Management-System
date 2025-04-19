using Clinic_Management_system.DTO;
using Clinic_Management_system.Models;

namespace Clinic_Management_system.Repository.Interfaces
{
    public interface IDoctorRepository : ICRUDRepository<Doctor>
    {
        DoctorReportDto MakeReport();

        Doctor? GetByEmail(string email);

        DoctorAccountInformationDto GetDoctorAccountInfo(int userId);

        List<DoctorSimpleDto> GetDoctorsToReceptionist();
    }
}