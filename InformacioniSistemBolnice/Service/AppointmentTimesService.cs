using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Service
{
    public class AppointmentTimesService
    {
        public List<String> GetAvailableAppointmentTimes(List<Appointment> appointments, Doctor doctor)
        {
            List<String> times = new List<String>();
            DateTime lastPossibleTime = GetLastPossibleTime();
            for (DateTime potentialTime = GetFirstPossibleTime(); potentialTime <= lastPossibleTime; potentialTime = potentialTime.AddMinutes(15))
            {
                if (!IsDoctorWorkingAtPotentialTime(doctor, potentialTime))
                    continue;
                bool free = true;
                foreach (Appointment appointment in appointments)
                {
                    DateTime start = GetStartTime(appointment);
                    DateTime end = GetEndTime(appointment);
                    if (potentialTime >= start && potentialTime <= end)
                    {
                        free = false;
                        break;
                    }
                }
                if (free)
                    times.Add(potentialTime.ToString("HH:mm"));
            }
            return times;
        }

       

        private bool IsDoctorWorkingAtPotentialTime(Doctor doctor, DateTime potentialTime)
        {
            if (doctor == null)
                return true;
            if (doctor.IsWithinVacations(potentialTime) || !doctor.IsWithinWorkHours(potentialTime))
            {
                return false;
            }
            return true;
        }

        public List<string> GetPossibleAppointmentTimes()
        {
            List<string> times = new List<string>();
            DateTime lastPossibleTime = GetLastPossibleTime();
            for (DateTime potentialTime = GetFirstPossibleTime(); potentialTime <= lastPossibleTime; potentialTime = potentialTime.AddMinutes(15))
            {
                times.Add(potentialTime.ToString("HH:mm"));
            }
            return times;
        }

        private DateTime GetFirstPossibleTime()
        {
            return DateTime.Parse("01-Jan-1970" + " " + "08:00");
        }
        private DateTime GetLastPossibleTime()
        {
            return DateTime.Parse("01-Jan-1970" + " " + "19:30");
        }

        private DateTime GetStartTime(Appointment appointment)
        {
            return DateTime.Parse("01-Jan-1970" + " " + appointment.AppointmentDate.ToString("HH:mm"));
        }
        private DateTime GetEndTime(Appointment appointment)
        {
            return DateTime.Parse("01-Jan-1970" + " " + appointment.AppointmentDate.AddMinutes(appointment.DurationInMinutes).ToString("HH:mm"));
        }


        public DateTime GetNextEarliestAppointmentTime(DateTime datetime)
        {
            List<string> times = GetPossibleAppointmentTimes();
            while (!times.Contains(datetime.ToString("HH:mm")))
            {
                datetime = datetime.AddMinutes(1);
            }
            return datetime;
        }

        public DateTime GetFirstPossibleAppointmentTime()
        {
            return DateTime.Now.Date.AddHours(8);
        }
        public DateTime GetlastPossibleAppointmentTime()
        {
            return DateTime.Now.Date.AddHours(19).AddMinutes(30);

        }
    }
}
