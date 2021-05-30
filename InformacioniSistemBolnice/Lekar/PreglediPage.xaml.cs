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
using Microsoft.Win32;

namespace InformacioniSistemBolnice.Lekar
{
    /// <summary>
    /// Interaction logic for PreglediPage.xaml
    /// </summary>
    public partial class PreglediPage : Page
    {
        public LekarWindow parent;
        public global::Lekar lekar;
        private static PreglediPage instance;
        public PreglediPage(LekarWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            UpdateTable();
        }

        public static PreglediPage GetPage(LekarWindow parent)
        {
            if (instance == null)
                instance = new PreglediPage(parent);
            return instance;
        }

        //dodavanje
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LekarDodajTerminWindow dodajWindow = new LekarDodajTerminWindow(parent);
            Application.Current.MainWindow = dodajWindow;
            dodajWindow.Show();
        }

        //izmena
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (PrikazPregleda.SelectedItem != null)
            {
                Termin termin = TerminFileStorage.GetOne(((Termin)PrikazPregleda.SelectedItem).iDTermina);
                LekarIzmeniTerminWindow izmeniWindow = new LekarIzmeniTerminWindow(termin, parent);
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

        public static Termin GetSelected()
        {
            if (instance != null && instance.PrikazPregleda.SelectedItem != null)
            {
                Termin termin = TerminFileStorage.GetOne(((Termin)instance.PrikazPregleda.SelectedItem).iDTermina);
                return termin;
            }
            return null;
        }

    }
}
