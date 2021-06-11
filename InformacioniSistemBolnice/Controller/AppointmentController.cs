using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Service;

namespace InformacioniSistemBolnice.Controller
{
    class AppointmentController
    {
        private AppointmentService _appointmentService = new AppointmentService();

        public void Add(Appointment appointment)
        {
            _appointmentService.Add(appointment);
        }

        public void Remove(Appointment appointment)
        {
            _appointmentService.Remove(appointment);
        }

        public void Update(Appointment appointment)
        {
            _appointmentService.Update(appointment);
        }

        public int GenerateNewId()
        {
            return _appointmentService.GenerateNewId();
        }

        public List<Appointment> GetAll()
        {
            return _appointmentService.GetAll();
        }

        public Appointment GetOne(Appointment appointment)
        {
            return _appointmentService.GetOne(appointment);
        }

        public List<Appointment> GetScheduled()
        {
            return _appointmentService.GetScheduled();
        }


        public List<Appointment> GetScheduledAppointmentForPatient(Patient patient)
        {
            return _appointmentService.GetScheduledAppointmentsForPatient(patient);
        }

        public Boolean IsAppointmentTomorrow(Appointment appointment)
        {
            return _appointmentService.IsAppointmentTomorrow(appointment);
        }

        public List<Appointment> PatientsAppointments(Patient patient)
        {
            return _appointmentService.PatientsAppointments(patient);
        }

        public void FinishAppointment(Appointment appointment)
        {
            _appointmentService.FinishAppointment(appointment);
        }

        public Boolean IsScheduled(Appointment appointment)
        {
            return _appointmentService.IsScheduled(appointment);
        }

        public List<Appointment> GetPatientsAppointmentsInLastTenDays(Patient patient)
        {
            return _appointmentService.GetPatientsAppointmentsInLastTenDays(patient);
        }
        public List<String> GetAvailableAppointmentTimes(List<Appointment> appointments)
        {
            return _appointmentService.GetAvailableAppointmentTimes(appointments);
        }

        public bool CreateNewUrgentAppointment(Patient patient, int duration, DoctorType doctorType, RoomType roomType, AppointmentType appointmentType, DateTime appointmentStart, DateTime appointmentEnd)
        {
            return _appointmentService.CreateNewUrgentAppointment(patient, duration, doctorType, roomType, appointmentType, appointmentStart, appointmentEnd);
        }

        public List<string> GetPossibleAppointmentTimes()
        {
            return _appointmentService.GetPossibleAppointmentTimes();
        }
        public DateTime GetNextEarliestAppointmentTime(DateTime datetime)
        {
            return _appointmentService.GetNextEarliestAppointmentTime(datetime);
        }

        public void CheckMissedAppointments()
        {
            _appointmentService.CheckMissedAppointments();
        }
        public void CancelAllRoomAppointments(Room room)
        {
            _appointmentService.CancelAllRoomAppointments(room);
        }

    }
}
