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
        public string JMBG { get; set; }
        public NewPatientWindow(PatientsPage parent)
        {
            this._parent = parent;
            InitializeComponent();
            this.DataContext = this;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {

            string name = NameText.Text;
            string surname = SurnameText.Text;
            string jmbg = JMBGText.Text;
            char gender;
            if (GenderCombo.SelectedIndex == 0)
                gender = 'M';
            else
                gender = 'Ž';
            string phoneNumber = PhoneText.Text;
            string email = EmailText.Text;
            DateTime dateOfBirth;
            if (BirthDate.SelectedDate.Value != null)
                dateOfBirth = BirthDate.SelectedDate.Value;
            else
                dateOfBirth = new DateTime();
            string username = UsernameText.Text;
            string password = PasswordText.Text;
            bool isGuest = (bool)GuestCheckbox.IsChecked;
            AdresaStanovanja residentialAddress = new AdresaStanovanja(AddressText.Text, new MestoStanovanja(CityText.Text, PostalCodeText.Text, new DrzavaStanovanja(CountryText.Text)));
            string socialSecurityNumber = SocialSecurityText.Text;
            if (!IsUsernameUnique(username))
            {
                MessageBox.Show("Uneto korisničko ime već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }
            if (!IsJMBGUnique(JMBG))
            {
                MessageBox.Show("Uneti JMBG već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }
            Pacijent patient = new Pacijent(name, surname, jmbg, gender, phoneNumber, email, dateOfBirth, username, password, residentialAddress, isGuest, socialSecurityNumber, new ZdravstveniKarton(PacijentFileStorage.GetAll().Count.ToString()), false);
            patient.zdravstveniKarton.pacijent = patient;
            PacijentFileStorage.AddPacijent(patient);
            _parent.UpdateTable();
            this.Close();

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


    }
}
