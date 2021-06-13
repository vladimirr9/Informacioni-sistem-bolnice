using InformacioniSistemBolnice.Controller;
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

namespace InformacioniSistemBolnice.Secretary_ns
{
    public partial class PatientsPage : Page
    {
        private PatientController _patientController = new PatientController();
        private static PatientsPage _instance;
        public String Filter { get; set; }
        private PatientsPage()
        {
            InitializeComponent();
            this.DataContext = this;
            UpdateTable();
        }
        public static PatientsPage GetPage(SecretaryMain parent)
        {
            if (_instance == null)
                _instance = new PatientsPage();
            else
                _instance.UpdateTable();
            parent.Title.Content = "Pacijenti";
            return _instance;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NewPatientWindow window = new NewPatientWindow(this);
            window.ShowDialog();
            UpdateTable();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (PatientsDataGrid.SelectedItem == null)
                return;
            
            Patient initialPatient = _patientController.GetOne(((Patient)(PatientsDataGrid.SelectedItem)).Username);
            EditPatientWindow window = new EditPatientWindow(initialPatient, this);
            window.Show();
            
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            if (PatientsDataGrid.SelectedItem == null)
                return;
            
            var result = MessageBox.Show("Da li ste sigurni da želite da obrišete ovog pacijenta?", "Potvrda brisanja", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;

            Patient patient = (Patient) PatientsDataGrid.SelectedItem;
            _patientController.Remove(patient);
            UpdateTable();
            
            
        }

        private void Unban_Click(object sender, RoutedEventArgs e)
        {
            if (PatientsDataGrid.SelectedItem == null)
                return;
            Patient patient = (Patient)(PatientsDataGrid.SelectedItem);
            if (!patient.Banned)
                return;
            var result = MessageBox.Show("Da li ste sigurni da želite da odblokirate ovog pacijenta?", "Potvrda odblokiranja", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;
            _patientController.Unban(patient);
            UpdateTable();

        }
        private void Detailed_Click(object sender, RoutedEventArgs e)
        {
            if (PatientsDataGrid.SelectedItem == null)
                return;

            DetailedWindow window = new DetailedWindow(this, ((Patient)(PatientsDataGrid.SelectedItem)).Username);
            window.Show();

        }
        public void UpdateTable()
        {
            PatientsDataGrid.Items.Clear();
            List<Patient> patients = _patientController.GetAll();
            foreach (Patient patient in patients)
            {
                if (!patient.IsDeleted)
                {
                    if (Filter == null)
                        PatientsDataGrid.Items.Add(patient);
                    else
                    {
                        if (patient.Name.ToUpper().StartsWith(TableFilter.Text.ToUpper()))
                        {
                            PatientsDataGrid.Items.Add(patient);
                        }
                    }
                }
            }
        }

        private void TableFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTable();
        }
    }
}
