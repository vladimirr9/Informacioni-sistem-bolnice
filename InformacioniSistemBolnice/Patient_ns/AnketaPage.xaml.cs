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
    /// Interaction logic for AnketaPage.xaml
    /// </summary>
    public partial class AnketaPage : Page
    {
        private PocetnaPacijent parent;
        private Termin selektovan;
        public Button kojiJePritisnut;
        public AnketaPage(PocetnaPacijent pp)
        {
            parent = pp;
            InitializeComponent();
            PretraziTermine();
            DataContext = this;
            rate.IsEnabled = false;
            rateHospital.Visibility = Visibility.Hidden;
            Provjera();
        }

        private void Provjera()
        {
            List<Anketa> ocjenjivanjeBolnice = new List<Anketa>();

            foreach (Anketa a in AnketaFileStorage.GetAll())
            {
                if (a.KorisnickoImeLekara == null && a.IdTermina == 0)
                {
                    if (a.KorisnickoImePacijenta.Equals(parent.Pacijent.korisnickoIme))
                    {
                        ocjenjivanjeBolnice.Add(a);
                    }
                }
            }

            DateTime posljednjaNapisana = DateTime.Parse("1970-01-01" + " " + "00:00:00");
            if (ocjenjivanjeBolnice.Count != 0)
            {
                posljednjaNapisana = ocjenjivanjeBolnice.ElementAt(0).NastanakAnkete;
            }

            foreach (Anketa a in ocjenjivanjeBolnice)
            {
                if (posljednjaNapisana < a.NastanakAnkete)
                {
                    posljednjaNapisana = a.NastanakAnkete;

                }
            }

            if (ocjenjivanjeBolnice.Count == 0 || posljednjaNapisana.AddSeconds(15) < DateTime.Now)
            {
                rateHospital.Visibility = Visibility.Visible;
            }

        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new StartPacijentPage(parent);
        }

        private void PretraziTermine()
        {


            foreach (Termin t in TerminFileStorage.GetAll())
            {
                if (t.status == StatusTermina.zakazan && !AnketaFileStorage.Contains(t.iDTermina))
                {
                    if (DateTime.Now.AddDays(-10) < t.datumZakazivanja && t.datumZakazivanja.Date < DateTime.Now)
                    {
                        PrikazPregleda.Items.Add(t);
                    }
                }

            }

        }

        private void rate_Click(object sender, RoutedEventArgs e)
        {
            kojiJePritisnut = rate;
            selektovan = (Termin)PrikazPregleda.SelectedItem;
            parent.imeLjekara.Visibility = Visibility.Visible;
            PopunjavanjeAnketePage pa = new PopunjavanjeAnketePage(this,parent,selektovan);
            parent.startWindow.Content = pa;
            return;

           
        }

        private void rateHospital_Click(object sender, RoutedEventArgs e)
        {
            kojiJePritisnut = rateHospital;
            PopunjavanjeAnketePage pa = new PopunjavanjeAnketePage(this,parent,selektovan);
            parent.startWindow.Content = pa;
            return;
        }

        public void UpdateTable()
        {

            PrikazPregleda.Items.Remove(selektovan);
        }

        private void PrikazPregleda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rate.IsEnabled = true;
        }
    }
}
