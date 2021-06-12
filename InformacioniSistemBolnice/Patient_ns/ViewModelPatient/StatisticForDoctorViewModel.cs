using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.View.ViewModel;
using LiveCharts;
using LiveCharts.Wpf;

namespace InformacioniSistemBolnice.Patient_ns.ViewModelPatient
{
    class StatisticForDoctorViewModel : BindableBase
    {
        private StartPatientWindow _parentp { get; set; }
        private Doctor _ratedDoctor;
        private RatingController _ratingController = new RatingController();
        private String _doctorsName;
        public MyICommand OK_Command { get; set; }

        public string DoctorsName
        {
            get { return _doctorsName; }
            set
            {
                _doctorsName = value;
                OnPropertyChanged("Patient");
            }
        }

        public StatisticForDoctorViewModel(StartPatientWindow spw, Appointment _selected)
        {
            _parentp = spw;
            _ratedDoctor = _selected.Doctor;
            OK_Command = new MyICommand(OK_Click);
            _parentp.ratingSuccessful.Visibility = Visibility.Visible;
            _parentp.titleLabel.Visibility = Visibility.Visible;
            DoctorsName = "Sveukupne ocene za lekara po imenu dr. " + _ratedDoctor.Name + " " +
                                  _ratedDoctor.Surname;
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title="Broj ocena",
                    Values = new ChartValues<double> { _ratingController.GetNumberOfRating(1,_ratedDoctor), _ratingController.GetNumberOfRating(2, _ratedDoctor) , _ratingController.GetNumberOfRating(3, _ratedDoctor) , _ratingController.GetNumberOfRating(4, _ratedDoctor) , _ratingController.GetNumberOfRating(5, _ratedDoctor) }
                }

            };

            Labels = new[] { "Ocena 1", "Ocena2", "Ocena 3", "Ocena 4", "Ocena 5" };
            YFormatter = value => value.ToString("N");

            
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private void OK_Click()
        {

            _parentp.startWindow.Content = new RatingPage(_parentp);
            //DoctorsName.Visibility = Visibility.Hidden;
            _parentp.titleLabel.Visibility = Visibility.Hidden;
            _parentp.ratingSuccessful.Visibility = Visibility.Hidden;
            _parentp.iconAndName.Visibility = Visibility.Visible;
            _parentp.odjava.Visibility = Visibility.Visible;
        }


    }
    
}
