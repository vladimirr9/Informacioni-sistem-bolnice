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
    /// Interaction logic for IzmenaPacijenta.xaml
    /// </summary>
    public partial class IzmenaPacijenta : Window
    {
        private Pacijent inicijalniPacijent;
        private PacijentiPage parent;
        public string JMBG { get; set; }
        public IzmenaPacijenta(Pacijent p, PacijentiPage parent)
        {
            inicijalniPacijent = p;
            InitializeComponent();
            this.DataContext = this;

            NameText.Text = inicijalniPacijent.ime;
            SurnameText.Text = inicijalniPacijent.prezime;
            JMBG = inicijalniPacijent.jmbg;
            if (inicijalniPacijent.pol == 'M')
                GenderCombo.SelectedIndex = 0;
            else
                GenderCombo.SelectedIndex = 1;
            PhoneText.Text = inicijalniPacijent.brojTelefona;
            EmailText.Text = inicijalniPacijent.email;
            BirthDate.SelectedDate = inicijalniPacijent.datumRodenja;
            UsernameText.Text = inicijalniPacijent.korisnickoIme;
            PasswordText.Text = inicijalniPacijent.lozinka;
            CountryText.Text = inicijalniPacijent.adresaStanovanja.mestoStanovanja.drzavaStanovanja.naziv;
            PostalCodeText.Text = inicijalniPacijent.adresaStanovanja.mestoStanovanja.postanskiBroj;
            CityText.Text = inicijalniPacijent.adresaStanovanja.mestoStanovanja.naziv;
            AddressText.Text = inicijalniPacijent.adresaStanovanja.ulicaIBroj;
            SocialSecurityText.Text = inicijalniPacijent.brojZdravstveneKartice;
            GuestCheckbox.IsChecked = p.isGuest;
            this.parent = parent;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string ime = NameText.Text;
            string prezime = SurnameText.Text;
            char pol;
            if (GenderCombo.SelectedIndex == 0)
                pol = 'M';
            else
                pol = 'Ž';
            string brojTelefona = PhoneText.Text;
            string email = EmailText.Text;
            DateTime datumRodjenja = BirthDate.SelectedDate.Value;
            string korisnickoIme = UsernameText.Text;
            string lozinka = PasswordText.Text;
            AdresaStanovanja adresaStanovanja = new AdresaStanovanja(AddressText.Text, new MestoStanovanja(CityText.Text, PostalCodeText.Text, new DrzavaStanovanja(CountryText.Text)));
            bool isGuest = (bool)GuestCheckbox.IsChecked;
            string brojZdravstveneKartice = SocialSecurityText.Text;
            if (!(IsUsernameUnique(korisnickoIme) || korisnickoIme.Equals(inicijalniPacijent.korisnickoIme)))
            {
                MessageBox.Show("Uneto korisničko ime već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }
            if (!(IsJMBGUnique(JMBG) || JMBG.Equals(inicijalniPacijent.jmbg)))
            {
                MessageBox.Show("Uneti JMBG već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }
            Pacijent p = new Pacijent(ime, prezime, JMBG, pol, brojTelefona, email, datumRodjenja, korisnickoIme, lozinka, adresaStanovanja, isGuest, brojZdravstveneKartice, new ZdravstveniKarton(PacijentFileStorage.GetAll().Count.ToString()), false);
            p.zdravstveniKarton.pacijent = p;
            PacijentFileStorage.UpdatePacijent(inicijalniPacijent.korisnickoIme, p);
            parent.updateTable();
            Close();
            
                

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
