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
            DateTime lastPossibleTime = DateTime.Parse("01-Jan-1970" + " " + "19:30");
            for (DateTime potentialTime = DateTime.Parse("01-Jan-1970" + " " + "08:00"); potentialTime <= lastPossibleTime; potentialTime = potentialTime.AddMinutes(15))
            {
                bool free = true;
                if (doctor != null)
                {
                    if (doctor.IsWithinVacations(potentialTime) || !doctor.IsWithinWorkHours(potentialTime))
                    {
                        free = false;
                    }
                }
                foreach (Appointment appointment in appointments)
                {
                    DateTime start = DateTime.Parse("01-Jan-1970" + " " + appointment.AppointmentDate.ToString("HH:mm"));
                    DateTime end = DateTime.Parse("01-Jan-1970" + " " + appointment.AppointmentDate.AddMinutes(appointment.DurationInMinutes).ToString("HH:mm"));
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

        public List<string> GetPossibleAppointmentTimes()
        {
            List<string> times = new List<string>();
            DateTime lastPossibleTime = DateTime.Parse("01-Jan-1970" + " " + "19:30");
            for (DateTime potentialTime = DateTime.Parse("01-Jan-1970" + " " + "08:00"); potentialTime <= lastPossibleTime; potentialTime = potentialTime.AddMinutes(15))
            {
                times.Add(potentialTime.ToString("HH:mm"));
            }
            return times;
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
