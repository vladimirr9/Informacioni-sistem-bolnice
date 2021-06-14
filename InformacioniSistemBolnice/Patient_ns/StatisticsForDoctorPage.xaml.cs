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
using LiveCharts;
using LiveCharts.Wpf;

namespace InformacioniSistemBolnice.Patient_ns
{
    /// <summary>
    /// Interaction logic for StatisticsForDoctorPage.xaml
    /// </summary>
    public partial class StatisticsForDoctorPage : Page
    {

        private StartPatientWindow _parentp { get; set; }
        private Doctor _ratedDoctor;
        private RatingController _ratingController = new RatingController();

        private RatingsWrittenByPatientController _ratingsWrittenByPatientController = new RatingsWrittenByPatientController();
        private Appointment _selectedAppointment;


        public StatisticsForDoctorPage(StartPatientWindow spw, Appointment _selected)
        {
            _parentp = spw;
            _ratedDoctor = _selected.Doctor;
            InitializeComponent();
            _parentp.ratingSuccessful.Visibility = Visibility.Visible;
            _parentp.titleLabel.Visibility = Visibility.Visible;
            doctorsName.Content = "Sveukupne ocene za lekara po imenu dr. " + _ratedDoctor.Name + " " +
                                  _ratedDoctor.Surname;

            /* Func<ChartPoint, string> labelPoint = chartPoint =>
                 string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            Jedan.Values= new ChartValues<double> { _ratingController.GetNumberOfRating(1, _ratedDoctor)} ;
            Dva.Values = new ChartValues<double> { _ratingController.GetNumberOfRating(2, _ratedDoctor) };
            Tri.Values = new ChartValues<double> { _ratingController.GetNumberOfRating(3, _ratedDoctor) };
            Cetiri.Values = new ChartValues<double> { _ratingController.GetNumberOfRating(4, _ratedDoctor) };
            Pet.Values = new ChartValues<double> { _ratingController.GetNumberOfRating(5, _ratedDoctor) };*/

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title="Broj ocena",
                    Values = new ChartValues<double> { _ratingsWrittenByPatientController.GetNumberOfRating(1,_ratedDoctor), _ratingsWrittenByPatientController.GetNumberOfRating(2, _ratedDoctor) , _ratingsWrittenByPatientController.GetNumberOfRating(3, _ratedDoctor) , _ratingsWrittenByPatientController.GetNumberOfRating(4, _ratedDoctor) , _ratingsWrittenByPatientController.GetNumberOfRating(5, _ratedDoctor) }
                }

            };

            Labels = new[] { "Ocena 1", "Ocena2", "Ocena 3", "Ocena 4", "Ocena 5" };
            YFormatter = value => value.ToString("N");

            //modifying the series collection will animate and update the chart

            //modifying any series values will also animate and update the chart


            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private void oK_Click(object sender, RoutedEventArgs e)
        {

            _parentp.startWindow.Content = new RatingPage(_parentp);
            doctorsName.Visibility = Visibility.Hidden;
            _parentp.titleLabel.Visibility = Visibility.Hidden;
            _parentp.ratingSuccessful.Visibility = Visibility.Hidden;
            _parentp.iconAndName.Visibility = Visibility.Visible;
            _parentp.odjava.Visibility = Visibility.Visible;
        }
    }

}

