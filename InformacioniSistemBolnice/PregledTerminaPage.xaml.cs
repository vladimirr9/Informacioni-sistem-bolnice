using InformacioniSistemBolnice.FileStorage;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for PregledTerminaPage.xaml
    /// </summary>
    public partial class PregledTerminaPage : Page
    {
        private PocetnaPacijent parent { get; set; }
        public PregledTerminaPage(PocetnaPacijent pp) 
        {
            parent = pp;
            InitializeComponent();
            this.DataContext = this;
            updateTable();

        }

        public void updateTable()
        {
            PrikazPregleda.Items.Clear();
            List<Termin> termini = TerminFileStorage.GetAll();
            foreach (Termin termin in termini)
            {
                if (parent.Pacijent.korisnickoIme == termin.Pacijent.korisnickoIme)
                {
                    if ((termin.status == StatusTermina.zakazan) && (termin.datumZakazivanja > DateTime.Now))
                        PrikazPregleda.Items.Add(termin);
                }
            }
        }

        
        private void back_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new StartPacijentPage(parent);
        }

        private void cancelTermin_Click(object sender, RoutedEventArgs e)
        {
            PacijentFileStorage.ProvjeritiStatusPacijenta(parent.Pacijent);
           if (parent.Pacijent.Banovan == true)
           {
               MessageBox.Show("Otkazivanje Vam je trenutno onemogućeno,obratite se sekretaru!", "Greška");
           }
           else
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
                        InformacijeOKoriscenjuFunkcionalnosti informacija = new InformacijeOKoriscenjuFunkcionalnosti(DateTime.Now, parent.Pacijent.korisnickoIme, VrstaFunkcionalnosti.otkazivanje);
                        InformacijeFileStorage.AddInformacije(informacija);
                    }
                }
            }
            else
            {
                MessageBox.Show("Prvo morate odabrati termin koji želite otkazati!", "Greška");

            }
            }

        }

        private void zakazi_Click(object sender, RoutedEventArgs e)
        {
            PacijentFileStorage.ProvjeritiStatusPacijenta(parent.Pacijent);
            if (parent.Pacijent.Banovan == true)
            {
                MessageBox.Show("Zakazivanje Vam je trenutno onemogućeno,obratite se sekretaru!", "Greška");
            }
            else
            {
                parent.startWindow.Content = new PacijentZakazujePage(parent);
            }
        }

        private void pomjeri_Click(object sender, RoutedEventArgs e)
        {
            PacijentFileStorage.ProvjeritiStatusPacijenta(parent.Pacijent);
            if (parent.Pacijent.Banovan == true)
            {
                MessageBox.Show("Pomeranje termina Vam je trenutno onemogućeno,obratite se sekretaru!", "Greška");
            }
            else
            {
                if (PrikazPregleda.SelectedIndex != -1)
                {
                    Termin termin = TerminFileStorage.GetOne(((Termin)PrikazPregleda.SelectedItem).iDTermina);
                    if (termin.datumZakazivanja.Date <= DateTime.Now.AddHours(24).Date)
                    {
                        MessageBox.Show("Nije moguće menjati termin koji je zakazan u naredna 24 sata!", "Greška");
                    }
                    else
                    {
                        parent.UpdateVisibilityOfComponents();
                        parent.startWindow.Content = new PomjeranjeTerminaPage(termin,parent);
                    }
                }
                else
                {
                    MessageBox.Show("Prvo morate odabrati termin koji želite pomeriti!", "Greška");

                }
            }
        }
    }
}
