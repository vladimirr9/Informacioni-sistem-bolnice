using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.View.ViewModel;

namespace InformacioniSistemBolnice.Doctor_ns
{
    public partial class DoctorAddAppointmentWindow : Window
    {
        private DoctorWindow parent;
        private AppointmentController _appointmentController = new AppointmentController();
        private DoctorControler _doctorControler = new DoctorControler();
        private PatientController _patientController = new PatientController();
        private RoomController _roomController = new RoomController();

        public DoctorAddAppointmentWindow(DoctorWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            InitializeComboBoxes();
        }

        private void Abandon_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)PatientComboBox.SelectedItem;
            Doctor doctor = (Doctor)DoctorComboBox.SelectedItem;
            Room room = (Room)RoomComboBox.SelectedItem;
            if (ConfirmCheck())
            {
                ComboBoxItem item = time.SelectedItem as ComboBoxItem;
                DateTime dateTime = DateTime.Parse(date.Text + " " + item.Content.ToString());
                AppointmentType type = (AppointmentType)TypeComboBox.SelectedIndex;

                Appointment appointment = new Appointment(_appointmentController.GenerateNewId(), dateTime, 15, type, AppointmentStatus.scheduled, patient, doctor, room);
                _appointmentController.Add(appointment);

                parent.Main.Content = new AppointmentsPage(parent);
                this.Close();
            }
        }

        private void Selection_Changed(object sender, SelectionChangedEventArgs e)
        {
            ConfirmCheck();
        }

        private void Date_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (date.SelectedDate != null && time.SelectedIndex != -1)
            {
                ComboBoxItem item = time.SelectedItem as ComboBoxItem;
                DateTime start = DateTime.Parse(date.Text + " " + item.Content.ToString());
                RoomComboBox.ItemsSource = _roomController.GetAvailableRoomList(start, start.AddMinutes(15));
                DoctorComboBox.ItemsSource = _doctorControler.GetAvailableDoctorList(start, start.AddMinutes(15));
                PatientComboBox.ItemsSource = _patientController.GetAvailablePatientList(start, start.AddMinutes(15));
            }

            ConfirmCheck();
        }

        private void InitializeComboBoxes()
        {
            DoctorComboBox.ItemsSource = _doctorControler.GetAll();
            PatientComboBox.ItemsSource = _patientController.GetAll();
            date.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
            ConfirmButton.IsEnabled = false;
        }

        private bool ConfirmCheck()
        {
            if (DoctorComboBox.SelectedIndex != -1 && PatientComboBox.SelectedIndex != -1 && date.SelectedDate != null && time.SelectedIndex != -1 &&TypeComboBox.SelectedIndex != -1 && RoomComboBox.SelectedIndex != -1)
            {
                ConfirmButton.IsEnabled = true;
                return true;
            }
            ConfirmButton.IsEnabled = false;
            return false;
        }
    }
}
