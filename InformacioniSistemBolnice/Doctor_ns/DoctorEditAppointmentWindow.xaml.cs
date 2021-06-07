using System;
using System.Collections.Generic;
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
    public partial class DoctorEditAppointmentWindow : Window
    {
        private DoctorWindow parent;
        private Appointment selected;
        private AppointmentController _appointmentController = new AppointmentController();
        private DoctorControler _doctorControler = new DoctorControler();
        private PatientController _patientController = new PatientController();
        private RoomController _roomController = new RoomController();

        public DoctorEditAppointmentWindow(Appointment selected, DoctorWindow parent)
        {
            this.selected = selected;
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
            if (time.SelectedIndex != -1)
            {
                ComboBoxItem timeItem = time.SelectedItem as ComboBoxItem;
                DateTime dateTime = DateTime.Parse(date.Text + " " + timeItem.Content.ToString());
                AppointmentType type = (AppointmentType)TypeComboBox.SelectedIndex;
                Appointment appointment = new Appointment(selected.AppointmentID, dateTime, 15, type, AppointmentStatus.scheduled, patient, doctor, room);

                _appointmentController.Update(appointment);

                AppointmentsViewModel.GetPage(parent).UpdateTable();
                this.Close();
            }
        }

        private void InitializeComboBoxes()
        {
            DoctorComboBox.ItemsSource = _doctorControler.GetAll();
            PatientComboBox.ItemsSource = _patientController.GetAll();
            RoomComboBox.ItemsSource = _roomController.GetAllRooms();

            date.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
            date.SelectedDate = selected.AppointmentDate;
            time.SelectedValue = selected.AppointmentDate.ToString("HH:mm");
            TypeComboBox.SelectedIndex = (int)selected.Type;

            DoctorComboBox.SelectedItem = selected.Doctor;
            PatientComboBox.SelectedItem = selected.Patient;
            RoomComboBox.SelectedItem = selected.Room;
        }
    }
}
