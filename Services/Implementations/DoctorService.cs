using Clinic_Management_system.DTO;
using Clinic_Management_system.Helper;
using Clinic_Management_system.Models;
using Clinic_Management_system.Repository.Interfaces;
using Clinic_Management_system.Services.Interfaces;
using System.Text;

namespace Clinic_Management_system.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public (bool isSuccess, string message) Add(Doctor doctor)
        {
            try
            {
                int doctorId = _doctorRepository.Add(doctor);
                return (true, $"\nDoctor \"{doctor.DoctorName}\" added successfully with ID {doctorId}.");
            }
            catch (Exception ex)
            {
                return (false, $"\nError: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) Clear()
        {
            try
            {
                return _doctorRepository.Clear()
                    ? (true, "\nAll Doctor Remove Successfully")
                    : (false, "\nNo Doctor To Remove");
            }
            catch (Exception ex)
            {
                return (false, $"\nError: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) Delete(int id)
        {
            try
            {
                bool isRemoved = _doctorRepository.Delete(id);
                return isRemoved
                    ? (true, "\nRemove Successfully ")
                    : (false, "\nDoctor not found or could not be removed.");
            }
            catch (Exception ex)
            {
                return (false, $"\nError: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) Update(int id, Doctor newDoctor)
        {
            try
            {
                return _doctorRepository.Update(id, newDoctor)
                    ? (true, $"\nDoctor with ID: '{id}' Updated successfully")
                    : (false, "\nUpdate failed.");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) GetAll()
        {
            try
            {
                var doctors = _doctorRepository.GetAll();

                if (doctors == null || !doctors.Any())
                    return (false, "\nNo doctors found.");

                string formattedResult = FormatHelper.FormatDoctorsTable(doctors);
                return (true, formattedResult);
            }
            catch (Exception ex)
            {
                return (false, $"\nError retrieving doctor data: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) GetById(int id)
        {
            try
            {
                var doctor = _doctorRepository.GetById(id);

                if (doctor == null)
                    return (false, $"\nCan't Find doctor with ID: '{id}'");

                return (true, doctor.ToString());
            }
            catch (Exception ex)
            {
                return (false, $"\nError retrieving doctor data: {ex.Message}");
            }
        }

        public bool IsDoctorExist(int id)
        {
            return _doctorRepository.GetById(id) == null ? false : true;
        }

        public string MakeReport()
        {
            return _doctorRepository.MakeReport().ToString();
        }

        public int GetDoctorIDByEmail(string email)
        {
            Doctor? doctor = _doctorRepository.GetByEmail(email);
            return doctor?.DoctorId ?? 0;
        }

        public (bool isSuccess, string message) GetDoctorAccountInfo(int userId)
        {
            try
            {
                return (true, _doctorRepository.GetDoctorAccountInfo(userId).ToString());
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool isSuccess, string message) GetDoctorsToReceptionist()
        {
            try
            {
                var doctors = _doctorRepository.GetDoctorsToReceptionist();
                if (doctors == null || !doctors.Any())
                    throw new Exception("\nNo Doctors To Show.");

                return (true, PrintDoctors(doctors));
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        private string PrintDoctors(List<DoctorSimpleDto> doctors)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{"ID",-3} | {"Doctor Name",-25} | {"Specialty"} |");
            sb.AppendLine(new string('-', 55));

            foreach (var doctor in doctors)
            {
                sb.AppendLine($"{doctor.DoctorId,-3} | {doctor.DoctorName,-25} | {doctor.Specialty} |");
            }
            return sb.ToString();
        }
    }
}