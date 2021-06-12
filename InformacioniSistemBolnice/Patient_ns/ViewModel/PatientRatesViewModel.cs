using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.View.ViewModel;

namespace InformacioniSistemBolnice.Patient_ns.ViewModel
{
    class PatientRatesViewModel : BindableBase
    {

        private ObservableCollection<int> _ratingComboBox;
        private int _selectedRatingCombo;
        private StartPatientWindow parent;
        private Appointment selektovan;
        private RatingPage parentp;

        public PatientRatesViewModel(RatingPage ap, StartPatientWindow pp, Appointment t) {
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
    }
}
