using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace InformacioniSistemBolnice.Secretary_ns
{

    public partial class AppointmentsPage : Page
    {
        private static AppointmentsPage _instance;
        public AppointmentsPage()
        {
            InitializeComponent();
            UpdateTable();
        }
        public static AppointmentsPage GetPage()
        {
            if (_instance == null)
                _instance = new AppointmentsPage();
            else
                _instance.UpdateTable();
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
            Termin initialAppointment = TerminFileStorage.GetOne(((Termin)(AppointmentPreview.SelectedItem)).iDTermina);
            TerminFileStorage.RemoveTermin(initialAppointment.iDTermina);
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

            TerminFileStorage.RemoveTermin(((Termin)AppointmentPreview.SelectedItem).iDTermina);
            UpdateTable();


        }

        private void UrgentAppointment_Click(object sender, RoutedEventArgs e)
        {
            NewUrgentAppointment window = new NewUrgentAppointment(this);
            window.ShowDialog();
        }
        public void UpdateTable()
        {
            AppointmentPreview.Items.Clear();
            List<Termin> appointments = TerminFileStorage.GetAll();
            foreach (Termin appointment in appointments)
            {
                if (appointment.status == StatusTermina.zakazan)
                    AppointmentPreview.Items.Add(appointment);
            }
        }


    }
}
