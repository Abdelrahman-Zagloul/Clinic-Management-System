using Clinic_Management_system.DTO;

namespace Clinic_Management_system.Repository.Interfaces
{
    public interface IManagerReportRepository
    {
        public ManagerReportDto GenerateManagerReport();
    }
}