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
            string username = "Guest" + PatientFileRepository.GetAll().Count.ToString();

            if (IsJMBGUnique(JMBG))
            {
                Patient = new Patient(name, surname, JMBG, ' ', "", "", new DateTime(), username, "", new ResidentialAddress("", new City("", "", new Country(""))), true, "", new MedicalRecord(""));
                PatientFileRepository.AddPatient(Patient);
                _parent.InitializePatients();
                _parent.PatientsList.SelectedItem = Patient.Name + " " + Patient.Surname + " - " + Patient.JMBG;
                Close();
            }
            else
                MessageBox.Show("JMBG koji ste pokušali da unesete se već nalazi u sistemu.", "JMBG postoji u sistemu", MessageBoxButton.OK);
        }

        private static bool IsJMBGUnique(string JMBG)
        {
            foreach (Patient patient in PatientFileRepository.GetAll())
            {
                if (patient.JMBG.Equals(JMBG))
                    return false;
            }

            return true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
