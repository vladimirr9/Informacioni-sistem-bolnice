using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public partial class NewAppointmentWindow : Window
    {
        private AppointmentsPage _parent;
        private List<global::Doctor> _doctors;
        private List<Patient> _patients;
        private List<Room> _rooms;
        private List<String> _times;
        public NewAppointmentWindow(AppointmentsPage parent)
        {
            this._parent = parent;


            InitializeComponent();

            InitializePatientValues();
            SetComponentIsEnabled();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            Patient selectedPatient = (Patient) PatientComboBox.SelectedItem;
            global::Doctor selectedDoctor = (global::Doctor)DoctorComboBox.SelectedItem;
            Room selectedRoom = (Room)RoomComboBox.SelectedItem;
            String selectedTime = AppointmentTime.SelectedItem.ToString();
            String selectedDate = DatePicker.Text;
            DateTime selectedDateTime = DateTime.Parse(selectedDate + " " + selectedTime);
            AppointmentType appointmentType = (AppointmentType)AppointmentTypeComboBox.SelectedIndex;
            int id = ApointmentFileRepository.GetAll().Count + 1;
            int duration = Int32.Parse(AppointmentDuration.Text);

            if (selectedPatient.IsAvailable(selectedDateTime, selectedDateTime.AddMinutes(duration)))
            {
                Appointment newAppointment = new Appointment(id, selectedDateTime, duration, appointmentType, AppointmentStatus.scheduled, selectedPatient, selectedDoctor, selectedRoom);
                ApointmentFileRepository.AddAppointment(newAppointment);
                _parent.UpdateTable();
                this.Close();
            }
            else
                MessageBox.Show("Pacijentu ne odgovara ovako dugo trajanje termina, preklapa se sa drugim obavezama", "Patient zauzet", MessageBoxButton.OK);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void Patient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetComponentIsEnabled();
            ResetComponentValues();
            UpdateComponents();
        }
        private void Doctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }

        private void Time_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }


        private void Room_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetComponentIsEnabled();
        }
        private void Date_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }

        private void Duration_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateComponents();
        }
        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetComponentIsEnabled();
        }
        private void UpdateComponents()
        {
            SetComponentIsEnabled();
            if (PatientComboBox.SelectedItem == null)
                return;

            DateTime start;
            DateTime end;

            CalculateStartAndEnd(out start, out end);

            UpdateAvailableTimes();

            ColorDurationField(start, end);
            UpdateAvailableDoctorList(start, end);
            UpdateAvailableRoomList(start, end);

            DoctorComboBox.ItemsSource = _doctors;
            RoomComboBox.ItemsSource = _rooms;
        }
        private void UpdateAvailableRoomList(DateTime start, DateTime end)
        {
            _rooms = new List<Room>();
            foreach (Room room in RoomFileRepository.GetAll())
            {
                if (room.IsAvailable(start, end) && !room.IsDeleted)
                {
                    _rooms.Add(room);
                }
            }
        }
        private void UpdateAvailableDoctorList(DateTime start, DateTime end)
        {
            _doctors = new List<global::Doctor>();
            foreach (global::Doctor doctor in DoctorFileRepository.GetAll())
            {
                if (doctor.IsAvailable(start, end) && !doctor.IsDeleted)
                {
                    _doctors.Add(doctor);
                }
            }
        }
        private void ColorDurationField(DateTime start, DateTime end)
        {
            if (((Patient)(PatientComboBox.SelectedItem)).IsAvailable(start, end))
                AppointmentDuration.Background = Brushes.White;
            else
                AppointmentDuration.Background = Brushes.Red;
        }

        private void CalculateStartAndEnd(out DateTime start, out DateTime end)
        {
            if (AppointmentTime.SelectedItem != null && DatePicker.SelectedDate != null && AppointmentDuration.Text != "")
            {
                String selectedTime = AppointmentTime.SelectedItem.ToString();
                String selectedDate = DatePicker.Text;
                int duration = Int32.Parse(AppointmentDuration.Text);

                start = DateTime.Parse(selectedDate + " " + selectedTime);
                end = start.AddMinutes(duration);
            }
            else
            {
                start = DateTime.Now;
                end = start;
            }
        }

        private void SetComponentIsEnabled()
        {
            DoctorComboBox.IsEnabled = (PatientComboBox.SelectedItem != null);
            DatePicker.IsEnabled = (PatientComboBox.SelectedItem != null);
            AppointmentTime.IsEnabled = (PatientComboBox.SelectedItem != null);
            AppointmentDuration.IsEnabled = (PatientComboBox.SelectedItem != null);
            AppointmentTypeComboBox.IsEnabled = (PatientComboBox.SelectedItem != null && DoctorComboBox.SelectedItem != null && AppointmentTime.SelectedItem != null && DatePicker.SelectedDate != null && AppointmentDuration.Text != "");
            RoomComboBox.IsEnabled = (PatientComboBox.SelectedItem != null && DoctorComboBox.SelectedItem != null && AppointmentTime.SelectedItem != null && DatePicker.SelectedDate != null && AppointmentDuration.Text != "");
            ConfirmButton.IsEnabled = (PatientComboBox.SelectedItem != null && DoctorComboBox.SelectedItem != null && AppointmentTime.SelectedItem != null && DatePicker.SelectedDate != null && AppointmentDuration.Text != "" && AppointmentTypeComboBox.SelectedItem != null && RoomComboBox.SelectedItem != null);
        }
        private void UpdateAvailableTimes()
        {
            DateTime date;
            if (DatePicker.SelectedDate != null)
                date = DateTime.Parse(DatePicker.Text);
            else
                date = DateTime.Now;


            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment appointment in ApointmentFileRepository.GetAll())
            {
                if (appointment.OccursOn(date) && appointment.InvolvesEither((Patient)PatientComboBox.SelectedItem, (global::Doctor)DoctorComboBox.SelectedItem) && appointment.AppointmentStatus == AppointmentStatus.scheduled)
                {
                    appointments.Add(appointment);
                }
            }
            AppointmentTime.ItemsSource = GetAvailableAppointmentTimes(appointments);
        }

        private List<String> GetAvailableAppointmentTimes(List<Appointment> appointments)
        {
            _times = new List<String>();
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
                    }
                }
                if (free)
                    _times.Add(potentialTime.ToString("HH:mm"));
            }
            return _times;
        }

        private void ResetComponentValues()
        {
            DoctorComboBox.SelectedIndex = -1;
            DatePicker.SelectedDate = null;
            AppointmentTime.SelectedIndex = -1;
            AppointmentDuration.Clear();
            AppointmentTypeComboBox.SelectedItem = null;
            RoomComboBox.SelectedIndex = -1;
        }
        private void InitializePatientValues()
        {
            _patients = new List<Patient>();
            foreach (Patient patient in PatientFileRepository.GetAll())
                if (!patient.IsDeleted)
                    _patients.Add(patient);
            PatientComboBox.ItemsSource = _patients;
        }
    }
    
}
