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

namespace InformacioniSistemBolnice.Sekretar_ns
{
    /// <summary>
    /// Interaction logic for NoviTerminWindow.xaml
    /// </summary>
    public partial class NoviTerminWindow : Window
    {
        private TerminiPage parent;
        public NoviTerminWindow(TerminiPage parent)
        {
            this.parent = parent;
            /*
            List<global::Lekar> lekari = LekarFileStorage.GetAll();
            lekar.ItemsSource = lekari;
            List<Pacijent> pacijenti = PacijentFileStorage.GetAll();
            pacijent.ItemsSource = pacijenti;
            List<Prostorija> prostorije = ProstorijaFileStorage.GetAll();
            prostorija.ItemsSource = prostorije;*/
            InitializeComponent();
            UpdateComponents();
        }

        private void PotvrdiB_Click(object sender, RoutedEventArgs e)
        {
            Pacijent p = (Pacijent) pacijent.SelectedItem;
            global::Lekar l = (global::Lekar)lekar.SelectedItem;
            Prostorija pros = (Prostorija)prostorija.SelectedItem;
            if (time.SelectedIndex != -1)
            {
                ComboBoxItem vremeItem = time.SelectedItem as ComboBoxItem;
                String timeS = vremeItem.Content.ToString();
                String dateS = date.Text;
                DateTime dt = DateTime.Parse(dateS + " " + timeS);
                TipTermina tipTermina = (TipTermina)tip.SelectedIndex;
                int id = TerminFileStorage.GetAll().Count + 1;
                int trajanje = Int32.Parse(Trajanje.Text);
                Termin termin = new Termin(id, dt, trajanje, tipTermina, StatusTermina.zakazan, p, l, pros);
                TerminFileStorage.AddTermin(termin);
                parent.updateTable();
                this.Close();
            }
        }

        private void OdustaniB_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
        private void UpdateComponents() { }/*
        {
            DateTime pocetak;
            DateTime kraj;
            if (time.SelectedItem != null && date.SelectedDate != null && Trajanje.Text != "")
            {
                ComboBoxItem vremeItem = time.SelectedItem as ComboBoxItem;
                String timeS = vremeItem.Content.ToString();
                String dateS = date.Text;
                int trajanje = Int32.Parse(Trajanje.Text);

                pocetak = DateTime.Parse(dateS + " " + timeS);
                kraj = pocetak.AddMinutes(trajanje);
            }
            else
            {
                pocetak = DateTime.Now;
                kraj = DateTime.Now;
            }
            List<global::Lekar> lekari = new List<global::Lekar>();
            List<Pacijent> pacijenti = new List<Pacijent>();
            List<Prostorija> prostorije = new List<Prostorija>();
            
            foreach (global::Lekar tmpLekar in LekarFileStorage.GetAll())
            {
                if (tmpLekar.IsAvailable(pocetak, kraj) && !tmpLekar.isDeleted)
                {
                    lekari.Add(tmpLekar);


                }
            }
            foreach (Pacijent tmpPacijent in PacijentFileStorage.GetAll())
            {
                if (tmpPacijent.IsAvailable(pocetak, kraj) && !tmpPacijent.isDeleted)
                {
                    pacijenti.Add(tmpPacijent);
                }
            }
            foreach (Prostorija tmpProstorija in ProstorijaFileStorage.GetAll())
            {
                if (tmpProstorija.IsAvailable(pocetak, kraj) && !tmpProstorija.IsDeleted)
                {
                    prostorije.Add(tmpProstorija);
                }
            }

            lekar.ItemsSource = lekari;
            pacijent.ItemsSource = pacijenti;
            prostorija.ItemsSource = prostorije;

            Pacijent selektovanPacijent = (Pacijent)pacijent.SelectedItem;
            global::Lekar selektovanLekar = (global::Lekar)lekar.SelectedItem;
            Prostorija selektovanaProstorija = (Prostorija)prostorija.SelectedItem;

            if (selektovanPacijent != null && pacijenti.Contains(selektovanPacijent))
            {
                pacijent.SelectedItem = selektovanPacijent;
            }
            else pacijent.SelectedItem = null;
            if (selektovanLekar != null && lekari.Contains(selektovanLekar))
            {
                lekar.SelectedItem = selektovanLekar;
            }
            else lekar.SelectedItem = null;
            if (selektovanaProstorija != null && prostorije.Contains(selektovanaProstorija))
            {
                prostorija.SelectedItem = selektovanaProstorija;
            }
            else prostorija.SelectedItem = null;
            






    */


        private void lekar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }

        private void tip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }

        private void pacijent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }

        private void time_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }

        private void Trajanje_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateComponents();
        }

        private void prostorija_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }
        private void date_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComponents();
        }
        
    }
}
