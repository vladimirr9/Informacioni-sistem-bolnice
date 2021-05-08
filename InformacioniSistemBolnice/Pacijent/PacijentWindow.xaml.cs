using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for PacijentWindow.xaml
    /// </summary>
    public partial class PacijentWindow : Window
    {
        public Pacijent pacijent { get; set; }
        public PacijentWindow(Pacijent pacijent)
        {
            this.pacijent = pacijent;
            InitializeComponent();
            this.DataContext = this;
            imePacijenta.Text = pacijent.ime + " " + pacijent.prezime;

            updateTable();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //dodaj
        {
            PacijentZakazuje zakazivanje = new PacijentZakazuje(this);
            Application.Current.MainWindow = zakazivanje;
            zakazivanje.Show();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //pomjeri
        {
            
            Termin termin = TerminFileStorage.GetOne(((Termin)PrikazPregleda.SelectedItem).iDTermina);
            if (termin.datumZakazivanja.Date <= DateTime.Now.AddHours(24).Date)  {
                MessageBox.Show("Nije moguće menjati termin koji je zakazan u naredna 24 sata!", "Greška");
            }
            else { 
                PacijentMijenja m = new PacijentMijenja(termin, this);
                Application.Current.MainWindow = m;
                m.Show();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //otkazi
        {
            if (PrikazPregleda.SelectedItem != null)
            {
                Termin termin = TerminFileStorage.GetOne(((Termin)PrikazPregleda.SelectedItem).iDTermina);
                if (termin.datumZakazivanja.Date <= DateTime.Now.AddHours(24).Date)
                {
                    MessageBox.Show("Nije moguće otkazati termin koji je zakazan u naredna 24 sata!", "Greška");
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da otkažete ovaj termin?", "Potvrda brisanja", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        TerminFileStorage.RemoveTermin(((Termin)(PrikazPregleda.SelectedItem)).iDTermina);
                        updateTable();
                    }
                }
            }

            

        }
        public void updateTable()
        {
            PrikazPregleda.Items.Clear();
            List<Termin> termini = TerminFileStorage.GetAll();
            foreach (Termin termin in termini)
            {
                if (pacijent.korisnickoIme == termin.Pacijent.korisnickoIme)
                {
                    if ((termin.status == StatusTermina.zakazan) && (termin.datumZakazivanja > DateTime.Now))
                        PrikazPregleda.Items.Add(termin);
                }
            }
        }

        private void button_Click_3(object sender, RoutedEventArgs e) //odjava

        {

            MainWindow m = new MainWindow();
            Application.Current.MainWindow = m;
            m.Show();
            this.Close();
            return;

        }

        private void button1_Click(object sender, RoutedEventArgs e) //nazad
        {
            PocetnaPacijent m = new PocetnaPacijent(this.pacijent);
            Application.Current.MainWindow = m;
            m.Show();
            this.Close();
            return;
        }

       
    }
}
