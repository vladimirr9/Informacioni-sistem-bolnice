using System;
using System.Collections.Generic;
using System.Globalization;
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
using InformacioniSistemBolnice.Lekar;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for LekarWindow.xaml
    /// </summary>
    public partial class LekarWindow : Window
    {
        public global::Lekar lekar;
        public LekarWindow(global::Lekar lekar)
        {
            this.lekar = lekar;
            InitializeComponent();
            UpdateTable();
        }

        //dodavanje
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LekarDodajTerminWindow dodajWindow = new LekarDodajTerminWindow(this);
            Application.Current.MainWindow = dodajWindow;
            dodajWindow.Show();
        }

        //izmena
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (PrikazPregleda.SelectedItem != null)
            {
                Termin termin = TerminFileStorage.GetOne(((Termin)PrikazPregleda.SelectedItem).iDTermina);
                LekarIzmeniTerminWindow izmeniWindow = new LekarIzmeniTerminWindow(termin, this);
                Application.Current.MainWindow = izmeniWindow;
                izmeniWindow.Show();
            }
        }

        //brisanje
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (PrikazPregleda.SelectedItem != null)
            {
                TerminFileStorage.RemoveTermin(((Termin)PrikazPregleda.SelectedItem).iDTermina);
                UpdateTable();
            }
        }

        //pacijenti
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (PrikazPregleda.SelectedItem != null)
            {
                Termin termin = TerminFileStorage.GetOne(((Termin)PrikazPregleda.SelectedItem).iDTermina);
                PrikazKartona kartoWindow = new PrikazKartona(termin, this);
                Application.Current.MainWindow = kartoWindow;
                kartoWindow.Show();
            }
        }

        //pregledi
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        public void UpdateTable()
        {
            PrikazPregleda.Items.Clear();
            List<Termin> termini = TerminFileStorage.GetAll();
            foreach (Termin termin in termini)
            {
                if (termin.status == StatusTermina.zakazan)
                    PrikazPregleda.Items.Add(termin);
            }
        }

    }
}
