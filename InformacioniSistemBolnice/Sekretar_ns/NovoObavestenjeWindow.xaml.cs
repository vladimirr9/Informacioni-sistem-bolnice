using InformacioniSistemBolnice.Korisnik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for NovoObavestenjeWindow.xaml
    /// </summary>
    public partial class NovoObavestenjeWindow : Window
    {
        private PocetnaPage parent;
        private List<String> primaoci;
        public NovoObavestenjeWindow(PocetnaPage parent)
        {
            this.parent = parent;
            InitializeComponent();
            InitializePrimalac();
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

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            String naslov = Naslov.Text;
            String sadrzaj = Sadrzaj.Text;
            DateTime datumIVreme = new DateTime();
            datumIVreme = DateTime.Now;
            if (Primalac.SelectedItem.ToString().Equals("Svi korisnici"))
            {
                int id = ObavestenjeFileStorage.GetAll().Count;
                Obavestenje novoObavestenje = new Obavestenje(id, naslov, sadrzaj, datumIVreme);
                ObavestenjeFileStorage.AddObavestenje(novoObavestenje);
            }
            else if (Primalac.SelectedItem.ToString().Equals("Zaposleni"))
            {
                foreach (Sekretar sekretar in SekretarFileStorage.GetAll())
                {
                    int id = ObavestenjeFileStorage.GetAll().Count;
                    if (!sekretar.isDeleted)
                    {
                        Obavestenje novoObavestenje = new Obavestenje(id, naslov, sadrzaj, datumIVreme, sekretar.korisnickoIme);
                        ObavestenjeFileStorage.AddObavestenje(novoObavestenje);
                    }
                    
                }
                foreach (global::Lekar lekar in LekarFileStorage.GetAll())
                {
                    int id = ObavestenjeFileStorage.GetAll().Count;
                    if (!lekar.isDeleted)
                    {
                        Obavestenje novoObavestenje = new Obavestenje(id, naslov, sadrzaj, datumIVreme, lekar.korisnickoIme);
                        ObavestenjeFileStorage.AddObavestenje(novoObavestenje);
                    }
                }
                foreach (global::Upravnik upravnik in UpravnikFileStorage.GetAll())
                {
                    int id = ObavestenjeFileStorage.GetAll().Count;
                    if (!upravnik.isDeleted)
                    {
                        Obavestenje novoObavestenje = new Obavestenje(id, naslov, sadrzaj, datumIVreme, upravnik.korisnickoIme);
                        ObavestenjeFileStorage.AddObavestenje(novoObavestenje);
                    }
                }
            }
            else if (Primalac.SelectedItem.ToString().Equals("Svi pacijenti"))
            {
                foreach (Pacijent pacijent in PacijentFileStorage.GetAll())
                {
                    int id = ObavestenjeFileStorage.GetAll().Count;
                    if (!pacijent.isDeleted)
                    {
                        Obavestenje novoObavestenje = new Obavestenje(id, naslov, sadrzaj, datumIVreme, pacijent.korisnickoIme);
                        ObavestenjeFileStorage.AddObavestenje(novoObavestenje);
                    }
                }
            }
            else if (Primalac.SelectedItem != null)
            {
                int id = ObavestenjeFileStorage.GetAll().Count;
                String korisnickoIme = Primalac.SelectedItem.ToString().Split('-')[1].Trim();
                Pacijent pacijent = PacijentFileStorage.GetOne(korisnickoIme);
                if (!pacijent.isDeleted)
                {
                    Obavestenje novoObavestenje = new Obavestenje(id, naslov, sadrzaj, datumIVreme, pacijent.korisnickoIme);
                    ObavestenjeFileStorage.AddObavestenje(novoObavestenje);
                }
            }

            parent.updateTable();
            Close();
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
