using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Service;

namespace InformacioniSistemBolnice.Controller
{
    public class StatusOfAppointmentController
    {
        private StatusOfAppointmentService _statusOfAppointmentService = new StatusOfAppointmentService();
        public List<Appointment> GetScheduled()
        {
            return _statusOfAppointmentService.GetScheduled();
        }
        public Boolean IsAppointmentTomorrow(Appointment appointment)
        {
            return _statusOfAppointmentService.IsAppointmentTomorrow(appointment);
        }
        public void FinishAppointment(Appointment appointment)
        {
            _statusOfAppointmentService.FinishAppointment(appointment);
        }
        public Boolean IsScheduled(Appointment appointment)
        {
            return _statusOfAppointmentService.IsScheduled(appointment);
        }
        public void CheckMissedAppointments()
        {
            _statusOfAppointmentService.CheckMissedAppointments();
        }
    }
}
