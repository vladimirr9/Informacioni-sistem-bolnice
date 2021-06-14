using InformacioniSistemBolnice.Controller;
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
        private List<global::Doctor> _doctors;
        private List<Patient> _patients;
        private List<Room> _rooms;
        private List<String> _times;
        private Appointment _selectedAppointment;
        private bool _confirmed;
        private bool _done;
        private AppointmentController _appointmentController = new AppointmentController();
        private RoomController _roomController = new RoomController();
        private DoctorControler _doctorController = new DoctorControler();
        private PatientController _patientController = new PatientController();
        private AppointmentTimesController _appointmentTimesController = new AppointmentTimesController();
        public EditAppointmentWindow(Appointment selectedAppointment)
        {
            _done = false;
            this._selectedAppointment = selectedAppointment;
            InitializeComponent();
            InitializeComponentValues();

        }

        


        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (!UInt64.TryParse(AppointmentDuration.Text, out var res))
            {
                MessageBox.Show("Trajanje mora biti pozitivna celobrojna vrednost", "Nevalidan unos", MessageBoxButton.OK);
                return;
            }

            Patient patient = (Patient)PatientComboBox.SelectedItem;
            global::Doctor doctor = (global::Doctor)DoctorComboBox.SelectedItem;
            Room room = (Room)RoomComboBox.SelectedItem;
            String selectedTime = AppointmentTime.SelectedItem.ToString();
            String selectedDate = DatePicker.Text;
            DateTime selectedDateTime = DateTime.Parse(selectedDate + " " + selectedTime);
            AppointmentType appointmentType = (AppointmentType)AppointmentTypeComboBox.SelectedIndex;
            int duration = int.Parse(AppointmentDuration.Text);

            if (patient.IsAvailable(selectedDateTime, selectedDateTime.AddMinutes(duration)))
            {
                Appointment appointment = new Appointment(_selectedAppointment.AppointmentID, selectedDateTime, duration, appointmentType, AppointmentStatus.scheduled, patient, doctor, room);
                if (selectedDateTime < DateTime.Now)
                {
                    MessageBox.Show("Nije moguće zakazati pregled u prošlosti", "Greška u zakazivanju", MessageBoxButton.OK);
                    return;
                }
                _appointmentController.Update(appointment);
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

            _doctors = _doctorController.GetAvailableDoctorList(start, end);
            _rooms = _roomController.GetAvailableRoomList(start, end);

            DoctorComboBox.ItemsSource = _doctors;
            RoomComboBox.ItemsSource = _rooms;
            _done = true;
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
                int duration = int.Parse(AppointmentDuration.Text);

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
            foreach (Appointment appointment in _appointmentController.GetAll())
            {
                if (appointment.OccursOn(date) && appointment.InvolvesEither((Patient)PatientComboBox.SelectedItem, (global::Doctor)DoctorComboBox.SelectedItem) && appointment.AppointmentStatus == AppointmentStatus.scheduled)
                {
                    appointments.Add(appointment);
                }
            }
            AppointmentTime.ItemsSource = _appointmentTimesController.GetAvailableAppointmentTimes(appointments, (Doctor)DoctorComboBox.SelectedItem);
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
        private void EditAppointmentWindow_Closing(object sender, CancelEventArgs e)
        {
            if (_confirmed)
                return;
            _selectedAppointment.AppointmentStatus = AppointmentStatus.scheduled;
            _appointmentController.Update(_selectedAppointment);
            
        }
        private void InitializeComponentValues()
        {
            _confirmed = false;

            _doctors = _doctorController.GetAll();
            DoctorComboBox.ItemsSource = _doctors;
            _rooms = _roomController.GetAllRooms();
            RoomComboBox.ItemsSource = _rooms;
            _patients = new List<Patient>();

            foreach (Patient patient in _patientController.GetAll())
                if (!patient.IsDeleted)
                    _patients.Add(patient);
            PatientComboBox.ItemsSource = _patients;
            PatientComboBox.SelectedItem = _selectedAppointment.Patient;
            foreach (global::Doctor doctors in _doctors)
            {
                if (doctors.Equals(_selectedAppointment.Doctor))
                    DoctorComboBox.SelectedItem = doctors;
            }
            UpdateAvailableTimes();
            DatePicker.SelectedDate = _selectedAppointment.AppointmentDate;
            AppointmentTime.SelectedValue = _selectedAppointment.AppointmentDate.ToString("HH:mm");
            AppointmentDuration.Text = _selectedAppointment.DurationInMinutes.ToString();
            AppointmentTypeComboBox.SelectedIndex = (int)_selectedAppointment.Type;

            foreach (Room room in _rooms)
            {
                if (room.RoomId == _selectedAppointment.Room.RoomId)
                {
                    RoomComboBox.SelectedItem = room;
                }
            }
            _done = true;
        }
    }
}
