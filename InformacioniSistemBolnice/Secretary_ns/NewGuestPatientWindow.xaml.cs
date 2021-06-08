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
    /// <summary>
    /// Interaction logic for NewGuestPatientWindow.xaml
    /// </summary>
    public partial class NewGuestPatientWindow : Window
    {
        public string JMBG { get; set; }
        private NewUrgentAppointment _parent;
        public Patient Patient;
        private PatientController _patientController = new PatientController();
        public NewGuestPatientWindow(NewUrgentAppointment parent)
        {
            this._parent = parent;
            InitializeComponent();
            DataContext = this;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string name = NameText.Text;
            string surname = SurnameText.Text;
            string username = "Guest" + _patientController.GetAll().Count.ToString();
            if (!Int64.TryParse(JMBG, out var JMBGRes) || JMBG.Length != 13)
            {
                MessageBox.Show("JMBG se mora sastojati iz 13 cifara", "Nedostatak informacija", MessageBoxButton.OK);
                return;
            }


            Patient = new Patient(name, surname, JMBG, ' ', "", "", new DateTime(), username, "", new ResidentialAddress("", new City("", "", new Country(""))), true, "", new MedicalRecord(""));
            bool retVal = _patientController.Register(Patient);
            if (retVal)
            {
                _parent.InitializePatients();
                _parent.PatientsList.SelectedItem = Patient.Name + " " + Patient.Surname + " - " + Patient.JMBG;
                Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
