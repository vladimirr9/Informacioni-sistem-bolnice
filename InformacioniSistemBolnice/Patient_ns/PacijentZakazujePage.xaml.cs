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
    /// Interaction logic for PacijentZakazujePage.xaml
    /// </summary>
    public partial class PacijentZakazujePage : Page
    {
        private const int trajanjePregleda = 15;
        private PocetnaPacijent parent;
        private List<string> availableTimes;
        private List<global::Doctor> lekari;
        private List<Prostorija> prostorije;
        private Pacijent pacijent;
        public PacijentZakazujePage(PocetnaPacijent p)
        {
            parent = p;
            InitializeComponent();
            parent.UpdateVisibilityOfComponents();
            availableTimes = new List<string>();
            timeComboBox();
            dugmePotvrdi.IsEnabled = false;
            prostorije = ProstorijaFileStorage.GetAll();
            BlackOutDates();
            lekari = new List<global::Doctor>();
            foreach (global::Doctor l in LekarFileStorage.GetAll())
            {
                if (l.doctorType.Equals(TipLekara.opstePrakse))
                {
                    lekari.Add(l);
                }
            }
            lekar.ItemsSource = lekari;
            pacijent = parent.Pacijent;
            parent.titleLabel.Content = "Zakazivanje pregleda";
            parent.titleLabel.Visibility = Visibility.Visible;
        }


        private void timeComboBox()
        {

            DateTime danas = DateTime.Today;

            for (DateTime tm = danas.AddHours(8); tm < danas.AddHours(20); tm = tm.AddMinutes(15))
            {
                availableTimes.Add(tm.ToString("HH:mm"));
            }

            time.ItemsSource = availableTimes;
        }
        private void BlackOutDates()
        {
            CalendarDateRange kalendar = new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1));
            date.BlackoutDates.Add(kalendar);
        }

        private void dugmePotvrdi_Click(object sender, RoutedEventArgs e)
        {
            global::Doctor l = (global::Doctor)lekar.SelectedItem;
            Pacijent p = parent.Pacijent;

            if (time.SelectedIndex != -1)
            {
                var item = time.SelectedItem;
                String t = item.ToString();
                String d = date.Text;
                DateTime dt = DateTime.Parse(d + " " + t);
                TipTermina tipt;
                if (l.doctorType.Equals(TipLekara.opstePrakse))
                {
                    tipt = TipTermina.pregledKodLekaraOpstePrakse;
                }
                else if (l.doctorType.Equals(TipLekara.hirurg))
                {
                    tipt = TipTermina.operacija;
                }
                else
                {
                    tipt = TipTermina.pregledKodLekaraSpecijaliste;
                }
                int id = TerminFileStorage.GetAll().Count + 1;

                DateTime start;
                DateTime end;
                CalculateStartAndEnd(out start, out end);

                Prostorija prvaDostupnaProstorija = GetAvailableRoom(start, end);
                Termin termin = new Termin(id, dt, trajanjePregleda, tipt, StatusTermina.zakazan, p, l, prvaDostupnaProstorija);
                TerminFileStorage.AddTermin(termin);
                PregledTerminaPage ptp = new PregledTerminaPage(parent);
                updateVisibility();
                parent.startWindow.Content = ptp;
                ptp.updateTable();
                InformacijeOKoriscenjuFunkcionalnosti informacija = new InformacijeOKoriscenjuFunkcionalnosti(DateTime.Now, pacijent.korisnickoIme, VrstaFunkcionalnosti.zakazivanje);
                InformacijeFileStorage.AddInformacije(informacija);
            }
        }


    

    private void UpdateComponents()
        {
            DateTime start;
            DateTime end;

            CalculateStartAndEnd(out start, out end);

            setEnabledButtonSubmit();

            CheckAvailableTimes();
        }
        private void CheckAvailableTimes()
        {

            DateTime datum;
            global::Doctor l = (global::Doctor)lekar.SelectedItem;
            if (date.SelectedDate != null)
            {
                datum = DateTime.Parse(date.Text);
            }
            else
            {
                datum = DateTime.Now;
            }

            availableTimes = new List<string>();
            List<Termin> termini = new List<Termin>();
            if (lekar.SelectedItem != null && date.SelectedDate != null)
            {
                foreach (Termin termin in TerminFileStorage.GetAll())
                {
                    if (l.jmbg == termin.Doctor.jmbg)
                    {
                        if (termin.status == StatusTermina.zakazan && termin.datumZakazivanja.Date.Equals(date.SelectedDate))
                        {
                            termini.Add(termin);
                        }
                    }

                    if (pacijent.korisnickoIme == termin.Pacijent.korisnickoIme)
                    {
                        if (termin.status == StatusTermina.zakazan && termin.datumZakazivanja.Date.Equals(date.SelectedDate))
                        {
                            termini.Add(termin);
                        }
                    }


                }
            }
            DateTime danas = DateTime.Today;

            for (DateTime tm = danas.AddHours(8); tm < danas.AddHours(20); tm = tm.AddMinutes(15))
            {
                bool slobodno = true;
                foreach (Termin termin in termini)
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
            time.ItemsSource = availableTimes;
        }

        private void setEnabledButtonSubmit()
        {
            if (lekar.SelectedItem != null && date.SelectedDate != null && time.SelectedItem != null)
            {
                dugmePotvrdi.IsEnabled = true;
            }
            else
            {
                dugmePotvrdi.IsEnabled = false;
            }
        }
        private void CalculateStartAndEnd(out DateTime start, out DateTime end)
        {
            if (time.SelectedItem != null && date.SelectedDate != null)
            {
                String timeSelected = time.SelectedItem.ToString();
                String dateSelected = date.Text;

                start = DateTime.Parse(dateSelected + " " + timeSelected);
                end = start.AddMinutes(trajanjePregleda);
            }
            else
            {
                start = DateTime.Now;
                end = DateTime.Now;
            }
        }

        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }

        private void time_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }
        private void lekar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
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

        private void back_Click(object sender, RoutedEventArgs e)
        {
            updateVisibility();
            parent.startWindow.Content = new PregledTerminaPage(parent);
        }

        private void updateVisibility() {
            parent.titleLabel.Visibility = Visibility.Hidden;
            parent.titlePriorityLabel.Visibility = Visibility.Hidden;
            parent.odjava.Visibility = Visibility.Visible;
            parent.imePacijenta.Visibility = Visibility.Visible;
        }

        private void search_Click_1(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new PacijentZakazujePoPrioritetuPage(parent);
        }
    }
}
