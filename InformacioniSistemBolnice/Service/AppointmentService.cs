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
            AppointmentFileRepository.AddAppointment(appointment);
        }

        public void Remove(Appointment appointment)
        {
            AppointmentFileRepository.RemoveAppointment(appointment.AppointmentID);
        }

        public void Update(Appointment appointment)
        {
            //odraditi provere!
            AppointmentFileRepository.UpdateAppointment(appointment.AppointmentID, appointment);
        }

        public List<Appointment> GetAll()
        {
            return AppointmentFileRepository.GetAll();
        }

        public int GenerateNewId()
        {
            return AppointmentFileRepository.GetAll().Count + 1;
        }

        public Appointment GetOne(Appointment appointment)
        {
            return AppointmentFileRepository.GetOne(appointment.AppointmentID);
        }
    }
}
