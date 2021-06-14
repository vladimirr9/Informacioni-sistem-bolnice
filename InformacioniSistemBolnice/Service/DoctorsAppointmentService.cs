using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Service
{
    public class DoctorsAppointmentService
    {
        private IAppointmentRepository _appointmentFileRepository = new AppointmentFileRepository();
        public void UpdateAppointmentsForDoctor(Doctor doctor)
        {
            foreach (var appointment in _appointmentFileRepository.GetAll())
            {
                if (appointment.AppointmentStatus != AppointmentStatus.scheduled)
                    continue;
                if (!appointment.Doctor.Equals(doctor))
                    continue;

                if (doctor.IsWithinVacations(appointment.AppointmentDate))
                    _appointmentFileRepository.Remove(appointment.AppointmentID);
                if (!doctor.IsWithinWorkHours(appointment.AppointmentDate))
                    _appointmentFileRepository.Remove(appointment.AppointmentID);
            }
        }
    }
}
