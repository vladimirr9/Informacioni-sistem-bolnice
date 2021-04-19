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
        public IzmenaPacijenta(Pacijent p, PacijentiPage parent)
        {
            inicijalniPacijent = p;
            InitializeComponent();

            Ime.Text = inicijalniPacijent.ime;
            Prezime.Text = inicijalniPacijent.prezime;
            JMBG.Text = inicijalniPacijent.jmbg;
            if (inicijalniPacijent.pol == 'M')
                Pol.SelectedIndex = 0;
            else
                Pol.SelectedIndex = 1;
            BrojTelefona.Text = inicijalniPacijent.brojTelefona;
            Email.Text = inicijalniPacijent.email;
            DatePicker.SelectedDate = inicijalniPacijent.datumRodenja;
            KorisnickoIme.Text = inicijalniPacijent.korisnickoIme;
            Lozinka.Text = inicijalniPacijent.lozinka;
            Drzava.Text = inicijalniPacijent.adresaStanovanja.mestoStanovanja.drzavaStanovanja.naziv;
            PostanskiBroj.Text = inicijalniPacijent.adresaStanovanja.mestoStanovanja.postanskiBroj;
            Mesto.Text = inicijalniPacijent.adresaStanovanja.mestoStanovanja.naziv;
            UlicaIBroj.Text = inicijalniPacijent.adresaStanovanja.ulicaIBroj;
            Guest.IsChecked = inicijalniPacijent.isGuest;
            BrojZdravstveneKartice.Text = inicijalniPacijent.brojZdravstveneKartice;
            this.parent = parent;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            string ime = Ime.Text;
            string prezime = Prezime.Text;
            string jmbg = JMBG.Text;
            char pol;
            if (Pol.SelectedIndex == 0)
                pol = 'M';
            else
                pol = 'Ž';
            string brojTelefona = BrojTelefona.Text;
            string email = Email.Text;
            DateTime datumRodjenja = DatePicker.SelectedDate.Value;
            string korisnickoIme = KorisnickoIme.Text;
            string lozinka = Lozinka.Text;
            AdresaStanovanja adresaStanovanja = new AdresaStanovanja(UlicaIBroj.Text, new MestoStanovanja(Mesto.Text, PostanskiBroj.Text, new DrzavaStanovanja(Drzava.Text)));
            bool isGuest = (bool)Guest.IsChecked;
            string brojZdravstveneKartice = BrojZdravstveneKartice.Text;
            Pacijent p = new Pacijent(ime, prezime, jmbg, pol, brojTelefona, email, datumRodjenja, korisnickoIme, lozinka, adresaStanovanja, isGuest, brojZdravstveneKartice, new ZdravstveniKarton(PacijentFileStorage.GetAll().Count.ToString()), false);
            p.zdravstveniKarton.pacijent = p;
            PacijentFileStorage.UpdatePacijent(inicijalniPacijent.korisnickoIme, p);
            parent.updateTable();
            Close();
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
