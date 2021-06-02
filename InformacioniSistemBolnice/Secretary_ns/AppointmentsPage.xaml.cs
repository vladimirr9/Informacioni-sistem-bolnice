using InformacioniSistemBolnice.Controller;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace InformacioniSistemBolnice.Secretary_ns
{

    public partial class AppointmentsPage : Page
    {
        private AppointmentController _appointmentController = new AppointmentController();

        private static AppointmentsPage _instance;
        public DateTime? Filter { get; set; }
        private AppointmentsPage(SecretaryMain parent)
        {
            Filter = null;
            InitializeComponent();
            this.DataContext = this;
            UpdateTable();
        }
        public static AppointmentsPage GetPage(SecretaryMain parent)
        {
            if (_instance == null)
                _instance = new AppointmentsPage(parent);
            else
                _instance.UpdateTable();
            parent.Title.Content = "Termini";
            return _instance;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NewAppointmentWindow window = new NewAppointmentWindow(this);
            window.ShowDialog();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentPreview.SelectedItem == null)
                return;
            Appointment initialAppointment = _appointmentController.GetOne((Appointment)AppointmentPreview.SelectedItem);
            _appointmentController.Remove(initialAppointment);
            EditAppointmentWindow window = new EditAppointmentWindow(this, initialAppointment);
            window.ShowDialog();

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentPreview.SelectedItem == null)
                return;

            var result = MessageBox.Show("Da li ste sigurni da želite da obrišete ovaj termin?", "Potvrda brisanja", MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes)
                return;

            _appointmentController.Remove((Appointment)AppointmentPreview.SelectedItem);
            UpdateTable();


        }

        private void UrgentAppointment_Click(object sender, RoutedEventArgs e)
        {
            NewUrgentAppointment window = new NewUrgentAppointment(this);
            window.ShowDialog();
        }
        private void Date_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTable();
        }
        public void UpdateTable()
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment appointment in _appointmentController.GetAll())
            {
                if (appointment.AppointmentStatus == AppointmentStatus.scheduled)
                    if (Filter == null)
                        appointments.Add(appointment);
                    else
                    {
                        if (appointment.AppointmentDate.Date == ((DateTime)Filter).Date)
                            appointments.Add(appointment);
                    }
            }
            appointments.Sort((x, y) => DateTime.Compare(x.AppointmentDate, y.AppointmentDate));
            AppointmentPreview.ItemsSource = appointments;
        }


    }
}
