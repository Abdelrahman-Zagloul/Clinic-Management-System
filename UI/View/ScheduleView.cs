using Clinic_Management_system.Helper;
using Clinic_Management_system.Models;
using Clinic_Management_system.Services.Implementations;
using Clinic_Management_system.Services.Interfaces;

namespace Clinic_Management_system.UI.View
{
    public class ScheduleView
    {
        private readonly IScheduleService _scheduleService;
        private readonly IDoctorService _doctorService;

        private int doctorId = 0;

        public ScheduleView(IScheduleService scheduleService, IDoctorService doctorService)
        {
            _scheduleService = scheduleService;
            _doctorService = doctorService;
        }

        public void AddSchedule()
        {
            try
            {
                var id = GetDoctorId();
                if (id == -1)
                {
                    ConsoleHelper.PrintMessage("\nDoctor not found.", false);
                    ConsoleHelper.PressEnterToContinue();
                    return;
                }

                if (_scheduleService.HasSchedule(id))
                {
                    ConsoleHelper.PrintMessage("\nYou Have already schedule. Only one schedule is allowed.", false);
                    ConsoleHelper.PressEnterToContinue();
                    return;
                }

                Schedule schedule = GetScheduleFromUser();
                schedule.DoctorId = id;

                var (isSuccess, message) = _scheduleService.Add(schedule);
                ConsoleHelper.PrintMessage(message, isSuccess);
                ConsoleHelper.PressEnterToContinue();
            }
            catch (Exception ex)
            {
                ConsoleHelper.PrintMessage($"An error occurred while adding the schedule: {ex.Message}", false);
                ConsoleHelper.PressEnterToContinue();
            }
        }

        public void UpdateSchedule()
        {
            try
            {
                var id = GetDoctorId();
                if (id == -1)
                {
                    ConsoleHelper.PrintMessage("\nDoctor not found.", false);
                    ConsoleHelper.PressEnterToContinue();
                    return;
                }

                if (!_scheduleService.HasSchedule(id))
                {
                    ConsoleHelper.PrintMessage("\nThis doctor doesn't have any schedule. First add a schedule, then you can make an update.", false);
                    ConsoleHelper.PressEnterToContinue();
                    return;
                }

                int scheduleId = _scheduleService.GetScheduleId(id);
                Schedule schedule = GetScheduleFromUser();
                schedule.DoctorId = id;

                var (isSuccess, message) = _scheduleService.Update(scheduleId, schedule);
                ConsoleHelper.PrintMessage(message, isSuccess);
                ConsoleHelper.PressEnterToContinue();
            }
            catch (Exception ex)
            {
                ConsoleHelper.PrintMessage(ex.Message, false);
                ConsoleHelper.PressEnterToContinue();
            }
        }

        public void RemoveSchedule()
        {
            try
            {
                var id = GetDoctorId();
                if (InputHelper.ConfirmAction("Remove Schedule"))
                {
                    if (id == -1)
                    {
                        ConsoleHelper.PrintMessage("\nDoctor not found.", false);
                        ConsoleHelper.PressEnterToContinue();
                        return;
                    }

                    if (!_scheduleService.HasSchedule(id))
                    {
                        ConsoleHelper.PrintMessage("\nThis doctor doesn't have any schedule to remove.", false);
                        ConsoleHelper.PressEnterToContinue();
                        return;
                    }

                    int scheduleId = _scheduleService.GetScheduleId(id);
                    var (isSuccess, message) = _scheduleService.Delete(scheduleId);
                    ConsoleHelper.PrintMessage(message, isSuccess);
                }
                else
                    ConsoleHelper.PrintMessage("\nOperation canceled.", true);
                ConsoleHelper.PressEnterToContinue();
            }
            catch (Exception ex)
            {
                ConsoleHelper.PrintMessage(ex.Message, false);
                ConsoleHelper.PressEnterToContinue();
            }
        }

        public void ShowScheduleInformation()
        {
            try
            {
                var id = GetDoctorId();
                if (id == -1)
                {
                    ConsoleHelper.PrintMessage("\nDoctor not found.", false);
                    ConsoleHelper.PressEnterToContinue();
                    return;
                }

                if (!_scheduleService.HasSchedule(id))
                {
                    ConsoleHelper.PrintMessage("\nThis doctor doesn't have any schedule to show.", false);
                    ConsoleHelper.PressEnterToContinue();
                    return;
                }

                int scheduleId = _scheduleService.GetScheduleId(id);
                var (isSuccess, message) = _scheduleService.GetById(scheduleId);
                ConsoleHelper.PrintMessage(message, isSuccess);
                ConsoleHelper.PressEnterToContinue();
            }
            catch (Exception ex)
            {
                ConsoleHelper.PrintMessage(ex.Message, false);
                ConsoleHelper.PressEnterToContinue();
            }
        }

        private Schedule GetScheduleFromUser()
        {
            return new Schedule
            {
                Saturday = InputHelper.AskForDay("Saturday"),
                Sunday = InputHelper.AskForDay("Sunday"),
                Monday = InputHelper.AskForDay("Monday"),
                Tuesday = InputHelper.AskForDay("Tuesday"),
                Wednesday = InputHelper.AskForDay("Wednesday"),
                Thursday = InputHelper.AskForDay("Thursday"),
                Friday = InputHelper.AskForDay("Friday"),
                StartTime = InputHelper.AskForTime("Enter start time (e.g. 09:00): "),
                EndTime = InputHelper.AskForTime("Enter end time (e.g. 17:00): ")
            };
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
    }
}