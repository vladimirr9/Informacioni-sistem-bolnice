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
    /// Interaction logic for PatientExaminesAppointmentPage.xaml
    /// </summary>
    public partial class PatientExaminesAppointmentPage : Page
    {
        private StartPatientWindow parent { get; set; }
        private PatientController _patientController = new PatientController();
        private ActivityLogController _activityLogController = new ActivityLogController();

        public PatientExaminesAppointmentPage(StartPatientWindow pp)
        {
            parent = pp;
            InitializeComponent();
            this.DataContext = this;
            updateTable();

        }

        public void updateTable()
        {
            PrikazPregleda.Items.Clear();
            List<Appointment> termini = ApointmentFileRepository.GetAll();
            foreach (Appointment termin in termini)
            {
                if (parent.Patient.Username == termin.Patient.Username)
                {
                    if ((termin.AppointmentStatus == AppointmentStatus.scheduled) && (termin.AppointmentDate > DateTime.Now))
                        PrikazPregleda.Items.Add(termin);
                }
            }
        }


        private void back_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new StartPatientPage(parent);
        }

        private void cancelTermin_Click(object sender, RoutedEventArgs e)
        {
            Boolean isBanned = _patientController.CheckStatusOfPatient(parent.Patient);
            if (isBanned == true)
            {
                MessageBox.Show("Otkazivanje Vam je trenutno onemogućeno,obratite se sekretaru!", "Greška");
            }
            else
            {

                if (PrikazPregleda.SelectedItem != null)
                {
                    Appointment appointment = ApointmentFileRepository.GetOne(((Appointment) PrikazPregleda.SelectedItem).AppointmentID);
                    if (appointment.AppointmentDate.Date <= DateTime.Now.AddHours(24).Date)
                    {
                        MessageBox.Show("Nije moguće otkazati termin koji je zakazan u naredna 24 sata!", "Greška");
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show(
                            "Da li ste sigurni da želite da otkažete ovaj termin?", "Potvrda brisanja",
                            MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            ApointmentFileRepository.RemoveAppointment(((Appointment) (PrikazPregleda.SelectedItem)).AppointmentID);
                            updateTable();
                            ActivityLog informacija =
                                new ActivityLog(DateTime.Now, parent.Patient.Username,
                                    TypeOfActivity.cancelingAppointment);
                            Debug.WriteLine(informacija.UsernameOfPatient);
                            _activityLogController.AddActivity(informacija);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Prvo morate odabrati termin koji želite otkazati!", "Greška");

                }
            }

        }

        private void zakazi_Click(object sender, RoutedEventArgs e)
        {
            Boolean isBanned = _patientController.CheckStatusOfPatient(parent.Patient);
            if (isBanned == true)
            {
                MessageBox.Show("Zakazivanje Vam je trenutno onemogućeno,obratite se sekretaru!", "Greška");
            }
            else
            {
                parent.startWindow.Content = new PatientMakesAppointmentPage(parent);
            }
        }

        private void pomjeri_Click(object sender, RoutedEventArgs e)
        {
            Boolean isBanned = _patientController.CheckStatusOfPatient(parent.Patient);
            if (isBanned == true)
            {
                MessageBox.Show("Pomeranje termina Vam je trenutno onemogućeno,obratite se sekretaru!", "Greška");
            }
            else
            {
                if (PrikazPregleda.SelectedIndex != -1)
                {
                    Appointment appointment = ApointmentFileRepository.GetOne(((Appointment) PrikazPregleda.SelectedItem).AppointmentID);
                    if (appointment.AppointmentDate.Date <= DateTime.Now.AddHours(24).Date)
                    {
                        MessageBox.Show("Nije moguće menjati termin koji je zakazan u naredna 24 sata!", "Greška");
                    }
                    else
                    {
                        parent.UpdateVisibilityOfComponents();
                        parent.startWindow.Content = new PatientEditsAppointmentPage(appointment, parent);
                    }
                }
                else
                {
                    MessageBox.Show("Prvo morate odabrati termin koji želite pomeriti!", "Greška");

                }
            }
        }
    }
}
