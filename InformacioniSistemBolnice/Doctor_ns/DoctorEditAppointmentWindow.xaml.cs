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

namespace InformacioniSistemBolnice.Doctor_ns
{
    public partial class DoctorEditAppointmentWindow : Window
    {
        private DoctorWindow parent;
        private Appointment selected;
        private AppointmentController _appointmentController = new AppointmentController();

        public DoctorEditAppointmentWindow(Appointment selected, DoctorWindow parent)
        {
            this.selected = selected;
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
                String t = item.Content.ToString();
                String d = date.Text;
                DateTime dateTime = DateTime.Parse(d + " " + t);
                AppointmentType type = (AppointmentType)TypeComboBox.SelectedIndex;
                Appointment appointment = new Appointment(selected.AppointmentID, dateTime, 15, type, AppointmentStatus.scheduled, patient, doctor, room);

                _appointmentController.Update(appointment);

                AppointmentsPage.GetPage(parent).UpdateTable();
                this.Close();
            }
        }

        private void InitializeComboBoxes()                         //ubaciti kontrolere
        {
            List<Doctor> doctors = DoctorFileRepository.GetAll();
            DoctorComboBox.ItemsSource = doctors;
            List<Patient> patients = PatientFileRepository.GetAll();
            PatientComboBox.ItemsSource = patients;
            List<Room> rooms = RoomFileRepository.GetAll();
            RoomComboBox.ItemsSource = rooms;

            date.SelectedDate = selected.AppointmentDate;
            time.SelectedValue = selected.AppointmentDate.ToString("HH:mm");

            foreach (Doctor doctor in doctors)
            {
                if (doctor.JMBG == selected.Doctor.JMBG)
                    DoctorComboBox.SelectedItem = doctor;
            }

            foreach (Patient patient in patients)
            {
                if (patient.JMBG != null && patient.JMBG == selected.Patient.JMBG)
                    PatientComboBox.SelectedItem = patient;
            }

            TypeComboBox.SelectedIndex = (int)selected.Type;

            foreach (Room room in rooms)
            {
                if (room.RoomId == selected.Room.RoomId)
                    RoomComboBox.SelectedItem = room;
            }
        }
    }
}
