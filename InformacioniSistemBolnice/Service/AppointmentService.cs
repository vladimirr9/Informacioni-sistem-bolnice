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

        public List<Appointment> GetScheduled()
        {
            List<Appointment> scheduled = new List<Appointment>();
            foreach (Appointment appointment in AppointmentFileRepository.GetAll())
            {
                if (appointment.AppointmentStatus == AppointmentStatus.scheduled)
                {
                      scheduled.Add(appointment);  
                }
            }
            return scheduled;
        }

        public List<Appointment> PatientsAppointments(Patient patient)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment appointment in AppointmentFileRepository.GetAll())
            {
                if (appointment.Patient.Equals(patient) && appointment.AppointmentStatus != AppointmentStatus.cancelled && appointment.AppointmentStatus != AppointmentStatus.missed)
                {
                    appointments.Add(appointment);
                } 
            }
            return appointments;
        }

        public void FinishAppointment(Appointment appointment)
        {
            appointment.AppointmentStatus = AppointmentStatus.finished;
            AppointmentFileRepository.UpdateAppointment(appointment.AppointmentID, appointment);
        }
    }
}
