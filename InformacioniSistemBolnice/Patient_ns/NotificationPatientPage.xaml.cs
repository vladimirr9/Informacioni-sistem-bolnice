using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Patient_ns
{
    /// <summary>
    /// Interaction logic for NotificationPatientPage.xaml
    /// </summary>
    public partial class NotificationPatientPage : Page
    {
        private StartPatientWindow parent;
        private Patient loggedInPatient;
        private PatientController _patientController = new PatientController();
        private AnamnesisController _anamnesisController = new AnamnesisController();
        private AppointmentController _appointmentController = new AppointmentController();
        public NotificationPatientPage(StartPatientWindow pp)
        {
            parent = pp;
            loggedInPatient = pp.Patient;
            InitializeComponent();
            updateVisibility();
            this.DataContext = this;
            LoadRemindersForAppointment();
            LoadNotifications();
            LoadReminders();
            
        }

        private void LoadRemindersForAppointment()
        {
            foreach (Appointment a in _appointmentController.GetAll())
            {
                if (_appointmentController.IsAppointmentTomorrow(a) &&
                    a.PatientUsername.Equals(loggedInPatient.Username))
                {
                    PrikazObavjestenja.Items.Add("Imate zakazan termin u " +
                                                 a.AppointmentDate.ToShortTimeString() + ".");
                }
            }
        }

        private void LoadNotifications()
        {
            foreach (Therapy t in _patientController.GetTherapiesFromMedicalRecord(loggedInPatient))
            {
                PrikazObavjestenja.Items.Add(t.Description);
            }
        }

        private void LoadReminders()
        {
            foreach (Note n in _anamnesisController.GetNotesWithReminder(loggedInPatient))
            {
                PrikazObavjestenja.Items.Add(n.DescriptionOfNote);
            }
        }

        private void updateVisibility()
        {
            parent.titleLabel.Visibility = Visibility.Hidden;
            parent.titlePriorityLabel.Visibility = Visibility.Hidden;
            parent.odjava.Visibility = Visibility.Visible;
            parent.iconAndName.Visibility = Visibility.Visible;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new StartPatientPage(parent);
        }
    }
}
