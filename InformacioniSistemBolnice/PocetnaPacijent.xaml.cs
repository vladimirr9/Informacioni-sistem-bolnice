using InformacioniSistemBolnice.FileStorage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Threading;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for PocetnaPacijent.xaml
    /// </summary>
    public partial class PocetnaPacijent : Window
    {

        public Pacijent Pacijent { get; set; }
        public DateTime DatumProvjereStatusa { get; set; }
        public PocetnaPacijent(Pacijent pacijent)
        {
            this.Pacijent = pacijent;
            InitializeComponent();
            DatumProvjereStatusa = DatumiProvjereStatusaFileStorage.PosljednjiDatumProvjere();
            imePacijenta.Text = pacijent.ime + " " + pacijent.prezime;
            if (DatumProvjereStatusa.AddMinutes(2) < DateTime.Now)
            {
                ProvjeraStatusaPacijenta();
            }

            PacijentFileStorage.OdblokirajPacijenta(Pacijent);


        }

        

        private void ProvjeraStatusaPacijenta()
        {
            DatumProvjereStatusa = DateTime.Now;
            DatumiProvjereStatusaFileStorage.AddDatum(DatumProvjereStatusa);
            int brojZakazivanja = InformacijeFileStorage.kolikoJePutaIzvrsenaNekaFunkcionalnost(Pacijent.korisnickoIme, VrstaFunkcionalnosti.zakazivanje);
            int brojPomjeranja = InformacijeFileStorage.kolikoJePutaIzvrsenaNekaFunkcionalnost(Pacijent.korisnickoIme, VrstaFunkcionalnosti.pomjeranje);
            int brojOtkazivanja = InformacijeFileStorage.kolikoJePutaIzvrsenaNekaFunkcionalnost(Pacijent.korisnickoIme, VrstaFunkcionalnosti.otkazivanje);

                if (brojZakazivanja > 2 || brojOtkazivanja > 1 || brojPomjeranja > 1)
                {
                    Pacijent.Banovan = true;
                    Pacijent.TrenutakBanovanja = DateTime.Now;
                    PacijentFileStorage.UpdatePacijent(Pacijent.korisnickoIme, Pacijent);
                    InformacijeFileStorage.RemoveInformacijePacijenta(Pacijent.korisnickoIme);
                }
                else {
                    Pacijent.Banovan = false;
                    Pacijent.TrenutakBanovanja = DateTime.Parse("1970-01-01T00:00:00");
                    PacijentFileStorage.UpdatePacijent(Pacijent.korisnickoIme, Pacijent);
                    InformacijeFileStorage.RemoveInformacijePacijenta(Pacijent.korisnickoIme);
                }

            
        }

        private void odjava_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            Application.Current.MainWindow = m;
            m.Show();
            this.Close();
            return;
        }

        private void pregledTermina_Click(object sender, RoutedEventArgs e)
        {
            PacijentWindow pw = new PacijentWindow(this.Pacijent);
            Application.Current.MainWindow = pw;
            pw.Show();
            this.Close();
            return;
        }

        private void obavjestenja_Click(object sender, RoutedEventArgs e)
        {
            ObavjestenjaPacijent op = new ObavjestenjaPacijent(this.Pacijent);
            Application.Current.MainWindow = op;
            op.Show();
            this.Close();
            return;
        }

        private void ocjenjivanje_Click(object sender, RoutedEventArgs e)
        {
            PacijentFileStorage.OdblokirajPacijenta(Pacijent);

            if (Pacijent.Banovan == true)
            {
                MessageBox.Show("Ova opcija Vam je trenutno onemogućena!", "Greška");
            }
            else
            {
                Ocjenjivanje o = new Ocjenjivanje(this);
                Application.Current.MainWindow = o;
                o.Show();
                this.Close();
                return;
            }
        }

       

        

       
    }
}
