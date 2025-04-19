using Clinic_Management_system.DTO;
using Clinic_Management_system.Helper;
using Clinic_Management_system.Models;
using Clinic_Management_system.Repository.Implementations;
using Clinic_Management_system.Repository.Interfaces;
using Clinic_Management_system.Services.Interfaces;
using System.Text;

namespace Clinic_Management_system.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public (bool isSuccess, string message) Add(User user)
        {
            try
            {
                int userId = _userRepository.Add(user);
                return (true, $"\nUser With Id: \"{userId}\" added successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"\nError: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) Update(int id, User newUser)
        {
            try
            {
                return _userRepository.Update(id, newUser) ? (true, $"\nUser with ID: '{id}' Updated successfully") : (false, "\nUpdate failed.");
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
                bool isRemoved = _userRepository.Delete(id);
                return isRemoved ? (true, "\nRemove successfully ") : (false, "\nUser not found or could not be removed.");
            }
            catch (Exception ex)
            {
                return (false, $"\nError: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) GetById(int id)
        {
            try
            {
                var user = _userRepository.GetById(id);

                if (user == null)
                    return (false, $"\nCan't Find User with ID: '{id}'");

                return (true, user.ToString() ?? "");
            }
            catch (Exception ex)
            {
                return (false, $"\nError retrieving User data: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) GetAll()
        {
            try
            {
                var users = _userRepository.GetAll();

                if (users == null || !users.Any())
                    return (false, "\nNo Users found.");

                // استدعاء دالة التنسيق لعرض النتائج بشكل مرتب
                string formattedResults = FormatHelper.FormatUserDetails(users);

                return (true, formattedResults);
            }
            catch (Exception ex)
            {
                return (false, $"\nError retrieving User data: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) Clear()
        {
            try
            {
                return _userRepository.Clear() ? (true, "\nAll User Remove Successfully") : (false, "\nNo User To Remove");
            }
            catch (Exception ex)
            {
                return (false, $"\nError: {ex.Message}");
            }
        }

        public string MakeReport()
        {
            return _userRepository.MakeReport().ToString();
        }

        public bool IsUserExist(int id)
        {
            return _userRepository.GetById(id) == null ? false : true;
        }

        public bool IsEmailExist(string email)
        {
            return _userRepository.IsEmailExist(email);
        }

        public string GenerateManagerReport()
        {
            return _userRepository.GenerateManagerReport().ToString();
        }

        //public (bool isSuccess, string message) GetAccountInfo(int userId)
        //{
        //    try
        //    {
        //        var user = _userRepository.get(id);

        //        if (user == null)
        //            return (false, $"\nCan't Find User with ID: '{id}'");

        //        return (true, user.ToString() ?? "");
        //    }
        //    catch (Exception ex)
        //    {
        //        return (false, $"\nError retrieving User data: {ex.Message}");
        //    }
        //}
    }
}