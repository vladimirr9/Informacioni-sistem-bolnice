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

namespace InformacioniSistemBolnice.Patient_ns
{
    /// <summary>
    /// Interaction logic for StartPatientPage.xaml
    /// </summary>
    public partial class StartPatientPage : Page
    {
        private static StartPatientWindow parent;
        private PatientController _patientController = new PatientController();

        public StartPatientPage(StartPatientWindow p)
        {
            parent = p;
            InitializeComponent();
        }






        private void pregledTermina_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new PatientExaminesAppointmentPage(parent);
        }

        private void obavjestenja_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new NotificationPatientPage(parent);
        }

        private void ocjenjivanje_Click(object sender, RoutedEventArgs e)
        {
            if (_patientController.CheckStatusOfPatient(parent.Patient) == true)
            {
                MessageBox.Show("Ova funkcionalnost Vam je trenutno onemogućena,obratite se sekretaru!", "Greška");
            }
            else
            {
                parent.startWindow.Content = new RatingPage(parent);
            }
        }

        private void karton_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new PatientMedicalRecordPage(parent);
        }

        private void feedback_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new PatientFeedbackPage(parent);
        }
    }

}