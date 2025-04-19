using Clinic_Management_system.Helper;
using Clinic_Management_system.Models;
using Clinic_Management_system.Repository.Interfaces;
using Clinic_Management_system.Services.Interfaces;
using System.Text;

namespace Clinic_Management_system.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public (bool isSuccess, string message) Add(Patient patient)
        {
            try
            {
                int patientId = _patientRepository.Add(patient);
                return (true, $"\nPatient \"{patient.Name}\" added successfully with ID {patientId}.");
            }
            catch (Exception ex)
            {
                return (false, $"\nError: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) Clear()
        {
            // Not Use yet
            try
            {
                return _patientRepository.Clear() ? (true, "\nAll Patient Remove Successfully") : (false, "\nNo Patient To Remove");
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
                bool isRemoved = _patientRepository.Delete(id);
                return isRemoved ? (true, "\nRemove Successfully ") : (false, "\nPatient not found or could not be removed.");
            }
            catch (Exception ex)
            {
                return (false, $"\nError: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) GetAll()
        {
            try
            {
                var patients = _patientRepository.GetAll();

                if (patients == null || !patients.Any())
                    return (false, "\nNo Patients found.");

                string formattedPatients = FormatHelper.FormatPatientSearchResults(patients);
                return (true, formattedPatients);
            }
            catch (Exception ex)
            {
                return (false, $"\nError retrieving patient data: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) GetAll(int doctorId)
        {
            try
            {
                var patients = _patientRepository.GetAll();

                if (patients == null || !patients.Any())
                    return (false, "\nNo Patients found.");

                string formattedPatients = FormatHelper.FormatPatientSearchResults(patients);
                return (true, formattedPatients);
            }
            catch (Exception ex)
            {
                return (false, $"\nError retrieving patient data: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) GetById(int id)
        {
            try
            {
                var patient = _patientRepository.GetById(id);

                if (patient == null)
                    return (false, $"\nCan't Find Patient with ID: '{id}'");

                return (true, patient.ToString());
            }
            catch (Exception ex)
            {
                return (false, $"\nError retrieving Patient data: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) SearchByName(string name)
        {
            try
            {
                var patients = _patientRepository.SearchByName(name);

                if (patients.Count == 0)
                    return (false, $"\nNo patients found with name: {name}");

                string formattedResult = FormatHelper.FormatPatientSearchResults(patients);
                return (true, formattedResult);
            }
            catch (Exception ex)
            {
                return (false, $"\nERROR: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) Update(int id, Patient newPatient)
        {
            try
            {
                return _patientRepository.Update(id, newPatient) ? (true, $"\nPatient with ID: '{id}' Updated Successfully") : (false, "\nUpdate failed.");
            }
            catch (Exception ex)
            {
                return (false, $"\nError: {ex.Message}");
            }
        }
    }
}