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
using System.Windows.Shapes;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for PacijentMijenja.xaml
    /// </summary>
    public partial class PacijentMijenja : Window
    {
        private PacijentWindow parent;
        private Termin selektovan;
        private List<string> lista;
        private List<Termin> termini;
        private List<global::Lekar> lekari;
        public PacijentMijenja(Termin selektovan, PacijentWindow prozor)
        {

            this.selektovan = selektovan;
            this.parent = prozor;

            InitializeComponent();
            lista = new List<string>();
            termini = TerminFileStorage.GetAll();
            lista.Add("");
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

            lekari = LekarFileStorage.GetAll();
            lekar.ItemsSource = lekari;
            List<Pacijent> pacijenti = PacijentFileStorage.GetAll();
            //pacijent.ItemsSource = pacijenti;

            foreach (global::Lekar l in lekari)
            {
                if (l.jmbg == selektovan.Lekar.jmbg)
                    lekar.SelectedItem = l;
            }

            /*foreach (Pacijent p in pacijenti)
            {
                if (p.jmbg != null || p.jmbg == selektovan.pacijent.jmbg)
                    pacijent.SelectedItem = p;
            }*/

            date.SelectedDate = selektovan.datumZakazivanja.Date;
            CalendarDateRange kalendar = new CalendarDateRange(DateTime.MinValue, selektovan.datumZakazivanja.AddDays(-3));
            CalendarDateRange kalendar1 = new CalendarDateRange(selektovan.datumZakazivanja.AddDays(3), DateTime.MaxValue);
            date.BlackoutDates.Add(kalendar);
            date.BlackoutDates.Add(kalendar1);

            time.SelectedItem = selektovan.datumZakazivanja.ToString("HH:mm");

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//potvrda
        {
            global::Lekar l = (global::Lekar)lekar.SelectedItem;
            Pacijent p = parent.pacijent;

            if (time.SelectedIndex != -1)
            {
                var item = time.SelectedItem;
                String t = item.ToString();
                String d = date.Text;
                DateTime dt = DateTime.Parse(d + " " + t);
                TipTermina tt = TipTermina.pregledKodLekaraOpstePrakse;
                Termin termin = new Termin(selektovan.iDTermina, dt, 15, tt, StatusTermina.zakazan, p, l);
                TerminFileStorage.UpdateTermin(selektovan.iDTermina, termin);
                parent.updateTable();
                this.Close();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e) //odustajanje
        {
            this.Close();
        }

        private void button_Click_2(object sender, RoutedEventArgs e) //unesen je datum
        {
            foreach (Termin t in termini)
            {
                if ((t.datumZakazivanja.Date == date.SelectedDate) && (t.status == StatusTermina.zakazan))
                {
                    string sat = t.datumZakazivanja.Hour.ToString();
                    string minute = t.datumZakazivanja.Minute.ToString();
                    string izbaci = "";
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

                    /*lista.Remove(izbaci);*/

                    time.ItemsSource = lista;



                }

                time.SelectedIndex = 0;
            }


        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            foreach (Termin t in termini)
            {

                string sat = t.datumZakazivanja.Hour.ToString();
                string minute = t.datumZakazivanja.Minute.ToString();
                string izbaci = "";
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

                if (t.Lekar.jmbg.Equals(selektovan.lekar.jmbg))
                {
                    if ((t.datumZakazivanja.Date == date.SelectedDate) && (t.status == StatusTermina.zakazan) && (time.SelectedItem.Equals(izbaci)))
                    {
                        lekari.Remove(selektovan.lekar);
                        lekar.ItemsSource = lekari;
                        lekar.SelectedIndex = lekari.Count() - 1;


                    }
                }
            }
        }
    }



}

