using InformacioniSistemBolnice.Controller;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Service
{
    public class AppointmentService
    {
        private RoomController _roomController = new RoomController();
        private DoctorControler _doctorController = new DoctorControler();
        private RatingService _ratingService = new RatingService();
        public void Add(Appointment appointment)
        {
            AppointmentFileRepository.AddAppointment(appointment);
        }

        public void Remove(Appointment appointment)
        {
            AppointmentFileRepository.RemoveAppointment(appointment.AppointmentID);
        }

        public void Update(Appointment appointment)
        {
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


        public List<Appointment> GetScheduledAppointmentsForPatient(Patient patient)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment a in GetScheduled())
            {
                if (a.PatientUsername.Equals(patient.Username) && !IsDateOfAppointmentPassed(a))
                {
                    appointments.Add(a);
                }
            }

            return appointments;
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

        public bool CreateNewUrgentAppointment(Patient patient, int duration, DoctorType doctorType, RoomType roomType, AppointmentType appointmentType, DateTime appointmentStart, DateTime appointmentEnd)
        {
            List<Room> availableRooms = _roomController.GetAvailableRoomList(appointmentStart, appointmentEnd);
            List<Doctor> availableDoctors = _doctorController.GetAvailableDoctorList(appointmentStart, appointmentEnd);

            List<Room> filteredRooms = _roomController.GetFilteredRooms(availableRooms, appointmentType);
            List<Doctor> filteredDoctors = _doctorController.GetFilteredDoctors(availableDoctors, doctorType);


            if (filteredRooms.Count > 0 && filteredDoctors.Count > 0)
            {
                foreach (Appointment appointmentItem in GetAll())
                {
                    if (appointmentItem.AppointmentStatus == AppointmentStatus.scheduled && appointmentItem.Patient.Equals(patient) && (appointmentItem.AppointmentDate >= appointmentStart && appointmentItem.AppointmentDate <= appointmentEnd))
                        Remove(appointmentItem);
                }


                Room room = filteredRooms[0];
                global::Doctor doctor = filteredDoctors[0];
                int id = GetAll().Count + 1;

                Appointment appointment = new Appointment(id, appointmentStart, duration, appointmentType, AppointmentStatus.scheduled, patient, doctor, room);
                Add(appointment);
                return true;
            }
            return false;
        }

        public void UpdateAppointmentsForDoctor(Doctor doctor)
        {
            foreach (var appointment in AppointmentFileRepository.GetAll())
            {
                if (appointment.AppointmentStatus != AppointmentStatus.scheduled)
                    continue;
                if (!appointment.Doctor.Equals(doctor))
                    continue;

                if (doctor.IsWithinVacations(appointment.AppointmentDate))
                    AppointmentFileRepository.RemoveAppointment(appointment.AppointmentID);
                if (!doctor.IsWithinWorkHours(appointment.AppointmentDate))
                    AppointmentFileRepository.RemoveAppointment(appointment.AppointmentID);
            }
        }

        private Boolean IsDateOfAppointmentPassed(Appointment appointment)
        {
            Boolean returnValue = false;
            if (appointment.AppointmentDate < DateTime.Now)
            {
                returnValue = true;
            }

            return returnValue;

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

        public void FinishAppointment(Appointment appointment)
        {
            appointment.AppointmentStatus = AppointmentStatus.finished;
            AppointmentFileRepository.UpdateAppointment(appointment.AppointmentID, appointment);

        }

        public Boolean IsScheduled(Appointment appointment)
        {
            if (appointment.AppointmentStatus.Equals(AppointmentStatus.scheduled))
            {
                return true;
            }

            return false;
        }

        public List<Appointment> GetPatientsAppointmentsInLastTenDays(Patient patient)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment t in GetAll())
            {
                if (!_ratingService.Contains(t.AppointmentID) &&
                    t.PatientUsername.Equals(patient.Username))
                {
                    if (DateTime.Now.AddDays(-10) < t.AppointmentDate && t.AppointmentDate.Date < DateTime.Now)
                    {
                        appointments.Add(t);
                    }
                }
            }

            return appointments;
        }
        public List<String> GetAvailableAppointmentTimes(List<Appointment> appointments)
        {
            List<String> times = new List<String>();
            DateTime lastPossibleTime = DateTime.Parse("01-Jan-1970" + " " + "19:30");
            for (DateTime potentialTime = DateTime.Parse("01-Jan-1970" + " " + "08:00"); potentialTime <= lastPossibleTime; potentialTime = potentialTime.AddMinutes(15))
            {
                bool free = true;
                foreach (Appointment appointment in appointments)
                {
                    DateTime start = DateTime.Parse("01-Jan-1970" + " " + appointment.AppointmentDate.ToString("HH:mm"));
                    DateTime end = DateTime.Parse("01-Jan-1970" + " " + appointment.AppointmentDate.AddMinutes(appointment.DurationInMinutes).ToString("HH:mm"));
                    if (potentialTime >= start && potentialTime <= end)
                    {
                        free = false;
                        break;
                    }
                }
                if (free)
                    times.Add(potentialTime.ToString("HH:mm"));
            }
            return times;
        }

        public List<string> GetPossibleAppointmentTimes()
        {
            List<string> times = new List<string>();
            DateTime lastPossibleTime = DateTime.Parse("01-Jan-1970" + " " + "19:30");
            for (DateTime potentialTime = DateTime.Parse("01-Jan-1970" + " " + "08:00"); potentialTime <= lastPossibleTime; potentialTime = potentialTime.AddMinutes(15))
            {
                times.Add(potentialTime.ToString("HH:mm"));
            }
            return times;
        }
        public DateTime GetNextEarliestAppointmentTime(DateTime datetime)
        {
            List<string> times = GetPossibleAppointmentTimes();
            while (!times.Contains(datetime.ToString("HH:mm")))
            {
                datetime = datetime.AddMinutes(1);
            }
            return datetime;
        }

        public void CheckMissedAppointments()
        {
            List<Appointment> appointments = GetScheduled();
            foreach (Appointment appointment in appointments)
            {
                if (appointment.AppointmentDate < DateTime.Now)
                {
                    appointment.AppointmentStatus = AppointmentStatus.missed;
                    AppointmentFileRepository.UpdateAppointment(appointment.AppointmentID, appointment);
                }
            }
        }

        public List<Appointment> GetAllApointmentsByRoom(Room room)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach(Appointment appointment in GetAll())
            {
                if(room.RoomId == appointment.Room.RoomId)
                {
                    appointments.Add(appointment);
                }
            }
            return appointments;
        }

        public void CancelAllRoomAppointments(Room room)
        {
            foreach(Appointment appointment in GetAllApointmentsByRoom(room))
            {
                appointment.AppointmentStatus = AppointmentStatus.cancelled;
            }
        }

        public DateTime GetFirstPossibleAppointmentTime()
        {
            return DateTime.Now.Date.AddHours(8);
        }
        public DateTime GetlastPossibleAppointmentTime()
        {
            return DateTime.Now.Date.AddHours(19).AddMinutes(30);

        }
    }
}
