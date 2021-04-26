using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for NoviTerminWindow.xaml
    /// </summary>
    public partial class IzmeniTerminWindow : Window
    {
        private TerminiPage parent;
        private List<global::Lekar> lekari;
        private List<Pacijent> pacijenti;
        private List<Prostorija> prostorije;
        private List<String> vremena;
        private Termin selektovanTermin;
        private bool potvrdjeno;
        private bool done;
        public IzmeniTerminWindow(TerminiPage parent, Termin selektovanTermin)
        {
            done = false;
            InitializeComponent();
            this.selektovanTermin = selektovanTermin;
            this.parent = parent;
            potvrdjeno = false;

            lekari = LekarFileStorage.GetAll();
            lekar.ItemsSource = lekari;
            prostorije = ProstorijaFileStorage.GetAll();
            prostorija.ItemsSource = prostorije;

            pacijenti = new List<Pacijent>();
            
            
            foreach (Pacijent tmpPacijent in PacijentFileStorage.GetAll())
                if (!tmpPacijent.isDeleted)
                    pacijenti.Add(tmpPacijent);
            pacijent.ItemsSource = pacijenti;
            pacijent.SelectedItem = selektovanTermin.Pacijent;
            foreach (global::Lekar l in lekari)
            {
                if (l.Equals(selektovanTermin.Lekar))
                    lekar.SelectedItem = l;
            }
            SetAvailableTimes();
            date.SelectedDate = selektovanTermin.datumZakazivanja;
            time.SelectedValue = selektovanTermin.datumZakazivanja.ToString("HH:mm");
            Trajanje.Text = selektovanTermin.trajanjeUMinutima.ToString();
            tip.SelectedIndex = (int)selektovanTermin.tipTermina;




            foreach (Prostorija pros in prostorije)
            {
                if (pros.IDprostorije == selektovanTermin.Prostorija.IDprostorije)
                {
                    prostorija.SelectedItem = pros;
                }
            }


            done = true;


        }

        private void SetAvailableTimes()
        {
            DateTime datum;
            if (date.SelectedDate != null)
                datum = DateTime.Parse(date.Text);
            else
                datum = DateTime.Now;

            vremena = new List<String>();
            List<Termin> termini = new List<Termin>();
            if (pacijent.SelectedItem != null && lekar.SelectedItem != null)
            {
                foreach (Termin termin in TerminFileStorage.GetAll())
                {
                    if (termin.status == StatusTermina.zakazan && (termin.Pacijent.Equals((Pacijent)pacijent.SelectedItem) || termin.Lekar.Equals((global::Lekar)lekar.SelectedItem)) && termin.datumZakazivanja.Date.Equals(datum.Date))
                    {
                        termini.Add(termin);
                    }
                }
            }
            else if (pacijent.SelectedItem != null)
            {
                foreach (Termin termin in TerminFileStorage.GetAll())
                {
                    if (termin.status == StatusTermina.zakazan && termin.Pacijent.Equals((Pacijent)pacijent.SelectedItem) && termin.datumZakazivanja.Date.Equals(datum.Date));
                    {
                        termini.Add(termin);
                    }
                }
            }

            DateTime k = DateTime.Parse("01-Jan-1970" + " " + "19:30");
            for (DateTime i = DateTime.Parse("01-Jan-1970" + " " + "08:00"); i <= k; i = i.AddMinutes(15))
            {
                bool slobodno = true;
                foreach (Termin termin in termini)
                {
                    DateTime pocetak = DateTime.Parse("01-Jan-1970" + " " + termin.datumZakazivanja.ToString("HH:mm"));
                    DateTime kraj = DateTime.Parse("01-Jan-1970" + " " + termin.datumZakazivanja.AddMinutes(termin.trajanjeUMinutima).ToString("HH:mm"));
                    if (i >= pocetak && i <= kraj)
                    {
                        slobodno = false;
                    }
                }
                if (slobodno)
                    vremena.Add(i.ToString("HH:mm"));
            }
            time.ItemsSource = vremena;
        }

        private void PotvrdiB_Click(object sender, RoutedEventArgs e)
        {
            Pacijent p = (Pacijent)pacijent.SelectedItem;
            global::Lekar l = (global::Lekar)lekar.SelectedItem;
            Prostorija pros = (Prostorija)prostorija.SelectedItem;
            String timeS = time.SelectedItem.ToString();
            String dateS = date.Text;
            DateTime dt = DateTime.Parse(dateS + " " + timeS);
            TipTermina tipTermina = (TipTermina)tip.SelectedIndex;
            int trajanje = Int32.Parse(Trajanje.Text);

            if (p.IsAvailable(dt, dt.AddMinutes(trajanje)))
            {
                Termin termin = new Termin(selektovanTermin.iDTermina, dt, trajanje, tipTermina, StatusTermina.zakazan, p, l, pros);
                TerminFileStorage.UpdateTermin(selektovanTermin.iDTermina,termin);
                parent.updateTable();
                potvrdjeno = true;
                this.Close();
            }
            else
                MessageBox.Show("Pacijentu ne odgovara ovako dugo trajanje, preklapa se sa drugim obavezama", "Pacijent zauzet", MessageBoxButton.OK);
        }

        private void OdustaniB_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void UpdateComponents()
        {
            if (!done) return;
            done = false;
            DateTime pocetak;
            DateTime kraj;
            CalculatePocetakAndKraj(out pocetak, out kraj);

            SetComponentIsEnabled();

            SetAvailableTimes();
            Prostorija selektovanaProstorija = null;
            global::Lekar selektovanLekar = null;
            if (prostorija.SelectedItem != null)
                selektovanaProstorija = (Prostorija)prostorija.SelectedItem;
            if (lekar.SelectedItem != null)
                selektovanLekar = (global::Lekar)lekar.SelectedItem;

            if (pacijent.SelectedItem != null)
            {
                if (!((Pacijent)(pacijent.SelectedItem)).IsAvailable(pocetak, kraj))
                {
                    Trajanje.Background = Brushes.Red;
                }
                else
                    Trajanje.Background = Brushes.White;

                lekari = new List<global::Lekar>();
                foreach (global::Lekar tmpLekar in LekarFileStorage.GetAll())
                {
                    if (tmpLekar.IsAvailable(pocetak, kraj) && !tmpLekar.isDeleted)
                    {
                        lekari.Add(tmpLekar);
                    }
                }
                prostorije = new List<Prostorija>();
                foreach (Prostorija tmpProstorija in ProstorijaFileStorage.GetAll())
                {
                    if (tmpProstorija.IsAvailable(pocetak, kraj) && !tmpProstorija.IsDeleted)
                    {
                        prostorije.Add(tmpProstorija);
                    }
                }
            }

            lekar.ItemsSource = lekari;
            if (selektovanLekar != null && lekari.Contains(selektovanLekar))
                lekar.SelectedItem = selektovanLekar;

            prostorija.ItemsSource = prostorije;
            if (selektovanaProstorija != null && prostorije.Contains(selektovanaProstorija))
                prostorija.SelectedItem = selektovanaProstorija;
            done = true;

        }
        private void CalculatePocetakAndKraj(out DateTime pocetak, out DateTime kraj)
        {
            if (time.SelectedItem != null && date.SelectedDate != null && Trajanje.Text != "")
            {
                String timeS = time.SelectedItem.ToString();
                String dateS = date.Text;
                int trajanje = Int32.Parse(Trajanje.Text);

                pocetak = DateTime.Parse(dateS + " " + timeS);
                kraj = pocetak.AddMinutes(trajanje);
            }
            else
            {
                pocetak = DateTime.Now;
                kraj = DateTime.Now;
            }
        }

        private void SetComponentIsEnabled()
        {
            if (pacijent.SelectedItem != null & lekar.SelectedItem != null & time.SelectedItem != null && date.SelectedDate != null && Trajanje.Text != "")
            {
                lekar.IsEnabled = true;
                date.IsEnabled = true;
                time.IsEnabled = true;
                Trajanje.IsEnabled = true;
                tip.IsEnabled = true;
                prostorija.IsEnabled = true;
            }
            else if (pacijent.SelectedItem != null)
            {
                lekar.IsEnabled = true;
                date.IsEnabled = true;
                time.IsEnabled = true;
                Trajanje.IsEnabled = true;
                tip.IsEnabled = false;
                prostorija.IsEnabled = false;
            }
            else
            {
                lekar.IsEnabled = false;
                date.IsEnabled = false;
                time.IsEnabled = false;
                Trajanje.IsEnabled = false;
                tip.IsEnabled = false;
                prostorija.IsEnabled = false;
            }
            if (pacijent.SelectedItem != null & lekar.SelectedItem != null & time.SelectedItem != null && date.SelectedDate != null && Trajanje.Text != "" && tip.SelectedItem != null && prostorija.SelectedItem != null)
                PotvrdiB.IsEnabled = true;
            else
                PotvrdiB.IsEnabled = false;
        }

        private void lekar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }

        private void tip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }

        private void pacijent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
            lekar.SelectedIndex = -1;
            date.SelectedDate = null;
            time.SelectedIndex = -1;
            Trajanje.Clear();
            tip.SelectedItem = null;
            prostorija.SelectedIndex = -1;

        }

        private void time_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }

        private void Trajanje_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateComponents();
        }

        private void prostorija_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }
        private void date_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }

        void IzmeniTerminWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!potvrdjeno)
            {
                selektovanTermin.status = StatusTermina.zakazan;
                TerminFileStorage.UpdateTermin(selektovanTermin.iDTermina, selektovanTermin);
            }
        }
    }
}
