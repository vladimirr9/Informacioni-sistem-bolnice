using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Service;

namespace InformacioniSistemBolnice.Controller
{
    public class AppointmentTimesController
    {
        private AppointmentTimesService _appointmentTimesService = new AppointmentTimesService();
        public List<string> GetPossibleAppointmentTimes()
        {
            return _appointmentTimesService.GetPossibleAppointmentTimes();
        }
        public DateTime GetNextEarliestAppointmentTime(DateTime datetime)
        {
            return _appointmentTimesService.GetNextEarliestAppointmentTime(datetime);
        }


        public DateTime GetFirstPossibleAppointmentTime()
        {
            return _appointmentTimesService.GetFirstPossibleAppointmentTime();
        }
        public DateTime GetLastPossibleAppointmentTime()
        {
            return _appointmentTimesService.GetlastPossibleAppointmentTime();
        }

        public List<String> GetAvailableAppointmentTimes(List<Appointment> appointments, Doctor doctor)
        {
            return _appointmentTimesService.GetAvailableAppointmentTimes(appointments, doctor);
        }
    }
}
