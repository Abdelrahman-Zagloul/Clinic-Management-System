using Clinic_Management_system.Helper;
using Clinic_Management_system.Models;
using Clinic_Management_system.Repository.Implementations;
using Clinic_Management_system.Repository.Interfaces;
using Clinic_Management_system.Services.Interfaces;
using System.Text;

namespace Clinic_Management_system.Services.Implementations
{
    public class ReceptionistService : IReceptionistService
    {
        private readonly IReceptionistRepository _receptionistRepository;

        public ReceptionistService(IReceptionistRepository receptionistRepository)
        {
            _receptionistRepository = receptionistRepository;
        }

        public (bool isSuccess, string message) Add(Receptionist receptionist)
        {
            try
            {
                int receptionistId = _receptionistRepository.Add(receptionist);
                return (true, $"\nReceptionist \"{receptionist.Name}\" added successfully with ID {receptionistId}.");
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
                return _receptionistRepository.Clear() ? (true, "\nAll Receptionist Remove Successfully") : (false, "\nNo Receptionist To Remove");
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
                bool isRemoved = _receptionistRepository.Delete(id);
                return isRemoved ? (true, "\nRemove Successfully ") : (false, "\nReceptionist not found or could not be removed.");
            }
            catch (Exception ex)
            {
                return (false, $"\nError: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) Update(int id, Receptionist newReceptionist)
        {
            try
            {
                return _receptionistRepository.Update(id, newReceptionist) ? (true, $"\nReceptionist with ID: '{id}' Updated successfully") : (false, "\nUpdate failed.");
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
                var receptionists = _receptionistRepository.GetAll();

                if (receptionists == null || !receptionists.Any())
                    return (false, "\nNo Receptionists found.");

                string formattedResults = FormatHelper.FormatReceptionistDetails(receptionists);

                return (true, formattedResults);
            }
            catch (Exception ex)
            {
                return (false, $"\nError retrieving Receptionist data: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) GetById(int id)
        {
            try
            {
                var receptionist = _receptionistRepository.GetById(id);

                if (receptionist == null)
                    return (false, $"\nCan't Find Receptionist with ID: '{id}'");

                return (true, receptionist.ToString());
            }
            catch (Exception ex)
            {
                return (false, $"\nError retrieving Receptionist data: {ex.Message}");
            }
        }

        public bool IsReceptionistExist(int id)
        {
            return _receptionistRepository.GetById(id) == null ? true : false;
        }

        public string MakeReport()
        {
            return _receptionistRepository.MakeReport().ToString();
        }

        public (bool isSuccess, string message) GetReceptionistAccountInfo(int userId)
        {
            try
            {
                return (true, _receptionistRepository.GetReceptionistAccountInfo(userId).ToString());
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public int GetReceptionistIDByEmail(string email)
        {
            Receptionist? receptionist = _receptionistRepository.GetByEmail(email);

            return receptionist?.ReceptionistId ?? 0;
        }
    }
}