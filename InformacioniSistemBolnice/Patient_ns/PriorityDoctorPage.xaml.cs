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
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Patient_ns
{
    /// <summary>
    /// Interaction logic for PriorityDoctorPage.xaml
    /// </summary>
    public partial class PriorityDoctorPage : Page
    {
        private const int trajanjePregleda = 15;
        private List<global::Doctor> ljekariLista;
        private List<Termin> termini;
        private List<string> availableTimes;
        private List<Prostorija> prostorije;
        private StartPatientWindow parent;
        private PatientMakesAppointmentByPriorityPage parentp;

        public PriorityDoctorPage(StartPatientWindow pp, PatientMakesAppointmentByPriorityPage pzppp)
        {
            parent = pp;
            parentp = pzppp;
            termini = new List<Termin>();
            InitializeComponent();
            ljekariLista = new List<global::Doctor>();
            foreach (global::Doctor l in LekarFileStorage.GetAll())
            {
                if (l.doctorType == DoctorType.opstePrakse)
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

        private void ljekari_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            date.IsEnabled = true;
            date.IsEnabled = true;
        }

        private void BlackOutDates()
        {
            CalendarDateRange kalendar = new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1));
            date.BlackoutDates.Add(kalendar);

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
            global::Doctor selektovanLjekar = (global::Doctor)ljekari.SelectedItem;
            foreach (Termin t in TerminFileStorage.GetAll())
            {
                if (t.Doctor.jmbg.Equals(selektovanLjekar.jmbg))
                {
                    if (t.datumZakazivanja.Date.Equals(date.SelectedDate) && t.status == StatusTermina.zakazan)
                    {
                        termini.Add(t);
                    }
                }

                if (parent.Pacijent.korisnickoIme == t.Pacijent.korisnickoIme && t.datumZakazivanja.Date.Equals(date.SelectedDate))
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

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            ZakaziTermin();
            ActivityLog informacija = new ActivityLog(DateTime.Now, parent.Pacijent.korisnickoIme, TypeOfActivity.makingAppointment);
            ActivityLogFileRepository.AddActivity(informacija);
        }

        private void ZakaziTermin()
        {
            global::Doctor l = (global::Doctor)ljekari.SelectedItem;
            Pacijent p = parent.Pacijent;


            var item = times.SelectedItem;
            String t = item.ToString();
            String d = date.Text;
            DateTime start = DateTime.Parse(d + " " + t);
            TipTermina tipt;
            if (l.doctorType.Equals(DoctorType.opstePrakse))
            {
                tipt = TipTermina.pregledKodLekaraOpstePrakse;
            }
            else if (l.doctorType.Equals(DoctorType.hirurg))
            {
                tipt = TipTermina.operacija;
            }
            else
            {
                tipt = TipTermina.pregledKodLekaraSpecijaliste;
            }
            int id = TerminFileStorage.GetAll().Count + 1;
            DateTime end = start.AddMinutes(trajanjePregleda);

            Prostorija prvaDostupnaProstorija = GetAvailableRoom(start, end);
            Termin termin = new Termin(id, start, trajanjePregleda, tipt, StatusTermina.zakazan, p, l, prvaDostupnaProstorija);
            TerminFileStorage.AddTermin(termin);
            PatientExaminesAppointmentPage ptp = new PatientExaminesAppointmentPage(parent);
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
