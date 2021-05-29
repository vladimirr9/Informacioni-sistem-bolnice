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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace InformacioniSistemBolnice.Lekar
{
    public partial class AppointmentsPage : Page
    {
        public DoctorWindow parent;
        public Doctor Doctor;
        private static AppointmentsPage instance;
        public AppointmentsPage(DoctorWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            UpdateTable();
        }

        public static AppointmentsPage GetPage(DoctorWindow parent)
        {
            if (instance == null)
                instance = new AppointmentsPage(parent);
            return instance;
        }

        //dodavanje
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoctorAddAppointmentWindow addWindow = new DoctorAddAppointmentWindow(parent);
            Application.Current.MainWindow = addWindow;
            addWindow.Show();
        }

        //izmena
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (AppointmentsDataGrid.SelectedItem != null)
            {
                Termin appointment = TerminFileStorage.GetOne(((Termin)AppointmentsDataGrid.SelectedItem).iDTermina);
                DoctorEditAppointmentWindow editWindow = new DoctorEditAppointmentWindow(appointment, parent);
                Application.Current.MainWindow = editWindow;
                editWindow.Show();
            }
        }

        //brisanje
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (AppointmentsDataGrid.SelectedItem != null)
            {
                TerminFileStorage.RemoveTermin(((Termin)AppointmentsDataGrid.SelectedItem).iDTermina);
                UpdateTable();
            }
        }

        public void UpdateTable()
        {
            AppointmentsDataGrid.Items.Clear();
            List<Termin> appointments = TerminFileStorage.GetAll();
            foreach (Termin appointment in appointments)
            {
                if (appointment.status == StatusTermina.zakazan)
                    AppointmentsDataGrid.Items.Add(appointment);
            }
        }

        public static Termin GetSelected()
        {
            if (instance != null && instance.AppointmentsDataGrid.SelectedItem != null)
            {
                Termin appointment = TerminFileStorage.GetOne(((Termin)instance.AppointmentsDataGrid.SelectedItem).iDTermina);
                return appointment;
            }
            return null;
        }

    }
}
