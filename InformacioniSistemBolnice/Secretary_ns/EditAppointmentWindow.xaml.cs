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
    public partial class EditAppointmentWindow : Window
    {
        private AppointmentsPage _parent;
        private List<global::Doctor> _doctors;
        private List<Patient> _patients;
        private List<Room> _rooms;
        private List<String> _times;
        private Appointment _selectedAppointment;
        private bool _confirmed;
        private bool _done;
        public EditAppointmentWindow(AppointmentsPage parent, Appointment selectedAppointment)
        {
            _done = false;
            InitializeComponent();
            this._selectedAppointment = selectedAppointment;
            this._parent = parent;
            _confirmed = false;

            _doctors = DoctorFileRepository.GetAll();
            DoctorComboBox.ItemsSource = _doctors;
            _rooms = RoomFileRepository.GetAll();
            RoomComboBox.ItemsSource = _rooms;

            _patients = new List<Patient>();
            
            
            foreach (Patient patient in PatientFileRepository.GetAll())
                if (!patient.IsDeleted)
                    _patients.Add(patient);
            PatientComboBox.ItemsSource = _patients;
            PatientComboBox.SelectedItem = selectedAppointment.Patient;
            foreach (global::Doctor doctors in _doctors)
            {
                if (doctors.Equals(selectedAppointment.Doctor))
                    DoctorComboBox.SelectedItem = doctors;
            }
            UpdateAvailableTimes();
            DatePicker.SelectedDate = selectedAppointment.AppointmentDate;
            AppointmentTime.SelectedValue = selectedAppointment.AppointmentDate.ToString("HH:mm");
            DurationTextBox.Text = selectedAppointment.DurationInMinutes.ToString();
            AppointmentTypeComboBox.SelectedIndex = (int)selectedAppointment.Type;




            foreach (Room room in _rooms)
            {
                if (room.RoomId == selectedAppointment.Room.RoomId)
                {
                    RoomComboBox.SelectedItem = room;
                }
            }


            _done = true;


        }

       

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)PatientComboBox.SelectedItem;
            global::Doctor doctor = (global::Doctor)DoctorComboBox.SelectedItem;
            Room room = (Room)RoomComboBox.SelectedItem;
            String selectedTime = AppointmentTime.SelectedItem.ToString();
            String selectedDate = DatePicker.Text;
            DateTime selectedDateTime = DateTime.Parse(selectedDate + " " + selectedTime);
            AppointmentType appointmentType = (AppointmentType)AppointmentTypeComboBox.SelectedIndex;
            int duration = int.Parse(DurationTextBox.Text);

            if (patient.IsAvailable(selectedDateTime, selectedDateTime.AddMinutes(duration)))
            {
                Appointment appointment = new Appointment(_selectedAppointment.AppointmentID, selectedDateTime, duration, appointmentType, AppointmentStatus.scheduled, patient, doctor, room);
                ApointmentFileRepository.UpdateAppointment(_selectedAppointment.AppointmentID,appointment);
                _parent.UpdateTable();
                _confirmed = true;
                this.Close();
            }
            else
                MessageBox.Show("Pacijentu ne odgovara ovako dugo trajanje, preklapa se sa drugim obavezama", "Patient zauzet", MessageBoxButton.OK);
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
        private void AppointmentType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetComponentIsEnabled();
        }
        private void UpdateComponents()
        {
            if (!_done)
                return;
            _done = false;
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
            _done = true;
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
                DurationTextBox.Background = Brushes.White;
            else
                DurationTextBox.Background = Brushes.Red;
        }

        private void CalculateStartAndEnd(out DateTime start, out DateTime end)
        {
            if (AppointmentTime.SelectedItem != null && DatePicker.SelectedDate != null && DurationTextBox.Text != "")
            {
                String selectedTime = AppointmentTime.SelectedItem.ToString();
                String selectedDate = DatePicker.Text;
                int duration = int.Parse(DurationTextBox.Text);

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
            DurationTextBox.IsEnabled = (PatientComboBox.SelectedItem != null);
            AppointmentTypeComboBox.IsEnabled = (PatientComboBox.SelectedItem != null && DoctorComboBox.SelectedItem != null && AppointmentTime.SelectedItem != null && DatePicker.SelectedDate != null && DurationTextBox.Text != "");
            RoomComboBox.IsEnabled = (PatientComboBox.SelectedItem != null && DoctorComboBox.SelectedItem != null && AppointmentTime.SelectedItem != null && DatePicker.SelectedDate != null && DurationTextBox.Text != "");
            ConfirmButton.IsEnabled = (PatientComboBox.SelectedItem != null && DoctorComboBox.SelectedItem != null && AppointmentTime.SelectedItem != null && DatePicker.SelectedDate != null && DurationTextBox.Text != "" && AppointmentTypeComboBox.SelectedItem != null && RoomComboBox.SelectedItem != null);
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
            DurationTextBox.Clear();
            AppointmentTypeComboBox.SelectedItem = null;
            RoomComboBox.SelectedIndex = -1;
        }
        private void EditAppointmentWindow_Closing(object sender, CancelEventArgs e)
        {
            if (_confirmed)
                return;
            _selectedAppointment.AppointmentStatus = AppointmentStatus.scheduled;
            ApointmentFileRepository.UpdateAppointment(_selectedAppointment.AppointmentID, _selectedAppointment);
            
        }
    }
}
