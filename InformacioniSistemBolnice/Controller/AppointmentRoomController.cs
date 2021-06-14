using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Service;

namespace InformacioniSistemBolnice.Controller
{
    public class AppointmentRoomController
    {
        private AppointmentRoomService _appointmentRoomService = new AppointmentRoomService();
        public void CancelAllRoomAppointments(Room room)
        {
            _appointmentRoomService.CancelAllRoomAppointments(room);
        }
    }
}
