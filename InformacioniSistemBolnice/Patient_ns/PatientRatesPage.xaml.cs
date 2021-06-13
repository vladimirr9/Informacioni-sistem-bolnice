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

namespace InformacioniSistemBolnice.Patient_ns
{
    /// <summary>
    /// Interaction logic for PatientRatesPage.xaml
    /// </summary>
    public partial class PatientRatesPage : Page
    {
        private StartPatientWindow parent;
        private Appointment selektovan;
        private RatingPage parentp;
        private RatingController _ratingController = new RatingController();
        public PatientRatesPage(RatingPage ap, StartPatientWindow pp, Appointment t)
        {
            parentp = ap;
            selektovan = t;
            parent = pp;
            InitializeComponent();
            submit.IsEnabled = false;
            if (parentp._kojiJePritisnut == parentp.rate)
            {
                parent.imeLjekara.Text = "dr. " + t.Doctor.Name + " " + t.Doctor.Surname;
            }
            else
            {
                parent.imeLjekara.Text = null;
            }
            InitializeComboBox();
            parent.UpdateVisibilityOfComponents();
        }



        private void InitializeComboBox()
        {
            for (int i = 1; i < 6; i++)
            {
                rateComboBox.Items.Add(i);
            }
        }

        private void ProvjeritiPopunjenostPolja()
        {
            if (commentText.Text != null && rateComboBox.SelectedItem != null)
            {
                submit.IsEnabled = true;
            }
        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            updateVisibility();
            parent.startWindow.Content = new RatingPage(parent);

        }

        private void rateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProvjeritiPopunjenostPolja();
        }

        private void commentText_TextChanged(object sender, TextChangedEventArgs e)
        {
            ProvjeritiPopunjenostPolja();
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            if (parentp._kojiJePritisnut == parentp.rate)
            {
                int IdAnkete = _ratingController.GetAll().Count + 1;
                string komentar = commentText.Text;
                Rating novaRating = new Rating(IdAnkete, komentar, (int)rateComboBox.SelectedItem, selektovan.Doctor.Username, selektovan.Patient.Username, selektovan.AppointmentID, false, DateTime.Now);
                _ratingController.Add(novaRating);
                //RatingPage ap = new RatingPage(parent);

                /*parentp.UpdateTable();
                parent.startWindow.Content = new RatingPage(parent);
                parent.iconAndName.Visibility = Visibility.Visible;
                parent.odjava.Visibility = Visibility.Visible;*/
                parent.iconAndNameDoctor.Visibility = Visibility.Hidden;
                StatisticsForDoctorPage statisticPage = new StatisticsForDoctorPage(parent, selektovan);
                parent.startWindow.Content = statisticPage;
            }
            else
            {
                int IdAnkete = _ratingController.GetAll().Count + 1;
                string komentar = commentText.Text;
                Rating novaRating = new Rating(IdAnkete, komentar, (int)rateComboBox.SelectedItem, null, parent.Patient.Username, 0, false, DateTime.Now);
                _ratingController.Add(novaRating);
                RatingPage ap = new RatingPage(parent);
                parentp.rateHospital.Visibility = Visibility.Hidden;
                parent.startWindow.Content = new RatingPage(parent);
                parent.iconAndName.Visibility = Visibility.Visible;
                parent.odjava.Visibility = Visibility.Visible;
                parent.iconAndNameDoctor.Visibility = Visibility.Hidden;

            }
        }

        private void updateVisibility()
        {
            parent.iconAndNameDoctor.Visibility = Visibility.Hidden;
            parent.titleLabel.Visibility = Visibility.Hidden;
            parent.titlePriorityLabel.Visibility = Visibility.Hidden;
            parent.odjava.Visibility = Visibility.Visible;
            parent.iconAndName.Visibility = Visibility.Visible;
        }
    }
}
