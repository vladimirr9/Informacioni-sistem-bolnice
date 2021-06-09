using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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

namespace InformacioniSistemBolnice.Patient_ns
{
    /// <summary>
    /// Interaction logic for PatientAllergensPage.xaml
    /// </summary>
    public partial class PatientAllergensPage : Page
    {
        private StartPatientWindow _parentp;
        private Patient _patient;

        public PatientAllergensPage(StartPatientWindow spw)
        {
            _parentp = spw;
            _patient = spw.Patient;
            InitializeComponent();
            GetItemsForListView();
        }

        private void GetItemsForListView()
        {
            foreach (Ingredient i in _patient.MedicalRecord.Allergens)
            {
                ListAllergens.Items.Add(i.Name);
            }
        }
    }
}
