using InformacioniSistemBolnice.Lekar;
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

namespace InformacioniSistemBolnice.Sekretar_ns
{
    /// <summary>
    /// Interaction logic for TerminiPage.xaml
    /// </summary>
    public partial class TerminiPage : Page
    {
        private static TerminiPage instance;
        public TerminiPage()
        {
            InitializeComponent();
            updateTable();
        }
        public static TerminiPage GetPage()
        {
            if (instance == null)
                instance = new TerminiPage();
            return instance;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            NoviTerminWindow window = new NoviTerminWindow(this);
            window.Show();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (PrikazPregleda.SelectedItem != null)
            {
                Termin inicijalniTermin = TerminFileStorage.GetOne(((Termin)(PrikazPregleda.SelectedItem)).iDTermina);
                IzmeniTerminWindow window = new IzmeniTerminWindow(this, inicijalniTermin);
                window.Show();
            }
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            if (PrikazPregleda.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da obrišete ovaj termin?", "Potvrda brisanja", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    TerminFileStorage.RemoveTermin(((Termin)PrikazPregleda.SelectedItem).iDTermina);
                    updateTable();
                }
            }
        }
        public void updateTable()
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
