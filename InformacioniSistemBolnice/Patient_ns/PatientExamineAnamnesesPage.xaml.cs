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
    /// Interaction logic for PatientExamineAnamnesesPage.xaml
    /// </summary>
    public partial class PatientExamineAnamnesesPage : Page
    {
        private  StartPatientWindow parent;
        private AnamnesisController _anamnesisController = new AnamnesisController();
        private NotesForAnamnesisController _notesForAnamnesisController = new NotesForAnamnesisController();
        public  PatientMedicalRecordPage karton { get; set; }
        private  Patient _loggedInPatient;
        public  Anamnesis selectedAnamnesis { get; set; }
        public PatientExamineAnamnesesPage(StartPatientWindow pp, PatientMedicalRecordPage pkp)
        {
            parent = pp;
            _loggedInPatient = pp.Patient;
            karton = pkp;
            InitializeComponent();
            this.DataContext = this;
            addNoteButton.IsEnabled = false;
            FillTable();
        }

        private void FillTable()
        {
            foreach (Anamnesis a in _notesForAnamnesisController.GetPatientsAnamneses(_loggedInPatient))
            {
                DataGridAnamneses.Items.Add(a);
            }
        }

        private void addNoteButton_Click(object sender, RoutedEventArgs e)
        {
            selectedAnamnesis = (Anamnesis)DataGridAnamneses.SelectedItem;
            karton.borderWindow.Content = new AddingNotesPage(parent, karton, selectedAnamnesis);
        }

        private void ShowNotesForAnamnesis(object sender, RoutedEventArgs e)
        {
            karton.borderWindow.Content = new ShowNotePage(karton, selectedAnamnesis);
        }

        private void DataGridAnamneses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedAnamnesis = (Anamnesis)DataGridAnamneses.SelectedItem;
            addNoteButton.IsEnabled = true;
        }
    }
}
