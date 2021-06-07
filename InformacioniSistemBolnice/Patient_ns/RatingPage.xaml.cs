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
using InformacioniSistemBolnice.FileStorage;
using InformacioniSistemBolnice.Service;

namespace InformacioniSistemBolnice.Patient_ns
{
    /// <summary>
    /// Interaction logic for RatingPage.xaml
    /// </summary>
    public partial class RatingPage : Page
    {
        private StartPatientWindow parent;
        private Appointment _selektovan;
        public Button _kojiJePritisnut;
        private RatingController _ratingController = new RatingController();
        private Patient _patient;
        private AppointmentController _appointmentController = new AppointmentController();
        public RatingPage(StartPatientWindow pp)
        {
            parent = pp;
            _patient = pp.Patient;
            InitializeComponent();
            PretraziTermine();
            DataContext = this;
            rateHospital.Visibility = Visibility.Hidden;
            Provjera();
        }

        private void Provjera()
        {
            if (_ratingController.IsCheckedCondition(_patient))
            {
                rateHospital.Visibility = Visibility.Visible;
            }

        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new StartPatientPage(parent);
        }

        private void PretraziTermine()
        {
            foreach (Appointment t in _appointmentController.GetPatientsAppointmentsInLastTenDays(_patient))
            {
                PrikazPregleda.Items.Add(t);
            }
        }

        private void rate_Click(object sender, RoutedEventArgs e)
        {
            _kojiJePritisnut = rate;
            if (PrikazPregleda.SelectedItem == null)
            {
               
                    MessageBox.Show("Morate selektovati termin kod lekara!", "Greška");
            }
            else
            {
                _selektovan = (Appointment) PrikazPregleda.SelectedItem;
                parent.iconAndNameDoctor.Visibility = Visibility.Visible;
                PatientRatesPage pa = new PatientRatesPage(this, parent, _selektovan);
                parent.startWindow.Content = pa;
            }

            return;


        }

        private void rateHospital_Click(object sender, RoutedEventArgs e)
        {
            _kojiJePritisnut = rateHospital;
            PatientRatesPage pa = new PatientRatesPage(this, parent, _selektovan);
            parent.startWindow.Content = pa;
            return;
        }

        public void UpdateTable()
        {

            PrikazPregleda.Items.Remove(_selektovan);
        }

        private void PrikazPregleda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rate.IsEnabled = true;
        }
    }
}
    

