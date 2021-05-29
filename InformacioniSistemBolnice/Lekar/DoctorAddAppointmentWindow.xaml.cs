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

namespace InformacioniSistemBolnice.Lekar
{
    public partial class DoctorAddAppointmentWindow : Window
    {
        private DoctorWindow parent;
        public DoctorAddAppointmentWindow(DoctorWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            List<Doctor> doctors = LekarFileStorage.GetAll();
            DoctorComboBox.ItemsSource = doctors;
            List<Pacijent> patients = PacijentFileStorage.GetAll();
            PatientComboBox.ItemsSource = patients;
            List<Prostorija> rooms = ProstorijaFileStorage.GetAll();
            RoomComboBox.ItemsSource = rooms;
        }

        //odustani
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //potvrdi
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Pacijent patient = (Pacijent)PatientComboBox.SelectedItem;
            Doctor doctor = (Doctor)DoctorComboBox.SelectedItem;
            Prostorija room = (Prostorija)RoomComboBox.SelectedItem;
            if (time.SelectedIndex != -1)
            {
                ComboBoxItem item = time.SelectedItem as ComboBoxItem;
                String t = item.Content.ToString();
                String d = date.Text;
                DateTime dt = DateTime.Parse(d + " " + t);
                TipTermina type = (TipTermina)TypeComboBox.SelectedIndex;
                int id = TerminFileStorage.GetAll().Count + 1;
                Termin appointment = new Termin(id, dt, 15, type, StatusTermina.zakazan, patient, doctor, room);
                TerminFileStorage.AddTermin(appointment);
                AppointmentsPage.GetPage(parent).UpdateTable();
                this.Close();
            }
        }
    }
}
