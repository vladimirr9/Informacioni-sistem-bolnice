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
using InformacioniSistemBolnice.Controller;
using Microsoft.Win32;

namespace InformacioniSistemBolnice.Doctor_ns
{
    public partial class AppointmentsPage : Page
    {
        public DoctorWindow parent;
        private static AppointmentsPage instance;
        private AppointmentController _appointmentController = new AppointmentController();

        public AppointmentsPage(DoctorWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            _appointmentController.CheckMissedAppointments();
            UpdateTable();
        }

        public static AppointmentsPage GetPage(DoctorWindow parent)
        {
            if (instance == null)
                instance = new AppointmentsPage(parent);
            return instance;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            DoctorAddAppointmentWindow addWindow = new DoctorAddAppointmentWindow(parent);
            Application.Current.MainWindow = addWindow;
            addWindow.Show();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentsDataGrid.SelectedItem != null)
            {
                DoctorEditAppointmentWindow editWindow = new DoctorEditAppointmentWindow(_appointmentController.GetOne((Appointment)AppointmentsDataGrid.SelectedItem), parent);
                Application.Current.MainWindow = editWindow;
                editWindow.Show();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentsDataGrid.SelectedItem != null)
            {
                _appointmentController.Remove(((Appointment)AppointmentsDataGrid.SelectedItem));
                UpdateTable();
            }
        }

        private void Double_Click(object sender, RoutedEventArgs e)
        {
            Appointment appointment = (Appointment) AppointmentsDataGrid.SelectedItem;
            MedicalRecordWindow recordWindow = new MedicalRecordWindow(appointment.Patient, parent, appointment);
            Application.Current.MainWindow = recordWindow;
            recordWindow.Show();
        }

        public void UpdateTable()
        {
            AppointmentsDataGrid.Items.Clear();
            foreach (Appointment appointment  in _appointmentController.GetScheduled())
            {
                AppointmentsDataGrid.Items.Add(appointment);
            }
        }
    }
}
