using InformacioniSistemBolnice.Korisnik;
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
    /// Interaction logic for IzmeniObavestenjeWindow.xaml
    /// </summary>
    public partial class IzmeniObavestenjeWindow : Window
    {
        private PocetnaPage parent;
        private Obavestenje staroObavestenje;
        private List<String> primaoci;
        public IzmeniObavestenjeWindow(PocetnaPage parent, Obavestenje staroObavestenje)
        {
            this.parent = parent;
            this.staroObavestenje = staroObavestenje;
            InitializeComponent();
            InitializePrimalac();

            Naslov.Text = staroObavestenje.naslov;
            Sadrzaj.Text = staroObavestenje.tekst;

        }



        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            String naslov = Naslov.Text;
            String sadrzaj = Sadrzaj.Text;
            DateTime datumIVreme = new DateTime();
            datumIVreme = DateTime.Now;

            staroObavestenje.naslov = naslov;
            staroObavestenje.tekst = sadrzaj;
            staroObavestenje.datumNastanka = datumIVreme;

            ObavestenjeFileStorage.UpdateObavestenje(staroObavestenje.idObavestenja, staroObavestenje);
            parent.updateTable();
            Close();
        }

        private void InitializePrimalac()
        {
            primaoci = new List<String>();
            primaoci.Add("Svi korisnici");
            primaoci.Add("Zaposleni");
            primaoci.Add("Svi pacijenti");
            foreach (Pacijent pacijent in PacijentFileStorage.GetAll())
            {
                if (!pacijent.isDeleted)
                    primaoci.Add(pacijent.ime + " " + pacijent.prezime + " - " + pacijent.korisnickoIme);
            }
            Primalac.ItemsSource = primaoci;
            Primalac.SelectedIndex = 0;
        }
        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
