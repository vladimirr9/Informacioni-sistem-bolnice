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

namespace InformacioniSistemBolnice.Secretary_ns
{
    public partial class NewPatientWindow : Window
    {
        private PatientsPage _parent;

        public string Username { get; set; }
        public string Password { get; set; }
        public string JMBG { get; set; }
        public bool Guest { get; set; }
        public string Name { get; set; }
        public char Gender { get; set; }
        public string Surname { get; set; }
        public string TelephoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string ResidentialAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SocialSecurityNumber { get; set; }
        public NewPatientWindow(PatientsPage parent)
        {
            this._parent = parent;
            InitializeComponent();
            this.DataContext = this;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {

            AdresaStanovanja residentialAddress = new AdresaStanovanja(ResidentialAddress, new MestoStanovanja(City, PostalCode, new DrzavaStanovanja(Country)));
            if (!IsUsernameUnique(Username))
            {
                MessageBox.Show("Uneto korisničko ime već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }
            if (!IsJMBGUnique(JMBG))
            {
                MessageBox.Show("Uneti JMBG već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }
            Pacijent patient = new Pacijent(Name, Surname, JMBG, Gender, TelephoneNumber, EmailAddress, DateOfBirth, Username, Password, residentialAddress, Guest, SocialSecurityNumber, new ZdravstveniKarton(PacijentFileStorage.GetAll().Count.ToString()), false);
            patient.zdravstveniKarton.pacijent = patient;
            PacijentFileStorage.AddPacijent(patient);
            _parent.UpdateTable();
            this.Close();

        }

        private void SetConfirmIsEnabled()
        {
            /*
            ConfirmButton.IsEnabled = Username.Length != 0 && Password.Length != 0 && JMBG.Length != 0 &&
                                      Name.Length != 00 && Surname.Length != 0 && TelephoneNumber.Length != 0 &&
                                      EmailAddress.Length != 0 && ResidentialAddress.Length != 0 &&
                                      PostalCode.Length != 0 && City.Length != 0 && Country.Length != 0 &&
                                      !DateOfBirth.Equals(null) && SocialSecurityNumber.Length != 0;
                                      */

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public bool IsUsernameUnique(String username)
        {
            if (PacijentFileStorage.GetOne(username) == null)
                return true;
            return false;
        }

        public bool IsJMBGUnique(String jmbg)
        {
            if (PacijentFileStorage.GetOneByJMBG(jmbg) == null)
                return true;
            return false;
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
    }
}
