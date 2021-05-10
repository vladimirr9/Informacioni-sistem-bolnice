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
using System.Windows.Shapes;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for PacijentZakazuje.xaml
    /// </summary>
    public partial class PacijentZakazuje : Window
    {
        private const int trajanjePregleda = 15;
        private PacijentWindow parent;
        private List<string> availableTimes;
        private List<global::Lekar> lekari;
        private List<Prostorija> prostorije;
        private Pacijent pacijent;



        public PacijentZakazuje(PacijentWindow window)
        {
            InitializeComponent();
            
            this.parent = window;
            availableTimes = new List<string>();
            time.ItemsSource = availableTimes;
            dugmePotvrdi.IsEnabled = false;
            prostorije = ProstorijaFileStorage.GetAll();
            blackOutDates();
           lekari = new List<global::Lekar>();
            foreach(global::Lekar l in LekarFileStorage.GetAll()) {
                if (l.tipLekara.Equals(TipLekara.opstePrakse)) {
                    lekari.Add(l);
                }
            } 
            lekar.ItemsSource = lekari;
            pacijent = parent.pacijent;
           
            
            
           
        }

        private void blackOutDates() {
            CalendarDateRange kalendar = new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1));
            date.BlackoutDates.Add(kalendar);
        }

       

        private void Button_Click(object sender, RoutedEventArgs e) //potvrdi
        {
            global::Lekar l = (global::Lekar)lekar.SelectedItem;
            Pacijent p = parent.pacijent;
            
            if (time.SelectedIndex != -1)
            {  
                var item = time.SelectedItem;
                String t = item.ToString();
                String d = date.Text;
                DateTime dt = DateTime.Parse(d + " " + t);
                TipTermina tipt;
                if (l.tipLekara.Equals(TipLekara.opstePrakse))
                {
                    tipt = TipTermina.pregledKodLekaraOpstePrakse;
                }
                else if (l.tipLekara.Equals(TipLekara.hirurg))
                {
                    tipt = TipTermina.operacija;
                }
                else {
                    tipt = TipTermina.pregledKodLekaraSpecijaliste;
                }
                int id = TerminFileStorage.GetAll().Count + 1;

                DateTime start;
                DateTime end;
                CalculateStartAndEnd(out start, out end); 

                Prostorija prvaDostupnaProstorija = GetAvailableRoom(start,end);
                Termin termin = new Termin(id, dt, trajanjePregleda, tipt, StatusTermina.zakazan, p, l, prvaDostupnaProstorija);
                TerminFileStorage.AddTermin(termin);
                parent.updateTable();
                this.Close();
                InformacijeOKoriscenjuFunkcionalnosti informacija = new InformacijeOKoriscenjuFunkcionalnosti(DateTime.Now, pacijent.korisnickoIme, VrstaFunkcionalnosti.zakazivanje);
                InformacijeFileStorage.AddInformacije(informacija);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // odustani
        {
            this.Close();
        }

        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();  
        }
       

        private void CheckAvailableTimes() {

            DateTime datum;
            global::Lekar l = (global::Lekar)lekar.SelectedItem;
            if (date.SelectedDate != null)
            {
                datum = DateTime.Parse(date.Text);
            }
            else {
                datum = DateTime.Now;
            }

            availableTimes = new List<string>();
            List<Termin> termini = new List<Termin>();
            if (lekar.SelectedItem != null && date.SelectedDate != null) 
            {
                foreach (Termin termin in TerminFileStorage.GetAll()) 
                {
                    if (l.jmbg == termin.Lekar.jmbg) {
                        if (termin.status == StatusTermina.zakazan && termin.datumZakazivanja.Date.Equals(date.SelectedDate)) {
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

                if (date.SelectedDate == danas) {
                    if (tm < DateTime.Now.AddMinutes(30)) {
                        availableTimes.Remove(tm.ToString("HH:mm"));
                    }
                }
                
            }
            time.ItemsSource = availableTimes;
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

        private void setEnabledButtonSubmit() {
            if (lekar.SelectedItem != null && date.SelectedDate != null && time.SelectedItem != null)
            {
                dugmePotvrdi.IsEnabled = true;
            }
            else {
                dugmePotvrdi.IsEnabled = false;
            }
        }

        private void lekar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
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

        private void UpdateComponents()
        {
            DateTime start;
            DateTime end;

            CalculateStartAndEnd(out start, out end);

            setEnabledButtonSubmit();

            CheckAvailableTimes();

            
            

          
        }

        private void time_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }

        private void button_Click_2(object sender, RoutedEventArgs e) //zakazi
        {
            PacijentZakazujePoPrioritetu pzpp = new PacijentZakazujePoPrioritetu(this.pacijent,parent);
            Application.Current.MainWindow = pzpp;
            pzpp.Show();
            this.Close();
            
           
        }

       

       
    }
}
