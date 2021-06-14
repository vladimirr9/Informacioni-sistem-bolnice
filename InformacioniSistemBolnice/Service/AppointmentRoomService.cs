using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Service
{
    public class AppointmentRoomService
    {
        private IAppointmentRepository _appointmentRepository = new AppointmentFileRepository();
        public List<Appointment> GetAllApointmentsByRoom(Room room)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment appointment in _appointmentRepository.GetAll())
            {
                if (room.RoomId == appointment.Room.RoomId)
                {
                    appointments.Add(appointment);
                }
            }
            return appointments;
        }

        public void CancelAllRoomAppointments(Room room)
        {
            foreach (Appointment appointment in GetAllApointmentsByRoom(room))
            {
                appointment.AppointmentStatus = AppointmentStatus.cancelled;
            }
        }
    }
}
