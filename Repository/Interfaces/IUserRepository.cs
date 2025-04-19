using Clinic_Management_system.DTO;
using Clinic_Management_system.Enums;
using Clinic_Management_system.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_system.Repository.Interfaces
{
    public interface IUserRepository : ICRUDRepository<User>, IManagerReportRepository
    {
        bool IsEmailExist(string email);

        UserReportDto MakeReport();
    }
}