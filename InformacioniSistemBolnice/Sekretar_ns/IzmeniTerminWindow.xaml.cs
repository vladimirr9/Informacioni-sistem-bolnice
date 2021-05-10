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
            UpdateAvailableTimes();
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


        private void pacijent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetComponentIsEnabled();
            ResetComponentValues();
            UpdateComponents();
        }
        private void lekar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }

        private void time_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }


        private void prostorija_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetComponentIsEnabled();
        }
        private void date_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }

        private void Trajanje_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateComponents();
        }
        private void tip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetComponentIsEnabled();
        }
        private void UpdateComponents()
        {
            if (!done)
                return;
            done = false;
            SetComponentIsEnabled();
            if (pacijent.SelectedItem == null)
                return;

            DateTime pocetak;
            DateTime kraj;

            CalculatePocetakAndKraj(out pocetak, out kraj);

            UpdateAvailableTimes();

            ColorDurationField(pocetak, kraj);
            UpdateAvailableLekarList(pocetak, kraj);
            UpdateAvailableRoomList(pocetak, kraj);

            lekar.ItemsSource = lekari;
            prostorija.ItemsSource = prostorije;
            done = true;
        }
        private void UpdateAvailableRoomList(DateTime pocetak, DateTime kraj)
        {
            prostorije = new List<Prostorija>();
            foreach (Prostorija prostorija in ProstorijaFileStorage.GetAll())
            {
                if (prostorija.IsAvailable(pocetak, kraj) && !prostorija.IsDeleted)
                {
                    prostorije.Add(prostorija);
                }
            }
        }
        private void UpdateAvailableLekarList(DateTime pocetak, DateTime kraj)
        {
            lekari = new List<global::Lekar>();
            foreach (global::Lekar tmpLekar in LekarFileStorage.GetAll())
            {
                if (tmpLekar.IsAvailable(pocetak, kraj) && !tmpLekar.isDeleted)
                {
                    lekari.Add(tmpLekar);
                }
            }
        }
        private void ColorDurationField(DateTime pocetak, DateTime kraj)
        {
            if (((Pacijent)(pacijent.SelectedItem)).IsAvailable(pocetak, kraj))
                Trajanje.Background = Brushes.White;
            else
                Trajanje.Background = Brushes.Red;
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
                kraj = pocetak;
            }
        }

        private void SetComponentIsEnabled()
        {
            lekar.IsEnabled = (pacijent.SelectedItem != null);
            date.IsEnabled = (pacijent.SelectedItem != null);
            time.IsEnabled = (pacijent.SelectedItem != null);
            Trajanje.IsEnabled = (pacijent.SelectedItem != null);
            tip.IsEnabled = (pacijent.SelectedItem != null && lekar.SelectedItem != null && time.SelectedItem != null && date.SelectedDate != null && Trajanje.Text != "");
            prostorija.IsEnabled = (pacijent.SelectedItem != null && lekar.SelectedItem != null && time.SelectedItem != null && date.SelectedDate != null && Trajanje.Text != "");
            PotvrdiB.IsEnabled = (pacijent.SelectedItem != null && lekar.SelectedItem != null && time.SelectedItem != null && date.SelectedDate != null && Trajanje.Text != "" && tip.SelectedItem != null && prostorija.SelectedItem != null);
        }
        private void UpdateAvailableTimes()
        {
            DateTime datum;
            if (date.SelectedDate != null)
                datum = DateTime.Parse(date.Text);
            else
                datum = DateTime.Now;


            List<Termin> termini = new List<Termin>();
            foreach (Termin termin in TerminFileStorage.GetAll())
            {
                if (termin.OccursOn(datum) && termin.InvolvesEither((Pacijent)pacijent.SelectedItem, (global::Lekar)lekar.SelectedItem) && termin.status == StatusTermina.zakazan)
                {
                    termini.Add(termin);
                }
            }
            time.ItemsSource = GetAvailableAppointmentTimes(termini);
        }

        private List<String> GetAvailableAppointmentTimes(List<Termin> termini)
        {
            vremena = new List<String>();
            DateTime lastPossibleTime = DateTime.Parse("01-Jan-1970" + " " + "19:30");
            for (DateTime potentialTime = DateTime.Parse("01-Jan-1970" + " " + "08:00"); potentialTime <= lastPossibleTime; potentialTime = potentialTime.AddMinutes(15))
            {
                bool slobodno = true;
                foreach (Termin termin in termini)
                {
                    DateTime pocetak = DateTime.Parse("01-Jan-1970" + " " + termin.datumZakazivanja.ToString("HH:mm"));
                    DateTime kraj = DateTime.Parse("01-Jan-1970" + " " + termin.datumZakazivanja.AddMinutes(termin.trajanjeUMinutima).ToString("HH:mm"));
                    if (potentialTime >= pocetak && potentialTime <= kraj)
                    {
                        slobodno = false;
                    }
                }
                if (slobodno)
                    vremena.Add(potentialTime.ToString("HH:mm"));
            }
            return vremena;
        }

        private void ResetComponentValues()
        {
            lekar.SelectedIndex = -1;
            date.SelectedDate = null;
            time.SelectedIndex = -1;
            Trajanje.Clear();
            tip.SelectedItem = null;
            prostorija.SelectedIndex = -1;
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
