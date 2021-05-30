using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Service
{
    public class AppointmentService
    {
        public void Add(Appointment appointment)
        {
            //odraditi provere!
            ApointmentFileRepository.AddAppointment(appointment);
        }

        public void Remove(Appointment appointment)
        {
            ApointmentFileRepository.RemoveAppointment(appointment.AppointmentID);
        }

        public void Update(Appointment appointment)
        {
            //odraditi provere!
            ApointmentFileRepository.UpdateAppointment(appointment.AppointmentID, appointment);
        }

        public List<Appointment> GetAll()
        {
            return ApointmentFileRepository.GetAll();
        }

        public int GenerateNewId()
        {
            return ApointmentFileRepository.GetAll().Count + 1;
        }

        public Appointment GetOne(Appointment appointment)
        {
            return ApointmentFileRepository.GetOne(appointment.AppointmentID);
        }
    }
}
