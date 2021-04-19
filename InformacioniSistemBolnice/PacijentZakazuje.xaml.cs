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
        private PacijentWindow parent;
        private List<string> lista;
        private List<global::Lekar> lekari;
        private List<Prostorija> prostorije;

        public PacijentZakazuje(PacijentWindow window)
        {
            InitializeComponent();
            this.parent = window;
            Debug.WriteLine("konstruktor");
            lista = new List<string>();
            lista.Add("08:00");
            lista.Add("08:30");
            lista.Add("09:00");
            lista.Add("09:30");
            lista.Add("10:00");
            lista.Add("10:30");
            lista.Add("11:00");
            lista.Add("11:30");
            lista.Add("12:00");
            lista.Add("12:30");
            lista.Add("13:00");
            lista.Add("13:30");
            lista.Add("14:00");
            lista.Add("15:30");
            lista.Add("16:00");
            lista.Add("16:30");
            lista.Add("17:00");
            lista.Add("17:30");
            lista.Add("18:00");
            lista.Add("18:30");
            lista.Add("19:00");
            time.ItemsSource = lista;
            prostorije = ProstorijaFileStorage.GetAll();
            CalendarDateRange kalendar = new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1));
            date.BlackoutDates.Add(kalendar);
            lekari = LekarFileStorage.GetAll();
            lekar.ItemsSource = lekari;
            List<Pacijent> pacijenti = PacijentFileStorage.GetAll();
            lekar.IsEnabled = false;
            time.IsEnabled = false;
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
                TipTermina tipt = TipTermina.pregledKodLekaraOpstePrakse;
                int id = TerminFileStorage.GetAll().Count + 1;
                Termin termin = new Termin(id, dt, 15, tipt, StatusTermina.zakazan, p, l, prostorije.ElementAt(0));
                TerminFileStorage.AddTermin(termin);
                parent.updateTable();
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // odustani
        {
            this.Close();
        }

        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (prioritetCombo.SelectedIndex == 0)
            {
                time.IsEnabled = true;
                global::Lekar l = (global::Lekar)lekar.SelectedItem;

                List<Termin> termini = TerminFileStorage.GetAll();
                foreach (Termin t in termini)
                {
                    if (t.Lekar.jmbg.Equals(l.jmbg))
                    {
                        if ((t.datumZakazivanja.Date == date.SelectedDate) && (t.status == StatusTermina.zakazan))
                        {
                            string sat = t.datumZakazivanja.Hour.ToString();
                            string minute = t.datumZakazivanja.Minute.ToString();
                            string izbaci;
                            int brojac1 = 0;
                            int brojac2 = 0;
                            foreach (char s in sat)
                            {
                                ++brojac1;

                            }
                            foreach (char s in minute)
                            {
                                ++brojac2;
                            }
                            if (brojac1 == 1)
                            {
                                izbaci = "0" + sat + ":" + minute;
                            }
                            else
                            {

                                izbaci = sat + ":" + minute;
                            }

                            if (brojac2 == 1)
                            {
                                izbaci = izbaci + "0";

                            }
                            Debug.WriteLine(izbaci);
                            lista.Remove(izbaci);


                        }


                    }

                }

                time.ItemsSource = lista;


            }
            else
            {
                lekar.IsEnabled = true;
                List<Termin> termini = TerminFileStorage.GetAll();
                foreach (Termin t in termini)
                {
                    string sat = t.datumZakazivanja.Hour.ToString();
                    string minute = t.datumZakazivanja.Minute.ToString();
                    string izbaci;
                    int brojac1 = 0;
                    int brojac2 = 0;
                    foreach (char s in sat)
                    {
                        ++brojac1;

                    }
                    foreach (char s in minute)
                    {
                        ++brojac2;
                    }
                    if (brojac1 == 1)
                    {
                        izbaci = "0" + sat + ":" + minute;
                    }
                    else
                    {

                        izbaci = sat + ":" + minute;
                    }

                    if (brojac2 == 1)
                    {
                        izbaci = izbaci + "0";

                    }

                    if ((t.datumZakazivanja.Date == date.SelectedDate) && (t.status == StatusTermina.zakazan) && (time.SelectedItem.Equals(izbaci)))
                    {
                        {

                            Debug.WriteLine(izbaci);
                            lekari.Remove(t.Lekar);
                            lekar.ItemsSource = lekari;
                            lekar.SelectedIndex = lekari.Count() - 1;



                        }


                    }

                }

                lekar.ItemsSource = lekari;

            }

        }

        private void prioritetCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (prioritetCombo.SelectedIndex == 0)
            {
                lekar.IsEnabled = true;
                lekar.ItemsSource = lekari;
            }
            else
            {
                time.IsEnabled = true;
                date.IsEnabled = false;
                time.ItemsSource = lista;
            }
        }

        private void time_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            date.IsEnabled = true;
        }
    }
}
