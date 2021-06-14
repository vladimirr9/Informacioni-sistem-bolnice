using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Service
{
    public class PatientsAppointmentService
    {
        private StatusOfAppointmentService _statusOfAppointmentService = new StatusOfAppointmentService();
        private IAppointmentRepository _appointmentRepository = new AppointmentFileRepository();
        private AppointmentService _appointmentService = new AppointmentService();
        private RatingService _ratingService = new RatingService();
        public List<Appointment> GetScheduledAppointmentsForPatient(Patient patient)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment a in _statusOfAppointmentService.GetScheduled())
            {
                if (a.PatientUsername.Equals(patient.Username) && !_statusOfAppointmentService.IsDateOfAppointmentPassed(a))
                {
                    appointments.Add(a);
                }
            }

            return appointments;
        }

        public List<Appointment> PatientsAppointments(Patient patient)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment appointment in _appointmentRepository.GetAll())
            {
                if (appointment.Patient.Equals(patient) && appointment.AppointmentStatus != AppointmentStatus.cancelled && appointment.AppointmentStatus != AppointmentStatus.missed)
                {
                    appointments.Add(appointment);
                }

            }
            return appointments;
        }

        public List<Appointment> GetPatientsAppointmentsInLastTenDays(Patient patient)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment t in _appointmentService.GetAll())
            {
                if (!_ratingService.Contains(t.AppointmentID) &&
                    t.PatientUsername.Equals(patient.Username))
                {
                    if (DateTime.Now.AddDays(-10) < t.AppointmentDate && t.AppointmentDate.Date < DateTime.Now)
                    {
                        appointments.Add(t);
                    }
                }
            }

            return appointments;
        }
    }
}
