using InformacioniSistemBolnice.Controller;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Doctor_ns
{
    public class WorkHours
    {
        AppointmentController _appointmentController = new AppointmentController();
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public List<WorkHourAberration> Aberrations { get; set; }

        public WorkHours()
        {
            Start = _appointmentController.GetFirstPossibleAppointmentTime();
            End = _appointmentController.GetLastPossibleAppointmentTime();
            Aberrations = new List<WorkHourAberration>();
        }
        public bool AberrationExists(DateTime date)
        {
            foreach (var aberration in Aberrations)
            {
                if (aberration.Date == date.Date)
                    return true;
            }
            return false;
        }
        public WorkHourAberration GetAberrationByDate(DateTime date)
        {
            foreach (var aberration in Aberrations)
            {
                if (aberration.Date == date.Date)
                    return aberration;
            }
            return null;
        }
    }
}
