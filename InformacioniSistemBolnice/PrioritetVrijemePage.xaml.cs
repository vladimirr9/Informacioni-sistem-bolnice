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
    /// Interaction logic for PrioritetVrijemePage.xaml
    /// </summary>
    public partial class PrioritetVrijemePage : Page
    {

        private const int trajanjePregleda = 15;
        private PocetnaPacijent parent;
        private PacijentZakazujePoPrioritetuPage parentp;
        private List<string> listStart;
        private List<string> listEnd;
        private List<Room> prostorije;
        public PrioritetVrijemePage(PocetnaPacijent pp,PacijentZakazujePoPrioritetuPage pzppp)
        {
            parent = pp;
            parentp = pzppp;
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
                    if (lekar.tipLekara.Equals(TipLekara.opstePrakse))
                    {
                        if (lekar.IsAvailable(tm, end.AddMinutes(-1)) && parent.Pacijent.IsAvailable(tm, end.AddMinutes(-1)))
                        {
                            PrikazSlobodnihTermina.Items.Add(new SlobodniTermini(lekar, tm.ToString("HH:mm")));
                        }
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

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            ZakaziTermin();
            InformacijeOKoriscenjuFunkcionalnosti informacija = new InformacijeOKoriscenjuFunkcionalnosti(DateTime.Now, parent.Pacijent.korisnickoIme, VrstaFunkcionalnosti.zakazivanje);
            InformacijeFileStorage.AddInformacije(informacija);
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

        private void ZakaziTermin()
        {

            SlobodniTermini selektovanTermin = (SlobodniTermini)PrikazSlobodnihTermina.SelectedItem;
            String d = date.Text;
            String t = selektovanTermin.AvailableTimes;
            DateTime start = DateTime.Parse(d + " " + t);
            TipTermina tipt;
            if (selektovanTermin.Ljekar.tipLekara.Equals(TipLekara.opstePrakse))
            {
                tipt = TipTermina.pregledKodLekaraOpstePrakse;
            }
            else if (selektovanTermin.Ljekar.tipLekara.Equals(TipLekara.hirurg))
            {
                tipt = TipTermina.operacija;
            }
            else
            {
                tipt = TipTermina.pregledKodLekaraSpecijaliste;
            }
            int id = TerminFileStorage.GetAll().Count + 1;
            DateTime end = start.AddMinutes(trajanjePregleda);

            Room prvaDostupnaProstorija = GetAvailableRoom(start, end);
            Termin termin = new Termin(id, start, trajanjePregleda, tipt, StatusTermina.zakazan, parent.Pacijent, selektovanTermin.Ljekar, prvaDostupnaProstorija);
            TerminFileStorage.AddTermin(termin);
            PregledTerminaPage ptp = new PregledTerminaPage(parent);
            updateVisibility();
            parent.startWindow.Content = ptp;
            ptp.updateTable();

        }

        private void updateVisibility()
        {
            parent.titleLabel.Visibility = Visibility.Hidden;
            parent.titlePriorityLabel.Visibility = Visibility.Hidden;
            parent.odjava.Visibility = Visibility.Visible;
            parent.imePacijenta.Visibility = Visibility.Visible;
        }

        private Room GetAvailableRoom(DateTime pocetak, DateTime kraj)
        {
            prostorije = new List<Room>();
            foreach (Room prostorija in RoomFileRepoistory.GetAll())
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
