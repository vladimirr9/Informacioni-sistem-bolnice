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
    public partial class EditPatientWindow : Window
    {
        private Pacijent _initialPatient;
        private PatientsPage _parent;
        public string JMBG { get; set; }
        public EditPatientWindow(Pacijent patient, PatientsPage parent)
        {
            _initialPatient = patient;
            InitializeComponent();
            this.DataContext = this;

            NameText.Text = _initialPatient.ime;
            SurnameText.Text = _initialPatient.prezime;
            JMBG = _initialPatient.jmbg;
            if (_initialPatient.pol == 'M')
                GenderCombo.SelectedIndex = 0;
            else
                GenderCombo.SelectedIndex = 1;
            PhoneText.Text = _initialPatient.brojTelefona;
            EmailText.Text = _initialPatient.email;
            BirthDate.SelectedDate = _initialPatient.datumRodenja;
            UsernameText.Text = _initialPatient.korisnickoIme;
            PasswordText.Text = _initialPatient.lozinka;
            CountryText.Text = _initialPatient.adresaStanovanja.mestoStanovanja.drzavaStanovanja.naziv;
            PostalCodeText.Text = _initialPatient.adresaStanovanja.mestoStanovanja.postanskiBroj;
            CityText.Text = _initialPatient.adresaStanovanja.mestoStanovanja.naziv;
            AddressText.Text = _initialPatient.adresaStanovanja.ulicaIBroj;
            SocialSecurityText.Text = _initialPatient.brojZdravstveneKartice;
            GuestCheckbox.IsChecked = patient.isGuest;
            this._parent = parent;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameText.Text;
            string surname = SurnameText.Text;
            char gender;
            if (GenderCombo.SelectedIndex == 0)
                gender = 'M';
            else
                gender = 'Ž';
            string phoneNumber = PhoneText.Text;
            string email = EmailText.Text;
            DateTime dateOfBirth = BirthDate.SelectedDate.Value;
            string username = UsernameText.Text;
            string password = PasswordText.Text;
            AdresaStanovanja residentialAddress = new AdresaStanovanja(AddressText.Text, new MestoStanovanja(CityText.Text, PostalCodeText.Text, new DrzavaStanovanja(CountryText.Text)));
            bool isGuest = (bool)GuestCheckbox.IsChecked;
            string socialSecurityNumber = SocialSecurityText.Text;
            if (!(IsUsernameUnique(username) || username.Equals(_initialPatient.korisnickoIme)))
            {
                MessageBox.Show("Uneto korisničko ime već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }
            if (!(IsJMBGUnique(JMBG) || JMBG.Equals(_initialPatient.jmbg)))
            {
                MessageBox.Show("Uneti JMBG već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }
            Pacijent patient = new Pacijent(name, surname, JMBG, gender, phoneNumber, email, dateOfBirth, username, password, residentialAddress, isGuest, socialSecurityNumber, new ZdravstveniKarton(PacijentFileStorage.GetAll().Count.ToString()), false);
            patient.zdravstveniKarton.pacijent = patient;
            if (!_initialPatient.korisnickoIme.Equals(username))
                UpdateAppointmentsForUsernameChange(username);
            PacijentFileStorage.UpdatePacijent(_initialPatient.korisnickoIme, patient);
            _parent.UpdateTable();
            Close();



        }

        private void UpdateAppointmentsForUsernameChange(string username)
        {
            foreach (Termin appointment in TerminFileStorage.GetAll())
            {
                if (appointment.KorisnickoImePacijenta.Equals(_initialPatient.korisnickoIme))
                {
                    appointment.KorisnickoImePacijenta = username;
                    TerminFileStorage.UpdateTermin(appointment.iDTermina, appointment);
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
    }

}
