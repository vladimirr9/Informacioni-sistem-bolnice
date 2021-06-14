using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using InformacioniSistemBolnice.FileStorage;
using InformacioniSistemBolnice.User;

namespace InformacioniSistemBolnice.Service
{
    public class DoctorsAppointmentService
    {
        private NotificationService _notificationService = new NotificationService();
        private IAppointmentRepository _appointmentFileRepository = new AppointmentFileRepository();
        public void UpdateAppointmentsForDoctor(Doctor doctor)
        {
            foreach (var appointment in _appointmentFileRepository.GetAll())
            {
                if (appointment.AppointmentStatus != AppointmentStatus.scheduled)
                    continue;
                if (!appointment.Doctor.Equals(doctor))
                    continue;
                if (doctor.IsWithinVacations(appointment.AppointmentDate) || !doctor.IsWithinWorkHours(appointment.AppointmentDate))
                {
                    var notification = new Notification(_notificationService.GetAll().Count, "Otkazan termin", "Termin je otkazan jer Doktor nije u mogućnosti da prisustvuje", DateTime.Now);
                    notification.Recipients.Add(appointment.Patient.Username);
                    _notificationService.Create(notification);
                    _appointmentFileRepository.Remove(appointment.AppointmentID);
                }
            }
        }
    }
}
