using InformacioniSistemBolnice.FileStorage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for PrioritetLjekar.xaml
    /// </summary>
    public partial class PrioritetLjekar : Page
    {
        private const int trajanjePregleda = 15;
        private List<global::Lekar> ljekariLista;
        private Pacijent pacijent;
        private List<Termin> termini;
        private List<string> availableTimes;
        private List<Prostorija> prostorije;
        private PacijentWindow parent;
        private PacijentZakazujePoPrioritetu parentp;
        public PrioritetLjekar(Pacijent p, PacijentWindow pw, PacijentZakazujePoPrioritetu pzpp)
        {
            this.pacijent = p;
            parent = pw;
            parentp = pzpp;
            termini = new List<Termin>();
            InitializeComponent();
            ljekariLista = new List<global::Lekar>();
            foreach (global::Lekar l in LekarFileStorage.GetAll())
            {
                if (l.tipLekara == TipLekara.opstePrakse)
                {
                    ljekariLista.Add(l);
                }
            }
            ljekari.ItemsSource = ljekariLista;
            submit.IsEnabled = false;
            BlackOutDates();
            date.IsEnabled = false;
            timeLabel.Visibility = Visibility.Hidden;
            times.Visibility = Visibility.Hidden;
            availableTimes = new List<string>();
        }

        private void BlackOutDates()
        {
            CalendarDateRange kalendar = new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1));
            date.BlackoutDates.Add(kalendar);

        }

        private void ljekari_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            date.IsEnabled = true;
        }

        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            timeLabel.Visibility = Visibility.Visible;
            times.Visibility = Visibility.Visible;
            pretraziTermine();
        }

        private void pretraziTermine()
        {
            times.Items.Clear();
            availableTimes.Clear();
            termini.Clear();
            global::Lekar selektovanLjekar = (global::Lekar)ljekari.SelectedItem;
            foreach (Termin t in TerminFileStorage.GetAll())
            {
                if (t.Lekar.jmbg.Equals(selektovanLjekar.jmbg))
                {
                    if (t.datumZakazivanja.Date.Equals(date.SelectedDate) && t.status == StatusTermina.zakazan)
                    {
                        termini.Add(t);
                    }
                }

                if (pacijent.korisnickoIme == t.Pacijent.korisnickoIme && t.datumZakazivanja.Date.Equals(date.SelectedDate))
                {
                    if (t.status == StatusTermina.zakazan)
                    {

                        termini.Add(t);

                    }
                }
            }

            List<Termin> terminiBezDuplikata = termini.Distinct().ToList();
            DateTime danas = DateTime.Today;

            for (DateTime tm = danas.AddHours(8); tm < danas.AddHours(20); tm = tm.AddMinutes(15))
            {
                bool slobodno = true;
                foreach (Termin termin in terminiBezDuplikata)
                {
                    DateTime start = DateTime.Parse(termin.datumZakazivanja.ToString("HH:mm"));
                    DateTime end = DateTime.Parse(termin.datumZakazivanja.AddMinutes(termin.trajanjeUMinutima).ToString("HH:mm"));
                    if (tm >= start && tm < end)
                    {
                        slobodno = false;
                    }
                }
                if (slobodno)
                    availableTimes.Add(tm.ToString("HH:mm"));

                if (date.SelectedDate == danas)
                {
                    if (tm < DateTime.Now.AddMinutes(30))
                    {
                        availableTimes.Remove(tm.ToString("HH:mm"));
                    }
                }

            }
            foreach (string time in availableTimes)
            {
                times.Items.Add(time);
            }

        }

        private void times_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            submit.IsEnabled = true;
        }

        private void submit_Click(object sender, RoutedEventArgs e) //potvrda
        {
            ZakaziTermin();
            InformacijeOKoriscenjuFunkcionalnosti informacija = new InformacijeOKoriscenjuFunkcionalnosti(DateTime.Now,pacijent.korisnickoIme,VrstaFunkcionalnosti.zakazivanje);
            InformacijeFileStorage.AddInformacije(informacija);

        }

        private void ZakaziTermin()
        {
            global::Lekar l = (global::Lekar)ljekari.SelectedItem;
            Pacijent p = this.pacijent;


            var item = times.SelectedItem;
            String t = item.ToString();
            String d = date.Text;
            DateTime start = DateTime.Parse(d + " " + t);
            TipTermina tipt;
            if (l.tipLekara.Equals(TipLekara.opstePrakse))
            {
                tipt = TipTermina.pregledKodLekaraOpstePrakse;
            }
            else if (l.tipLekara.Equals(TipLekara.hirurg))
            {
                tipt = TipTermina.operacija;
            }
            else
            {
                tipt = TipTermina.pregledKodLekaraSpecijaliste;
            }
            int id = TerminFileStorage.GetAll().Count + 1;
            DateTime end = start.AddMinutes(trajanjePregleda);
           
            Prostorija prvaDostupnaProstorija = GetAvailableRoom(start,end);
            Termin termin = new Termin(id, start, trajanjePregleda, tipt, StatusTermina.zakazan, p, l, prvaDostupnaProstorija);
            TerminFileStorage.AddTermin(termin);
            parent.updateTable();
            parentp.Close();

        }

        private Prostorija GetAvailableRoom(DateTime pocetak, DateTime kraj)
        {
            prostorije = new List<Prostorija>();
            foreach (Prostorija prostorija in ProstorijaFileStorage.GetAll())
            {
                if (prostorija.IsAvailable(pocetak, kraj) && !prostorija.IsDeleted)
                {
                    prostorije.Add(prostorija);
                }
            }

            return prostorije.ElementAt(0);

        }

        private void Button_Click(object sender, RoutedEventArgs e) //odustanak
        {
            parentp.Close();
        }
    }
}
