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
    /// Interaction logic for ShowNotePage.xaml
    /// </summary>
    public partial class ShowNotePage : Page
    {
        private PatientMedicalRecordPage karton;
        private Anamnesis _selectedAnamnesis;
        private AnamnesisController _anamnesisController = new AnamnesisController();
        private Patient _loggedInPatient;
        public ShowNotePage(PatientMedicalRecordPage pkp, Anamnesis selected)
        {
            _selectedAnamnesis = selected;
            _loggedInPatient = pkp.Patient;
            Debug.WriteLine(selected.IdOfAppointment.ToString());
            karton = pkp;
            InitializeComponent();
            this.DataContext = this;
            UpdateListView();
        }

        private void UpdateListView()
        {
            foreach (Note n in _anamnesisController.NotesForAnamnesis(_selectedAnamnesis, _loggedInPatient))
            {
                showNotesListView.Items.Add(n.DescriptionOfNote);
            }
        }
    }
}
