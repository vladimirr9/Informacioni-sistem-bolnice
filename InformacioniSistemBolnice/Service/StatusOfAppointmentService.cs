using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Service
{
    public class StatusOfAppointmentService
    {
        private IAppointmentRepository _appointmentRepository = new AppointmentFileRepository();
        public void FinishAppointment(Appointment appointment)
        {
            appointment.AppointmentStatus = AppointmentStatus.finished;
            _appointmentRepository.Update(appointment.AppointmentID, appointment);

        }

        public Boolean IsScheduled(Appointment appointment)
        {
            if (appointment.AppointmentStatus.Equals(AppointmentStatus.scheduled))
            {
                return true;
            }

            return false;
        }

        public void CheckMissedAppointments()
        {
            List<Appointment> appointments = GetScheduled();
            foreach (Appointment appointment in appointments)
            {
                if (appointment.AppointmentDate < DateTime.Now)
                {
                    appointment.AppointmentStatus = AppointmentStatus.missed;
                    _appointmentRepository.Update(appointment.AppointmentID, appointment);
                }
            }
        }

        public List<Appointment> GetScheduled()
        {
            List<Appointment> scheduled = new List<Appointment>();
            foreach (Appointment appointment in _appointmentRepository.GetAll())
            {
                if (appointment.AppointmentStatus == AppointmentStatus.scheduled)
                {
                    scheduled.Add(appointment);
                }
            }
            return scheduled;
        }

        public Boolean IsAppointmentTomorrow(Appointment appointment)
        {
            Boolean returnValue = false;
            if (appointment.AppointmentDate.Date <= DateTime.Now.AddHours(24).Date && IsScheduled(appointment))
            {
                returnValue = true;
            }

            return returnValue;
        }
        public Boolean IsDateOfAppointmentPassed(Appointment appointment)
        {
            Boolean returnValue = false;
            if (appointment.AppointmentDate < DateTime.Now)
            {
                returnValue = true;
            }

            return returnValue;

        }
    }
}
