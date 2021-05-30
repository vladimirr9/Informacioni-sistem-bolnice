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
        public NotificationPatientPage(StartPatientWindow pp)
        {
            parent = pp;
            loggedInPatient = pp.Patient;
            InitializeComponent();
            updateVisibility();
            this.DataContext = this;
            LoadNotifications();
            LoadReminders();
        }
        private void LoadNotifications()
        {
            
            foreach (Therapy therapy in _patientController.GetTherapiesFromMedicalRecord(loggedInPatient))
            {
                PrikazObavjestenja.Items.Add(therapy.Description);
            }
        }

        private void LoadReminders()
        {
            List<Anamnesis> anamneses = AnamnesisFileRepository.GetAll();
            foreach (Anamnesis a in anamneses)
            {
                if (a.UsernameOfPatient.Equals(parent.Patient.Username))
                {
                    foreach (Note n in a.NotesForAnamnesis)
                    {
                        if (n.IsSetReminder == true)
                        {
                            PrikazObavjestenja.Items.Add(n.DescriptionOfNote);
                        }
                    }
                }
            }
        }


        private void updateVisibility()
        {
            parent.titleLabel.Visibility = Visibility.Hidden;
            parent.titlePriorityLabel.Visibility = Visibility.Hidden;
            parent.odjava.Visibility = Visibility.Visible;
            parent.imePacijenta.Visibility = Visibility.Visible;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new StartPatientPage(parent);
        }
    }
}
