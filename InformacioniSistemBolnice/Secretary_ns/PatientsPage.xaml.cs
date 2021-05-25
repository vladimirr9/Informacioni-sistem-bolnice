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
        private PatientsPage()
        {
            InitializeComponent();
            UpdateTable();
        }
        public static PatientsPage GetPage()
        {
            if (_instance == null)
                _instance = new PatientsPage();
            else
                _instance.UpdateTable();
            return _instance;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NewPatientWindow window = new NewPatientWindow(this);
            window.Show();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (PatientsDataGrid.SelectedItem == null)
                return;
            
            Pacijent initialPatient = PacijentFileStorage.GetOne(((Pacijent)(PatientsDataGrid.SelectedItem)).korisnickoIme);
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
            
            PacijentFileStorage.RemovePacijent(((Pacijent)(PatientsDataGrid.SelectedItem)).korisnickoIme);
            UpdateTable();
            
            
        }

        private void Unban_Click(object sender, RoutedEventArgs e)
        {
            if (PatientsDataGrid.SelectedItem == null)
                return;
            Pacijent patient = (Pacijent)(PatientsDataGrid.SelectedItem);
            if (!patient.Banovan)
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

            DetailedWindow window = new DetailedWindow(this, ((Pacijent)(PatientsDataGrid.SelectedItem)).korisnickoIme);
            window.Show();

        }
        public void UpdateTable()
        {
            PatientsDataGrid.Items.Clear();
            List<Pacijent> patients = PacijentFileStorage.GetAll();
            foreach (Pacijent patient in patients)
            {
                if (!patient.isDeleted)
                    PatientsDataGrid.Items.Add(patient);
            }
        }

        

        
    }
}
