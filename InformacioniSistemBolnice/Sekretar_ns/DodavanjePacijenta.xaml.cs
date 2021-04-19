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
        public DodavanjePacijenta(PacijentiPage parent)
        {
            this.parent = parent;
            InitializeComponent();
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
            DateTime datumRodjenja;
            if (DatePicker.SelectedDate.Value != null)
                datumRodjenja = DatePicker.SelectedDate.Value;
            else
                datumRodjenja = new DateTime();
            string korisnickoIme = KorisnickoIme.Text;
            string lozinka = Lozinka.Text;
            AdresaStanovanja adresaStanovanja = new AdresaStanovanja(UlicaIBroj.Text, new MestoStanovanja(Mesto.Text, PostanskiBroj.Text, new DrzavaStanovanja(Drzava.Text)));
            bool isGuest = (bool)Guest.IsChecked;
            string brojZdravstveneKartice = BrojZdravstveneKartice.Text;
            if (IsUnique(korisnickoIme))
            {
                Pacijent p = new Pacijent(ime, prezime, jmbg, pol, brojTelefona, email, datumRodjenja, korisnickoIme, lozinka, adresaStanovanja, isGuest, brojZdravstveneKartice, new ZdravstveniKarton(PacijentFileStorage.GetAll().Count.ToString()), false);
                p.zdravstveniKarton.pacijent = p;
                PacijentFileStorage.AddPacijent(p);
                parent.updateTable();
                this.Close();
            }
            

            
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        public bool IsUnique(String korisnickoIme)
        {
            if (PacijentFileStorage.GetOne(korisnickoIme) == null)
                return true;
            else return false;
        }

     

    
    }
}
