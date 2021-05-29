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
using InformacioniSistemBolnice.Lekar;

namespace InformacioniSistemBolnice
{
    public partial class DoctorWindow : Window
    {
        public Doctor Doctor;
        public DoctorWindow(Doctor doctor)
        {
            this.Doctor = doctor;
            InitializeComponent();
            UpdateTable();
            this.Title = doctor.ime + " " + doctor.prezime;
            Main.Content = ProfilePage.GetPage(this);
        }

        private void KartonClick(object sender, RoutedEventArgs e)
        {
            if (AppointmentsPage.GetSelected() != null)
            {
                Termin appointment = AppointmentsPage.GetSelected();
                MedicalRecordWindow kartonWindow = new MedicalRecordWindow(appointment, this);
                Application.Current.MainWindow = kartonWindow;
                kartonWindow.Show();
            }
        }

        private void PreglediClick(object sender, RoutedEventArgs e)
        {
            Main.Content = AppointmentsPage.GetPage(this);
        }

        private void LekoviClick(object sender, RoutedEventArgs e)
        {
            Main.Content = DrugsPage.GetPage(this);
        }

        private void ProfilClick(object sender, RoutedEventArgs e)
        {
            Main.Content = ProfilePage.GetPage(this);
        }

        private void OdjavaClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            this.Close();
        }

        public void UpdateTable()  //srediti za page
        {
            /*
            PrikazPregleda.Items.Clear();
            List<Termin> termini = TerminFileStorage.GetAll();
            foreach (Termin termin in termini)
            {
                if (termin.status == StatusTermina.zakazan)
                    PrikazPregleda.Items.Add(termin);
            }
            */
        }

    }
}
