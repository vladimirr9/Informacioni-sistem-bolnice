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

        public List<Appointment> PatientsAppointments(Patient patient)
        {
            return _appointmentService.PatientsAppointments(patient);
        }
    }
}
