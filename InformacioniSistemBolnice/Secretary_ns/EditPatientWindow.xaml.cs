﻿using System;
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
        public EditPatientWindow(Pacijent patient, PatientsPage parent)
        {
            _initialPatient = patient;
            InitializeComponent();
            this.DataContext = this;

            //NameText.Text = _initialPatient.ime;
            LegalName = _initialPatient.ime;
            Surname = _initialPatient.prezime;
            JMBG = _initialPatient.jmbg;
            Gender = _initialPatient.pol.ToString();
            TelephoneNumber = _initialPatient.brojTelefona;
            EmailAddress = _initialPatient.email;
            DateOfBirth = _initialPatient.datumRodenja;
            Username = _initialPatient.korisnickoIme;
            Password = _initialPatient.lozinka;
            Country = _initialPatient.adresaStanovanja.mestoStanovanja.drzavaStanovanja.naziv;
            PostalCode = _initialPatient.adresaStanovanja.mestoStanovanja.postanskiBroj;
            City = _initialPatient.adresaStanovanja.mestoStanovanja.naziv;
            ResidentialAddress = _initialPatient.adresaStanovanja.ulicaIBroj;
            SocialSecurityNumber = _initialPatient.brojZdravstveneKartice;
            Guest = patient.isGuest;
            this._parent = parent;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
           
            AdresaStanovanja residentialAddress = new AdresaStanovanja(ResidentialAddress, new MestoStanovanja(City, PostalCode, new DrzavaStanovanja(Country)));
            if (!(IsUsernameUnique(Username) || Username.Equals(_initialPatient.korisnickoIme)))
            {
                MessageBox.Show("Uneto korisničko ime već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }
            if (!(IsJMBGUnique(JMBG) || JMBG.Equals(_initialPatient.jmbg)))
            {
                MessageBox.Show("Uneti JMBG već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }
            Pacijent patient = new Pacijent(LegalName, Surname, JMBG, char.Parse(Gender), TelephoneNumber, EmailAddress, DateOfBirth, Username, Password, residentialAddress, Guest, SocialSecurityNumber, new ZdravstveniKarton(PacijentFileStorage.GetAll().Count.ToString()), false);
            patient.zdravstveniKarton.pacijent = patient;
            if (!_initialPatient.korisnickoIme.Equals(Username))
                UpdateAppointmentsForUsernameChange(Username);
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

        private void Cancel_Click(object sender, RoutedEventArgs e)
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
    }

}