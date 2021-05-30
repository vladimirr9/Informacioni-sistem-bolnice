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
            this.Title = lekar.ime + " " + lekar.prezime;
            Main.Content = ProfilPage.GetPage(this);
        }

        private void DodajClick(object sender, RoutedEventArgs e)
        {
            LekarDodajTerminWindow dodajWindow = new LekarDodajTerminWindow(this);
            Application.Current.MainWindow = dodajWindow;
            dodajWindow.Show();
        }

        private void IzmeniClick(object sender, RoutedEventArgs e)
        {
            if (PrikazPregleda.SelectedItem != null)
            {
                Termin termin = TerminFileStorage.GetOne(((Termin)PrikazPregleda.SelectedItem).iDTermina);
                LekarIzmeniTerminWindow izmeniWindow = new LekarIzmeniTerminWindow(termin, this);
                Application.Current.MainWindow = izmeniWindow;
                izmeniWindow.Show();
            }
        }

        private void ObrisiClick(object sender, RoutedEventArgs e)
        {
            if (PrikazPregleda.SelectedItem != null)
            {
                TerminFileStorage.RemoveTermin(((Termin)PrikazPregleda.SelectedItem).iDTermina);
                UpdateTable();
            }
        }

        private void KartonClick(object sender, RoutedEventArgs e)
        {
            if (PreglediPage.GetSelected() != null)
            {
                Termin termin = PreglediPage.GetSelected();
                PrikazKartona kartonWindow = new PrikazKartona(termin, this);
                Application.Current.MainWindow = kartonWindow;
                kartonWindow.Show();
            }
        }

        private void PreglediClick(object sender, RoutedEventArgs e)
        {
            Main.Content = PreglediPage.GetPage(this);
        }

        private void LekoviClick(object sender, RoutedEventArgs e)
        {
            Main.Content = LekoviPage.GetPage(this);
        }

        private void ProfilClick(object sender, RoutedEventArgs e)
        {
            Main.Content = ProfilPage.GetPage(this);
        }

        private void OdjavaClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            this.Close();
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
