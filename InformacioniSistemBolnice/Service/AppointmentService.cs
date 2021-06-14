using InformacioniSistemBolnice.Controller;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Service
{
    public class AppointmentService
    {
        private IAppointmentRepository _appointmentFileRepository = new AppointmentFileRepository();
        public void Add(Appointment appointment)
        {
            _appointmentFileRepository.Add(appointment);
        }

        public void Remove(Appointment appointment)
        {
            _appointmentFileRepository.Remove(appointment.AppointmentID);
        }

        public void Update(Appointment appointment)
        {
            _appointmentFileRepository.Update(appointment.AppointmentID, appointment);
        }

        public List<Appointment> GetAll()
        {
            return _appointmentFileRepository.GetAll();
        }

        public int GenerateNewId()
        {
            return _appointmentFileRepository.GetAll().Count + 1;
        }

        public Appointment GetOne(Appointment appointment)
        {
            return _appointmentFileRepository.GetOne(appointment.AppointmentID);
        }

        

        

        
        
       
    }
}
