using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Service;

namespace InformacioniSistemBolnice.Controller
{
    public class PatientsAppointmentController
    {
        private PatientsAppointmentService _patientsAppointmentService = new PatientsAppointmentService();
        public List<Appointment> GetScheduledAppointmentForPatient(Patient patient)
        {
            return _patientsAppointmentService.GetScheduledAppointmentsForPatient(patient);
        }



        public List<Appointment> PatientsAppointments(Patient patient)
        {
            return _patientsAppointmentService.PatientsAppointments(patient);
        }

        public List<Appointment> GetPatientsAppointmentsInLastTenDays(Patient patient)
        {
            return _patientsAppointmentService.GetPatientsAppointmentsInLastTenDays(patient);
        }
    }
}
