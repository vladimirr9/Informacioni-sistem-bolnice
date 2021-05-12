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
    /// Interaction logic for StartPacijentPage.xaml
    /// </summary>
    public partial class StartPacijentPage : Page
    {

        private static PocetnaPacijent parent;
        public DateTime DatumProvjereStatusa { get; set; }
        public StartPacijentPage(PocetnaPacijent p)
        {
            parent = p;
            InitializeComponent();
            DatumProvjereStatusa = DatumiProvjereStatusaFileStorage.PosljednjiDatumProvjere();
            if (DatumProvjereStatusa.AddMonths(1) < DateTime.Now)
            {
                ProvjeraStatusaPacijenta();
            }

            PacijentFileStorage.OdblokirajPacijenta(parent.Pacijent);
        }

        private void ProvjeraStatusaPacijenta()
        {
            DatumProvjereStatusa = DateTime.Now;
            DatumiProvjereStatusaFileStorage.AddDatum(DatumProvjereStatusa);
            int brojZakazivanja = InformacijeFileStorage.kolikoJePutaIzvrsenaNekaFunkcionalnost(parent.Pacijent.korisnickoIme, VrstaFunkcionalnosti.zakazivanje);
            int brojPomjeranja = InformacijeFileStorage.kolikoJePutaIzvrsenaNekaFunkcionalnost(parent.Pacijent.korisnickoIme, VrstaFunkcionalnosti.pomjeranje);
            int brojOtkazivanja = InformacijeFileStorage.kolikoJePutaIzvrsenaNekaFunkcionalnost(parent.Pacijent.korisnickoIme, VrstaFunkcionalnosti.otkazivanje);

            if (brojZakazivanja > 7 || brojOtkazivanja > 3 || brojPomjeranja > 3)
            {
                PacijentFileStorage.BanujPacijenta(parent.Pacijent);
            }
            else
            {
                parent.Pacijent.Banovan = false;
                parent.Pacijent.TrenutakBanovanja = DateTime.Parse("1970-01-01T00:00:00");
                PacijentFileStorage.UpdatePacijent(parent.Pacijent.korisnickoIme, parent.Pacijent);
                InformacijeFileStorage.RemoveInformacijePacijenta(parent.Pacijent.korisnickoIme);
            }


        }
    

        private void pregledTermina_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new PregledTerminaPage(parent);  
        }

        private void obavjestenja_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new ObavjestenjaPage(parent);
        }

        private void ocjenjivanje_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new AnketaPage(parent);
        }
    }
}

