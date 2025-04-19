using Clinic_Management_system.Data;
using Clinic_Management_system.Models;
using Clinic_Management_system.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_system.Repository.Implementations
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        public int Add(Patient patient)
        {
            try
            {
                _context.Patients.Add(patient);

                _context.SaveChanges();
                return patient.PatientId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding patient", ex);
            }
        }

        public bool Update(int id, Patient newPatient)
        {
            //Not Use yet
            try
            {
                var patient = GetById(id);
                if (patient == null)
                    return false;

                patient.Name = newPatient.Name;
                patient.Gender = newPatient.Gender;
                patient.Phone = newPatient.Phone;
                patient.Age = newPatient.Age;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Delete(int patientId)
        {
            try
            {
                var patient = _context.Patients.FirstOrDefault(x => x.PatientId == patientId);

                if (patient == null)
                    return false;

                _context.Patients.Remove(patient);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Patient? GetById(int patientId)
        {
            try
            {
                return _context.Patients.Include(x => x.Appointments).Include(x => x.Receptionist).FirstOrDefault(x => x.PatientId == patientId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Patient> GetAll()
        {
            try
            {
                return _context.Patients.Include(x => x.Appointments).Include(x => x.Receptionist).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Clear()
        {
            try
            {
                return _context.Patients.ExecuteDelete() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Patient> SearchByName(string name)
        {
            try
            {
                var patients = _context.Patients
                    .Include(x => x.Appointments)
                    .Include(x => x.Receptionist)
                    .Where(x => x.Name.ToLower().Contains(name.ToLower() ?? ""))
                    .ToList();

                if (!patients.Any())
                    throw new Exception($"There is no patient with name: '{name}'");

                return patients;
            }
            catch (Exception ex)
            {
                throw new Exception($"Search failed: {ex.Message}");
            }
        }
    }
}