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
using System.Windows.Shapes;
using InformacioniSistemBolnice.Controller;

namespace InformacioniSistemBolnice.Secretary_ns
{
    public partial class EditPatientWindow : Window
    {
        private Patient _initialPatient;
        private PatientsPage _parent;
        private PatientController _patientController = new PatientController();

        public string Username { get; set; }
        public string Password { get; set; }
        public string JMBG { get; set; }
        public bool Guest { get; set; }
        public string LegalName { get; set; }
        public string Gender { get; set; }
        public string Surname { get; set; }
        public string TelephoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string ResidentialAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SocialSecurityNumber { get; set; }
        public EditPatientWindow(Patient patient, PatientsPage parent)
        {
            this._parent = parent;
            _initialPatient = patient;
            InitializeComponent();
            this.DataContext = this;

            InitializeStartingValuesForComponents(patient);

            
        }

        

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            UInt64 JMBGRes;
            char genderRes;

            if (!UInt64.TryParse(JMBG, out JMBGRes) || JMBG.Length != 13)
            {
                MessageBox.Show("JMBG se mora sastojati iz 13 cifara", "Nedostatak informacija", MessageBoxButton.OK);
                return;
            }
            if (Username == null || Username.Length == 0)
            {
                MessageBox.Show("Korisničko ime ne sme biti prazno", "Nevalidan unos", MessageBoxButton.OK);
                return;
            }
            ResidentialAddress residentialAddress = new ResidentialAddress(ResidentialAddress, new City(City, PostalCode, new Country(Country)));
            Patient patient = new Patient(LegalName, Surname, JMBG, char.Parse(Gender), TelephoneNumber, EmailAddress, DateOfBirth, Username, Password, residentialAddress, Guest, SocialSecurityNumber, new MedicalRecord(_patientController.GetAll().Count.ToString()), false);
            patient.MedicalRecord = _initialPatient.MedicalRecord;
            _initialPatient.MedicalRecord.patient = patient;
            _patientController.Update(_initialPatient.Username, patient);
            _parent.UpdateTable();
            this.Close();

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SetConfirmIsEnabled()
        {
            /*
            ConfirmButton.IsEnabled = Username.Length != 0 && Password.Length != 0 && JMBG.Length != 0 &&
                                      LegalName.Length != 00 && Surname.Length != 0 && TelephoneNumber.Length != 0 &&
                                      EmailAddress.Length != 0 && ResidentialAddress.Length != 0 &&
                                      PostalCode.Length != 0 && City.Length != 0 && Country.Length != 0 &&
                                      !DateOfBirth.Equals(null) && SocialSecurityNumber.Length != 0;
                                      */

        }
        private void UsernameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetConfirmIsEnabled();
        }

        private void PasswordText_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetConfirmIsEnabled();
        }

        private void JMBGText_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetConfirmIsEnabled();
        }

        private void NameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetConfirmIsEnabled();
        }

        private void SurnameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetConfirmIsEnabled();
        }

        private void PhoneText_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetConfirmIsEnabled();
        }

        private void EmailText_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetConfirmIsEnabled();
        }

        private void AddressText_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetConfirmIsEnabled();
        }

        private void PostalCodeText_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetConfirmIsEnabled();
        }

        private void CityText_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetConfirmIsEnabled();
        }

        private void CountryText_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetConfirmIsEnabled();
        }

        private void SocialSecurityText_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetConfirmIsEnabled();
        }

        private void BirthDate_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SetConfirmIsEnabled();
        }
        private void InitializeStartingValuesForComponents(Patient patient)
        {
            LegalName = _initialPatient.Name;
            Surname = _initialPatient.Surname;
            JMBG = _initialPatient.JMBG;
            Gender = _initialPatient.Gender.ToString();
            TelephoneNumber = _initialPatient.PhoneNumber;
            EmailAddress = _initialPatient.Email;
            DateOfBirth = _initialPatient.DateOfBirth;
            Username = _initialPatient.Username;
            Password = _initialPatient.Password;
            Country = _initialPatient.ResidentialAddress.City.Country.Name;
            PostalCode = _initialPatient.ResidentialAddress.City.PostalCode;
            City = _initialPatient.ResidentialAddress.City.Name;
            ResidentialAddress = _initialPatient.ResidentialAddress.StreetAndNumber;
            SocialSecurityNumber = _initialPatient.SocialSecurityNumber;
            Guest = patient.IsGuest;
        }
    }

}
