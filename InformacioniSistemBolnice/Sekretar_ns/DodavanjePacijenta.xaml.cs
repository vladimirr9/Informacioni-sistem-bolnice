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

namespace InformacioniSistemBolnice.Sekretar_ns
{
    /// <summary>
    /// Interaction logic for DodavanjePacijenta.xaml
    /// </summary>
    public partial class DodavanjePacijenta : Window
    {
        private PacijentiPage parent;
        public string JMBG { get; set; }
        public DodavanjePacijenta(PacijentiPage parent)
        {
            this.parent = parent;
            InitializeComponent();
            this.DataContext = this;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {

            string ime = NameText.Text;
            string prezime = SurnameText.Text;
            string jmbg = JMBGText.Text;
            char pol;
            if (GenderCombo.SelectedIndex == 0)
                pol = 'M';
            else
                pol = 'Ž';
            string brojTelefona = PhoneText.Text;
            string email = EmailText.Text;
            DateTime datumRodjenja;
            if (BirthDate.SelectedDate.Value != null)
                datumRodjenja = BirthDate.SelectedDate.Value;
            else
                datumRodjenja = new DateTime();
            string korisnickoIme = UsernameText.Text;
            string lozinka = PasswordText.Text;
            bool IsGuest = (bool)GuestCheckbox.IsChecked;
            AdresaStanovanja adresaStanovanja = new AdresaStanovanja(AddressText.Text, new MestoStanovanja(CityText.Text, PostalCodeText.Text, new DrzavaStanovanja(CountryText.Text)));
            string brojZdravstveneKartice = SocialSecurityText.Text;
            if (!IsUsernameUnique(korisnickoIme))
            {
                MessageBox.Show("Uneto korisničko ime već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }
            if (!IsJMBGUnique(JMBG))
            {
                MessageBox.Show("Uneti JMBG već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }
            Pacijent p = new Pacijent(ime, prezime, jmbg, pol, brojTelefona, email, datumRodjenja, korisnickoIme, lozinka, adresaStanovanja, IsGuest, brojZdravstveneKartice, new ZdravstveniKarton(PacijentFileStorage.GetAll().Count.ToString()), false);
            p.zdravstveniKarton.pacijent = p;
            PacijentFileStorage.AddPacijent(p);
            parent.updateTable();
            this.Close();

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public bool IsUsernameUnique(String korisnickoIme)
        {
            if (PacijentFileStorage.GetOne(korisnickoIme) == null)
                return true;
            else return false;
        }

        public bool IsJMBGUnique(String jmbg)
        {
            if (PacijentFileStorage.GetOneByJMBG(jmbg) == null)
                return true;
            else return false;
        }


    }
}
