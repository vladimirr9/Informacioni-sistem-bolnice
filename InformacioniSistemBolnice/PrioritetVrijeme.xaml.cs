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
    /// Interaction logic for PrioritetVrijeme.xaml
    /// </summary>
    public partial class PrioritetVrijeme : Page
    {
        private const int trajanjePregleda = 15;
        private Pacijent pacijent;
        private PacijentWindow parent;
        private PacijentZakazujePoPrioritetu parentp;
        private List<string> listStart;
        private List<string> listEnd;
        private List<Prostorija> prostorije;

        public PrioritetVrijeme(Pacijent p, PacijentWindow pw, PacijentZakazujePoPrioritetu pzpp)
        {
            this.pacijent = p;
            parent = pw;
            parentp = pzpp;
            listStart = new List<string>();
            listEnd = new List<string>();
            InitializeComponent();
            DataContext = this; ;
            BlackOutDates();
            submit.IsEnabled = false;
            date.IsEnabled = false;
            slobodniTerminiLabela.Visibility = Visibility.Hidden;
            PrikazSlobodnihTermina.Visibility = Visibility.Hidden;
            InitializeStartTimes();
            endTime.IsEnabled = false;

        }

        private void BlackOutDates()
        {
            CalendarDateRange kalendar = new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1));
            date.BlackoutDates.Add(kalendar);

        }
        private void InitializeStartTimes()
        {
            DateTime danas = DateTime.Today;

            for (DateTime tm = danas.AddHours(8); tm < danas.AddHours(20); tm = tm.AddMinutes(15))
            {
                listStart.Add(tm.ToString("HH:mm"));
            }
            startTime.ItemsSource = listStart;
        }

        private void InitializeEndTimes()
        {
            listEnd.Clear();
            DateTime danas = DateTime.Today;
            String datum = danas.ToShortDateString();
            DateTime pocetak = DateTime.Parse(datum + " " + startTime.SelectedItem.ToString()).AddMinutes(15);

            for (DateTime tm = pocetak; tm < danas.AddHours(20); tm = tm.AddMinutes(15))
            {
                listEnd.Add(tm.ToString("HH:mm"));
            }
            endTime.ItemsSource = listEnd;

        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            parentp.Close();
        }

        private void startTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            endTime.IsEnabled = true;
            endTime.SelectedIndex = -1;
            InitializeEndTimes();
        }

        private void endTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            date.IsEnabled = true;

        }

        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            slobodniTerminiLabela.Visibility = Visibility.Visible;
            PrikazSlobodnihTermina.Visibility = Visibility.Visible;
            pretraziTermine();
        }

        private void pretraziTermine()
        {
            DateTime danas = DateTime.Today;
            String pocetak = startTime.SelectedItem.ToString();
            String kraj = endTime.SelectedItem.ToString();
            String datum = date.Text;

            DateTime pocetniDatum = DateTime.Parse(datum + " " + pocetak);
            DateTime krajnjiDatum = DateTime.Parse(datum + " " + kraj);

            List<global::Lekar> ljekari = LekarFileStorage.GetAll();
            foreach (global::Lekar lekar in ljekari)
            {
                for (DateTime tm = pocetniDatum; tm < krajnjiDatum; tm = tm.AddMinutes(15))
                {
                    DateTime end = tm.AddMinutes(15);
                    if (lekar.IsAvailable(tm, end.AddMinutes(-1)) && this.pacijent.IsAvailable(tm, end.AddMinutes(-1)))
                    {
                        PrikazSlobodnihTermina.Items.Add(new SlobodniTermini(lekar, tm.ToString("HH:mm")));
                    }

                }
            }



        }

        public class SlobodniTermini
        {
            public global::Lekar Ljekar { get; set; }
            public string AvailableTimes { get; set; }


            public SlobodniTermini()
            {

            }
            public SlobodniTermini(global::Lekar ljekarNovi, string times)
            {
                this.Ljekar = ljekarNovi;
                this.AvailableTimes = times;

            }



        }

        private void PrikazSlobodnihTermina_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PrikazSlobodnihTermina.SelectedItem != null)
            {
                submit.IsEnabled = true;
            }
            
        }

        private void submit_Click(object sender, RoutedEventArgs e) //potvrda
        {

            ZakaziTermin();
        }


        private void ZakaziTermin()
        {

            SlobodniTermini selektovanTermin = (SlobodniTermini)PrikazSlobodnihTermina.SelectedItem;
            String d = date.Text;
            String t = selektovanTermin.AvailableTimes;
            DateTime start = DateTime.Parse(d + " " + t);
            TipTermina tipt = TipTermina.pregledKodLekaraOpstePrakse;
            int id = TerminFileStorage.GetAll().Count + 1;
            DateTime end = start.AddMinutes(trajanjePregleda);

            Prostorija prvaDostupnaProstorija = GetAvailableRoom(start, end);
            Termin termin = new Termin(id, start, trajanjePregleda, tipt, StatusTermina.zakazan, this.pacijent,selektovanTermin.Ljekar , prvaDostupnaProstorija);
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
    }
}
