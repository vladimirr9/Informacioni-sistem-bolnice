using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InformacioniSistemBolnice.Secretary_ns
{
    /// <summary>
    /// Interaction logic for NewUrgentAppointment.xaml
    /// </summary>
    public partial class NewUrgentAppointment : Window
    {
        public AppointmentsPage _parent;
        private List<string> _patients;
        private List<string> _doctorTypes;
        public NewUrgentAppointment(AppointmentsPage parent)
        {
            this._parent = parent;
            InitializeComponent();
            InitializePatients();
            InitializeDoctorTypes();
        }


        

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            DoctorType doctorType = global::Doctor.DoctorTypeFromString(DoctorTypeCombo.SelectedItem.ToString());
            int duration = int.Parse(DurationInMinutes.Text);
            string jmbg = PatientsList.SelectedItem.ToString().Split('-')[1].Trim();
            Patient patient = PatientFileRepository.GetOneByJMBG(jmbg);
            DateTime appointmentStart = GetNextEarliestAppointmentTime(DateTime.Today.AddHours(DateTime.Now.TimeOfDay.Hours).AddMinutes(DateTime.Now.TimeOfDay.Minutes));
            
            DateTime appointmentEnd = appointmentStart.AddMinutes(duration);
            AppointmentType appointmentType;
            RoomType roomType;
            if (AppointmentTypeCombo.Text.Equals("Operacija"))
            {
                appointmentType = AppointmentType.operation;
                roomType = RoomType.operatingRoom;
            }
            else
            {
                appointmentType = AppointmentType.generalPractitionerCheckup;
                roomType = RoomType.examinationRoom;
            }
                

            List<Room> availableRooms = GetAvailableRooms(appointmentStart, appointmentEnd);
            List<global::Doctor> availableDoctors = GetAvailableDoctors(appointmentStart, appointmentEnd);

            List<Room> filteredRooms = GetFilteredRooms(availableRooms, appointmentType);
            List<global::Doctor> filteredDoctors = GetFilteredDoctors(availableDoctors, doctorType);


            if (filteredRooms.Count > 0 && filteredDoctors.Count > 0)
            {
                foreach (Appointment appointmentItem in AppointmentFileRepository.GetAll())
                {
                    if (appointmentItem.AppointmentStatus == AppointmentStatus.scheduled && appointmentItem.Patient.Equals(patient) && (appointmentItem.AppointmentDate >= appointmentStart && appointmentItem.AppointmentDate <= appointmentEnd))
                        AppointmentFileRepository.RemoveAppointment(appointmentItem.AppointmentID);
                }


                Room room = filteredRooms[0];
                global::Doctor doctor = filteredDoctors[0];
                int id = AppointmentFileRepository.GetAll().Count + 1;

                Appointment appointment = new Appointment(id, appointmentStart, duration, appointmentType, AppointmentStatus.scheduled, patient, doctor, room);
                AppointmentFileRepository.AddAppointment(appointment);
                _parent.UpdateTable();
                Close();
            }
            else
            {
                PostponeAppointmentWIndow postponeWindow = new PostponeAppointmentWIndow(this, patient, duration, doctorType, roomType, appointmentType, appointmentStart);
                postponeWindow.ShowDialog();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void NewGuestClick(object sender, RoutedEventArgs e)
        {
            NewGuestPatientWindow window = new NewGuestPatientWindow(this);
            window.ShowDialog();
        }




        private List<Room> GetFilteredRooms(List<Room> rooms, AppointmentType appointmentType)
        {
            List<Room> filteredRooms = new List<Room>();
            if (appointmentType == AppointmentType.operation)
            {
                foreach (Room room in rooms)
                {
                    if (room.RoomType == RoomType.operatingRoom)
                        filteredRooms.Add(room);
                }
            }
            else
            {
                foreach (Room room in rooms)
                {
                    if (room.RoomType == RoomType.examinationRoom)
                        filteredRooms.Add(room);
                }
            }
            return filteredRooms;
        }
        private List<global::Doctor> GetFilteredDoctors(List<global::Doctor> doctors, DoctorType doctorType)
        {
            List<global::Doctor> filteredDoctors = new List<global::Doctor>();
            foreach (global::Doctor doctor in doctors)
            {
                if (doctor.doctorType == doctorType)
                    filteredDoctors.Add(doctor);
            }
            return filteredDoctors;
        }

        private List<Room> GetAvailableRooms(DateTime pocetak, DateTime kraj)
        {
            List<Room> rooms = new List<Room>();
            foreach (Room room in RoomFileRepository.GetAll())
            {
                if (room.IsAvailable(pocetak, kraj) && !room.IsDeleted)
                {
                    rooms.Add(room);
                }
            }
            return rooms;
        }
        private List<global::Doctor> GetAvailableDoctors(DateTime pocetak, DateTime kraj)
        {
            List<global::Doctor> doctors = new List<global::Doctor>();
            foreach (global::Doctor doctor in DoctorFileRepository.GetAll())
            {
                if (doctor.IsAvailable(pocetak, kraj) && !doctor.IsDeleted)
                {
                    doctors.Add(doctor);
                }
            }
            return doctors;
        }
        private List<string> GetPossibleAppointmentTimes()
        {
            List<string> times = new List<string>();
            DateTime lastPossibleTime = DateTime.Parse("01-Jan-1970" + " " + "19:30");
            for (DateTime potentialTime = DateTime.Parse("01-Jan-1970" + " " + "08:00"); potentialTime <= lastPossibleTime; potentialTime = potentialTime.AddMinutes(15))
            {
                times.Add(potentialTime.ToString("HH:mm"));
            }
            return times;
        }
        private DateTime GetNextEarliestAppointmentTime(DateTime datetime)
        {
            List<string> times = GetPossibleAppointmentTimes();
            while (!times.Contains(datetime.ToString("HH:mm")))
            {
                datetime = datetime.AddMinutes(1);
            }
            return datetime;
        }

        public void InitializePatients()
        {
            _patients = new List<String>();
            foreach (Patient patient in PatientFileRepository.GetAll())
            {
                if (!patient.IsDeleted)
                    _patients.Add(patient.Name + " " + patient.Surname + " - " + patient.JMBG);
            }
            PatientsList.ItemsSource = _patients;
        }
        public void InitializeDoctorTypes()
        {
            _doctorTypes = global::Doctor.GetDoctorTypes();
            DoctorTypeCombo.ItemsSource = _doctorTypes;
        }

    }
}
