﻿using InformacioniSistemBolnice.FileStorage;
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
    /// Interaction logic for PomjeranjeTerminaPage.xaml
    /// </summary>
    public partial class PomjeranjeTerminaPage : Page
    {

        private const int trajanjePregleda = 15;
        private PocetnaPacijent parent;
        private Termin selektovan;
        private List<string> availableTimes;
        private List<Termin> termini;
        private List<global::Doctor> lekari;
        private List<Prostorija> prostorije;
        private int brojac;
        public PomjeranjeTerminaPage(Termin selektovan, PocetnaPacijent prozor)
        {
            this.selektovan = selektovan;
            this.parent = prozor;
            brojac = 0;
            InitializeComponent();
            parent.titleLabel.Content = "Pomeranje termina";
            parent.titleLabel.Visibility = Visibility.Visible;
            availableTimes = new List<string>();
            termini = TerminFileStorage.GetAll();
            time.ItemsSource = availableTimes;
            prostorije = ProstorijaFileStorage.GetAll();
            LoadTimes();
            BlackOutDates();
            date.SelectedDate = selektovan.datumZakazivanja;
            time.SelectedItem = selektovan.datumZakazivanja.ToString("HH:mm");
            lekari = new List<global::Doctor>();
            foreach (global::Doctor l in LekarFileStorage.GetAll())
            {
                if (l.doctorType.Equals(TipLekara.opstePrakse))
                {
                    lekari.Add(l);
                }
            }
            lekar.ItemsSource = lekari;
            foreach (global::Doctor l in lekari)
            {
                if (l.jmbg == selektovan.Doctor.jmbg)
                    lekar.SelectedItem = l;
            }

        }

        private void LoadTimes()
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

        private void BlackOutDates()
        {
            date.SelectedDate = selektovan.datumZakazivanja.Date;
            CalendarDateRange kal = new CalendarDateRange(DateTime.Today, DateTime.Today);
            CalendarDateRange kalendar = new CalendarDateRange(DateTime.MinValue, selektovan.datumZakazivanja.AddDays(-3));
            CalendarDateRange kalendar1 = new CalendarDateRange(selektovan.datumZakazivanja.AddDays(3), DateTime.MaxValue);
            date.BlackoutDates.Add(kalendar);
            date.BlackoutDates.Add(kalendar1);
            date.BlackoutDates.Add(kal);


        }
        private void UpdateComponents()
        {
            DateTime start;
            DateTime end;

            CalculateStartAndEnd(out start, out end);

            setEnabledButtonSubmit();
            Debug.WriteLine(brojac);
            if (brojac > 4)
            {
                CheckAvailableTimes();
            }
            else
            {
                LoadTimes();
            }


            lekari = new List<global::Doctor>();
            foreach (global::Doctor l in LekarFileStorage.GetAll())
            {
                if (l.IsAvailable(start, end) && !l.isDeleted)
                {
                    lekari.Add(l);
                }
            }



        }

        private void setEnabledButtonSubmit()
        {
            if (lekar.SelectedItem != null && date.SelectedDate != null && time.SelectedItem != null)
            {
                submitButton.IsEnabled = true;
            }
            else
            {
                submitButton.IsEnabled = false;
            }
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

                    if (parent.Pacijent.korisnickoIme == termin.Pacijent.korisnickoIme)
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

                /*if (DatePicker.SelectedDate == danas)
                {
                    if (tm < DateTime.Now.AddMinutes(30))
                    {
                        availableTimes.Remove(tm.ToString("HH:mm"));
                    }
                }*/

            }

            time.ItemsSource = availableTimes;
            if (availableTimes.Contains(selektovan.datumZakazivanja.ToString("HH:mm")))
            {
                time.SelectedItem = selektovan.datumZakazivanja.ToString("HH:mm");
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

        private void time_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }

        private void lekar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }

        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ++brojac;
            UpdateComponents();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            parent.startWindow.Content = new PregledTerminaPage(parent);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            updateVisibility();
            parent.startWindow.Content = new PregledTerminaPage(parent);

        }

        private void updateVisibility()
        {
            parent.titleLabel.Visibility = Visibility.Hidden;
            parent.titlePriorityLabel.Visibility = Visibility.Hidden;
            parent.odjava.Visibility = Visibility.Visible;
            parent.imePacijenta.Visibility = Visibility.Visible;
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            global::Doctor l = (global::Doctor)lekar.SelectedItem;
            Pacijent p = parent.Pacijent;

            if (time.SelectedIndex != -1)
            {
                var item = time.SelectedItem;
                String t = item.ToString();
                String d = date.Text;
                DateTime dt = DateTime.Parse(d + " " + t);
                TipTermina tt = TipTermina.pregledKodLekaraOpstePrakse;

                DateTime start;
                DateTime end;
                CalculateStartAndEnd(out start, out end);

                Prostorija prvaDostupnaProstorija = GetAvailableRoom(start, end);

                Termin termin = new Termin(selektovan.iDTermina, dt, trajanjePregleda, tt, StatusTermina.zakazan, p, l, prvaDostupnaProstorija);
                TerminFileStorage.UpdateTermin(selektovan.iDTermina, termin);
                PregledTerminaPage ptp = new PregledTerminaPage(parent);
                updateVisibility();
                parent.startWindow.Content = ptp;
                ptp.updateTable();

                InformacijeOKoriscenjuFunkcionalnosti informacija = new InformacijeOKoriscenjuFunkcionalnosti(DateTime.Now, parent.Pacijent.korisnickoIme, VrstaFunkcionalnosti.pomjeranje);
                InformacijeFileStorage.AddInformacije(informacija);
            }



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
