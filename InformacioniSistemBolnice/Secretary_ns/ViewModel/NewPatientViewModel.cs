using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.View.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InformacioniSistemBolnice.Secretary_ns.ViewModel
{
    class NewPatientViewModel : BindableBase
    {
        private string _username;
        private string _password;
        private string _JMBG;
        private bool _guest;
        private string _legalName;
        private string _gender;
        private string _surname;
        private string _telephoneNumber;
        private string _emailAddress;
        private string _residentialAddress;
        private string _postalCode;
        private string _city;
        private string _country;
        private DateTime _dateOfBirth;
        private string _socialSecurityNumber;
        private PatientController _patientController = new PatientController();
        private NewPatientWindow _parent;

        public MyICommand ConfirmCommand { get; set; }
        public MyICommand CancelCommand { get; set; }



        public NewPatientViewModel(NewPatientWindow parent)
        {
            _parent = parent;
            ConfirmCommand = new MyICommand(Confirm_Click);
            CancelCommand = new MyICommand(Cancel_Click);

        }
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
        public string JMBG
        {
            get { return _JMBG; }
            set
            {
                _JMBG = value;
                OnPropertyChanged("JMBG");
            }
        }
        public bool Guest
        {
            get { return _guest; }
            set
            {
                _guest = value;
                OnPropertyChanged("Guest");
            }
        }
        public string LegalName
        {
            get { return _legalName; }
            set
            {
                _legalName = value;
                OnPropertyChanged("LegalName");
            }
        }
        public string Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged("Gender");
            }
        }
        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged("Surname");
            }
        }
        public string TelephoneNumber
        {
            get { return _telephoneNumber; }
            set
            {
                _telephoneNumber = value;
                OnPropertyChanged("TelephoneNumber");
            }
        }
        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                _emailAddress = value;
                OnPropertyChanged("EmailAddress");
            }
        }
        public string ResidentialAddress
        {
            get { return _residentialAddress; }
            set
            {
                _residentialAddress = value;
                OnPropertyChanged("ResidentialAddress");
            }
        }
        public string PostalCode
        {
            get { return _postalCode; }
            set
            {
                _postalCode = value;
                OnPropertyChanged("PostalCode");
            }
        }
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged("City");
            }
        }
        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                OnPropertyChanged("Country");
            }
        }
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }
        public string SocialSecurityNumber
        {
            get { return _socialSecurityNumber; }
            set
            {
                _socialSecurityNumber = value;
                OnPropertyChanged("SocialSecurityNumber");
            }
        }

        private void Confirm_Click()
        {
            UInt64 JMBGRes;
            char genderRes;
            
            if (!char.TryParse(Gender, out genderRes))
            {
                MessageBox.Show("Morate uneti pol", "Nedostatak informacija", MessageBoxButton.OK);
                return;
            }
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
            patient.MedicalRecord.patient = patient;
            bool retVal = _patientController.Register(patient);


            if (retVal)
            {
                MessageBox.Show("Pacijent je uspešno registrovan!", "Uspešno dodat korisnik", MessageBoxButton.OK);
                _parent.Close();
            }

        }

        private void Cancel_Click()
        {
            _parent.Close();
        }

    }
}
