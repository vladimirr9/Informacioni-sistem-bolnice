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
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Patient_ns
{
    /// <summary>
    /// Interaction logic for RatingPage.xaml
    /// </summary>
    public partial class RatingPage : Page
    {
        private StartPatientWindow parent;
        private Appointment selektovan;
        public Button kojiJePritisnut;
        public RatingPage(StartPatientWindow pp)
        {
            parent = pp;
            InitializeComponent();
            PretraziTermine();
            DataContext = this;
            rate.IsEnabled = false;
            rateHospital.Visibility = Visibility.Hidden;
            Provjera();
        }

        private void Provjera()
        {
            List<Rating> ocjenjivanjeBolnice = new List<Rating>();

            foreach (Rating a in RatingFileRepository.GetAll())
            {
                if (a.UsernameOfDoctor == null && a.IdOfAppointment == 0)
                {
                    if (a.UsernameOfPatient.Equals(parent.Patient.Username))
                    {
                        ocjenjivanjeBolnice.Add(a);
                    }
                }
            }

            DateTime posljednjaNapisana = DateTime.Parse("1970-01-01" + " " + "00:00:00");
            if (ocjenjivanjeBolnice.Count != 0)
            {
                posljednjaNapisana = ocjenjivanjeBolnice.ElementAt(0).DateOfWritingRating;
            }

            foreach (Rating a in ocjenjivanjeBolnice)
            {
                if (posljednjaNapisana < a.DateOfWritingRating)
                {
                    posljednjaNapisana = a.DateOfWritingRating;

                }
            }

            if (ocjenjivanjeBolnice.Count == 0 || posljednjaNapisana.AddSeconds(15) < DateTime.Now)
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


            foreach (Appointment t in ApointmentFileRepository.GetAll())
            {
                if (t.AppointmentStatus == AppointmentStatus.scheduled && !RatingFileRepository.Contains(t.AppointmentID) && t.PatientUsername.Equals(parent.Patient.Username))
                {
                    if (DateTime.Now.AddDays(-10) < t.AppointmentDate && t.AppointmentDate.Date < DateTime.Now)
                    {
                        PrikazPregleda.Items.Add(t);
                    }
                }

            }

        }

        private void rate_Click(object sender, RoutedEventArgs e)
        {
            kojiJePritisnut = rate;
            selektovan = (Appointment)PrikazPregleda.SelectedItem;
            parent.imeLjekara.Visibility = Visibility.Visible;
            PatientRatesPage pa = new PatientRatesPage(this, parent, selektovan);
            parent.startWindow.Content = pa;
            return;


        }

        private void rateHospital_Click(object sender, RoutedEventArgs e)
        {
            kojiJePritisnut = rateHospital;
            PatientRatesPage pa = new PatientRatesPage(this, parent, selektovan);
            parent.startWindow.Content = pa;
            return;
        }

        public void UpdateTable()
        {

            PrikazPregleda.Items.Remove(selektovan);
        }

        private void PrikazPregleda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rate.IsEnabled = true;
        }
    }
}
    

