using System;
using System.Collections.Generic;
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

namespace InformacioniSistemBolnice.Doctor_ns
{
    public partial class DoctorAddAppointmentWindow : Window
    {
        private DoctorWindow parent;
        private AppointmentController _appointmentController = new AppointmentController();
        private DoctorControler _doctorControler = new DoctorControler();
        private PatientController _patientController = new PatientController();

        public DoctorAddAppointmentWindow(DoctorWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            InitializeComboBoxes();
        }

        //odustani
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //potvrdi
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)PatientComboBox.SelectedItem;
            Doctor doctor = (Doctor)DoctorComboBox.SelectedItem;
            Room room = (Room)RoomComboBox.SelectedItem;
            if (time.SelectedIndex != -1)
            {
                ComboBoxItem item = time.SelectedItem as ComboBoxItem;
                DateTime dateTime = DateTime.Parse(date.Text + " " + item.Content.ToString());
                AppointmentType type = (AppointmentType)TypeComboBox.SelectedIndex;

                Appointment appointment = new Appointment(_appointmentController.GenerateNewId(), dateTime, 15, type, AppointmentStatus.scheduled, patient, doctor, room);
                _appointmentController.Add(appointment);

                AppointmentsPage.GetPage(parent).UpdateTable();
                this.Close();
            }
        }

        private void InitializeComboBoxes()                       //ubaciti kontrolere
        {
            DoctorComboBox.ItemsSource = _doctorControler.GetAll();
            PatientComboBox.ItemsSource = _patientController.GetAll();
            List<Room> rooms = RoomFileRepository.GetAll();
            RoomComboBox.ItemsSource = rooms;
        }
    }
}
