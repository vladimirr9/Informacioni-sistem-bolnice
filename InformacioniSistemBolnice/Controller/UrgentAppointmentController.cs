using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Service;

namespace InformacioniSistemBolnice.Controller
{
    public class UrgentAppointmentController
    {
        private UrgentAppointmentService _urgentAppointmentService = new UrgentAppointmentService();
        public bool CreateNewUrgentAppointment(Patient patient, int duration, DoctorType doctorType, RoomType roomType, AppointmentType appointmentType, DateTime appointmentStart, DateTime appointmentEnd)
        {
            return _urgentAppointmentService.CreateNewUrgentAppointment(patient, duration, doctorType, roomType, appointmentType, appointmentStart, appointmentEnd);
        }
    }
}
