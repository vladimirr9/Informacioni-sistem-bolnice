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
            if (Primalac.SelectedItems.Count == 0)
                return;
            String naslov = Naslov.Text;
            String sadrzaj = Sadrzaj.Text;
            DateTime datumIVreme = new DateTime();
            datumIVreme = DateTime.Now;
            int id = ObavestenjeFileStorage.GetAll().Count;
            Obavestenje newNotification = new Obavestenje(id, naslov, sadrzaj, datumIVreme);
            foreach (var item in Primalac.SelectedItems)
            {
                if (item.ToString().Equals("Svi korisnici"))
                    newNotification.Recipients.Add("ALL_USERS");
                else if (item.ToString().Equals("Zaposleni"))
                    newNotification.Recipients.Add("EMPLOYED_USERS");
                else if (item.ToString().Equals("Svi pacijenti"))
                    newNotification.Recipients.Add("PATIENT_USERS");
                else
                {
                    String username = item.ToString().Split('-')[1].Trim();
                    newNotification.Recipients.Add(username);
                }
            }
            ObavestenjeFileStorage.AddObavestenje(newNotification);
            parent.updateTable();
            Close();
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
