using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Service
{
    public class UrgentAppointmentService
    {
        private RoomService _roomService = new RoomService();
        private DoctorService _doctorService = new DoctorService();
        private AppointmentService _appointmentService = new AppointmentService();
        public bool CreateNewUrgentAppointment(Patient patient, int duration, DoctorType doctorType, RoomType roomType, AppointmentType appointmentType, DateTime appointmentStart, DateTime appointmentEnd)
        {
            List<Room> availableRooms = _roomService.GetAvailableRoomList(appointmentStart, appointmentEnd);
            List<Doctor> availableDoctors = _doctorService.GetAvailableDoctorList(appointmentStart, appointmentEnd);

            List<Room> filteredRooms = _roomService.GetFilteredRooms(availableRooms, appointmentType);
            List<Doctor> filteredDoctors = _doctorService.GetFilteredDoctors(availableDoctors, doctorType);


            if (filteredRooms.Count > 0 && filteredDoctors.Count > 0)
            {
                foreach (Appointment appointmentItem in _appointmentService.GetAll())
                {
                    if (appointmentItem.AppointmentStatus == AppointmentStatus.scheduled && appointmentItem.Patient.Equals(patient) && (appointmentItem.AppointmentDate >= appointmentStart && appointmentItem.AppointmentDate <= appointmentEnd))
                        _appointmentService.Remove(appointmentItem);
                }


                Room room = filteredRooms[0];
                global::Doctor doctor = filteredDoctors[0];
                int id = _appointmentService.GetAll().Count + 1;

                Appointment appointment = new Appointment(id, appointmentStart, duration, appointmentType, AppointmentStatus.scheduled, patient, doctor, room);
                _appointmentService.Add(appointment);
                return true;
            }
            return false;
        }
    }
}
