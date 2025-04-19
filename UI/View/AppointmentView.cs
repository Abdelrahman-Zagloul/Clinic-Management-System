using Clinic_Management_system.Data;
using Clinic_Management_system.Enums;
using Clinic_Management_system.Helper;
using Clinic_Management_system.Models;
using Clinic_Management_system.Repository.Implementations;
using Clinic_Management_system.Repository.Interfaces;
using Clinic_Management_system.Services.Implementations;
using Clinic_Management_system.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Clinic_Management_system.UI.View
{
    public class AppointmentView
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientService _patientService;
        private readonly IReceptionistService _receptionistService;
        private readonly IDoctorService _doctorService;
        private int receptionistsId = 0;
        private int doctorId = 0;

        public AppointmentView
            (
            IAppointmentService appointmentService,
            IPatientRepository patientRepository,
            IReceptionistService receptionistService,
            IDoctorService doctorService,
            IAppointmentRepository appointmentRepository,
            IPatientService patientService
            )
        {
            _appointmentService = appointmentService;
            _patientRepository = patientRepository;
            _patientService = patientService;
            _receptionistService = receptionistService;
            _doctorService = doctorService;
            _appointmentRepository = appointmentRepository;
        }

        public void AddAppointment()
        {
            (Appointment? appointment, int patientId) = GetAppointmentsFromUser();

            if (appointment == null)
            {
                _patientRepository.Delete(patientId);
                return;
            }

            (bool isSuccess, string message) = _appointmentService.AddAngGetAppointmentSimpleDto(appointment, GetRecipientId());
            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
        }

        public void SearchByName()
        {
            string name = InputHelper.GetName("Patient");
            (bool isSuccess, string message) = _patientService.SearchByName(name);
            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
        }

        public void DeleteAppointment()
        {
            int id = InputHelper.GetId("Appointment");

            (bool isSuccess, string message) = _appointmentService.Delete(id);
            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
        }

        public void GetAllAppointments()
        {
            (bool isSuccess, string message) = _appointmentService.GetAll();
            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
        }

        public void GetAppointmentsByDoctor()
        {
            if (GetDoctorId() == 0)
            {
                ConsoleHelper.PrintMessage("\nDoctor not found.", false);
                ConsoleHelper.PressEnterToContinue();
                return;
            }

            (bool isSuccess, string message) = _appointmentService.GetAppointmentsByDoctorId(doctorId);
            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
        }

        public void GetTodayAppointmentsByDoctor()
        {
            if (GetDoctorId() == 0)
            {
                ConsoleHelper.PrintMessage("\nDoctor not found.", false);
                ConsoleHelper.PressEnterToContinue();
                return;
            }

            (bool isSuccess, string message) = _appointmentService.GetTodayAppointmentsByDoctorId(doctorId);
            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
        }

        public void GetAppointmentsByPatient()
        {
            int patientId = InputHelper.GetId("Patient");

            (bool isSuccess, string message) = _appointmentService.GetAppointmentsByPatientId(patientId);
            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
        }

        public void ClearAllAppointments()
        {
            (bool isSuccess, string message) = _appointmentService.Clear();
            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
        }

        private (Appointment? appointment, int patientId) GetAppointmentsFromUser()
        {
            Patient patient = new Patient()
            {
                Name = InputHelper.GetName("Patient"),
                Age = InputHelper.GetAge("Patient", 0, 150),
                Gender = InputHelper.GetGender(),
                Phone = InputHelper.GetPhoneNumber("Patient"),
                ReceptionistId = GetRecipientId(),
            };

            int patientId = _patientRepository.Add(patient);

            Console.WriteLine("\n\t ----- All Doctor Available ----- \n");
            (bool isSuccess, string message) = _doctorService.GetDoctorsToReceptionist();
            ConsoleHelper.PrintMessage(message, isSuccess);
            int doctorId = InputHelper.GetId("Doctor");

            var consultationType = InputHelper.GetConsultationType();
            DateTime date = InputHelper.GetAppointmentDate();
            TimeSpan startTime = GetStratTime(doctorId, date);
            if (startTime == TimeSpan.Zero)
                return (null, patientId);
            Appointment appointment = new Appointment()
            {
                PatientId = patientId,
                DoctorId = doctorId,
                ConsultationType = consultationType,
                Date = date,
                StartTime = startTime,
                EndTime = GetEndTime(startTime, consultationType),
                Price = GetAppointmentPrice(consultationType),
                Notes = InputHelper.GetNotes()
            };

            return (appointment, patientId);
        }

        private int GetRecipientId()
        {
            if (receptionistsId != 0)
                return receptionistsId;

            string recipientEmail = SessionService.CurrentUser?.Email ?? string.Empty;
            if (string.IsNullOrEmpty(recipientEmail))
                return 0;

            receptionistsId = _receptionistService.GetReceptionistIDByEmail(recipientEmail);
            return receptionistsId;
        }

        private int GetDoctorId()
        {
            if (doctorId != 0)
                return doctorId;

            string doctorEmail = SessionService.CurrentUser?.Email ?? string.Empty;
            if (string.IsNullOrEmpty(doctorEmail))
                return 0;

            doctorId = _doctorService.GetDoctorIDByEmail(doctorEmail);
            return doctorId;
        }

        private TimeSpan GetEndTime(TimeSpan startTime, ConsultationType type)
        {
            TimeSpan duration;

            switch (type)
            {
                case ConsultationType.FollowUp:
                    duration = TimeSpan.FromMinutes(15);
                    break;

                case ConsultationType.Examination:
                    duration = TimeSpan.FromMinutes(30);
                    break;

                case ConsultationType.MedicalConsultation:
                    duration = TimeSpan.FromMinutes(20);
                    break;

                default:
                    duration = TimeSpan.FromMinutes(30);
                    break;
            }

            return startTime.Add(duration);
        }

        private decimal GetAppointmentPrice(ConsultationType consultationType)
        {
            switch (consultationType)
            {
                case ConsultationType.MedicalConsultation:
                    return 100.00m;

                case ConsultationType.Examination:
                    return 200.00m;

                case ConsultationType.FollowUp:
                    return 75.00m;

                default:
                    return 100.00m;
            }
        }

        private TimeSpan GetStratTime(int doctorId, DateTime date)
        {
            try
            {
                return _appointmentRepository.LastEndTime(doctorId, date);
            }
            catch (Exception ex)
            {
                ConsoleHelper.PrintMessage(ex.Message, false);
                ConsoleHelper.PressEnterToContinue();
                return TimeSpan.Zero;
            }
        }
    }
}