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
using InformacioniSistemBolnice.Patient_ns.ViewModelPatient;

namespace InformacioniSistemBolnice.Patient_ns
{
    /// <summary>
    /// Interaction logic for ShowNotePage.xaml
    /// </summary>
    public partial class ShowNotePage : Page
    {

        public ShowNotePage(PatientMedicalRecordPage pkp, Anamnesis selected)
        {

            InitializeComponent();
            DataContext = new ShowNoteViewModel(pkp, selected);


        }
    }
}
