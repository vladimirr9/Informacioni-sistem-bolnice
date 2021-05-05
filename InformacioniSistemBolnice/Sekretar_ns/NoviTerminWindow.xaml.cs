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
    public partial class NoviTerminWindow : Window
    {
        private TerminiPage parent;
        private List<global::Lekar> lekari;
        private List<Pacijent> pacijenti;
        private List<Prostorija> prostorije;
        private List<String> vremena;
        public NoviTerminWindow(TerminiPage parent)
        {
            this.parent = parent;


            InitializeComponent();

            InitializePatientValues();
            SetComponentIsEnabled();
        }

        private void PotvrdiB_Click(object sender, RoutedEventArgs e)
        {
            Pacijent selectedPatient = (Pacijent) pacijent.SelectedItem;
            global::Lekar selectedDoctor = (global::Lekar)lekar.SelectedItem;
            Prostorija selectedRoom = (Prostorija)prostorija.SelectedItem;
            String timeS = time.SelectedItem.ToString();
            String dateS = date.Text;
            DateTime selectedDateTime = DateTime.Parse(dateS + " " + timeS);
            TipTermina tipTermina = (TipTermina)tip.SelectedIndex;
            int id = TerminFileStorage.GetAll().Count + 1;
            int duration = Int32.Parse(Trajanje.Text);

            if (selectedPatient.IsAvailable(selectedDateTime, selectedDateTime.AddMinutes(duration)))
            {
                Termin termin = new Termin(id, selectedDateTime, duration, tipTermina, StatusTermina.zakazan, selectedPatient, selectedDoctor, selectedRoom);
                TerminFileStorage.AddTermin(termin);
                parent.updateTable();
                this.Close();
            }
            else
                MessageBox.Show("Pacijentu ne odgovara ovako dugo trajanje termina, preklapa se sa drugim obavezama", "Pacijent zauzet", MessageBoxButton.OK);
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
            SetComponentIsEnabled();
            if (pacijent.SelectedItem == null)
                return;

            DateTime pocetak;
            DateTime kraj;

            CalculatePocetakAndKraj(out pocetak, out kraj);

            SetAvailableTimes();

            ColorDurationField(pocetak, kraj);
            UpdateAvailableLekarList(pocetak, kraj);
            UpdateAvailableRoomList(pocetak, kraj);

            lekar.ItemsSource = lekari;
            prostorija.ItemsSource = prostorije;
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
        private void SetAvailableTimes()
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
        private void InitializePatientValues()
        {
            pacijenti = new List<Pacijent>();
            foreach (Pacijent tmpPacijent in PacijentFileStorage.GetAll())
                if (!tmpPacijent.isDeleted)
                    pacijenti.Add(tmpPacijent);
            pacijent.ItemsSource = pacijenti;
        }
    }
    
}
