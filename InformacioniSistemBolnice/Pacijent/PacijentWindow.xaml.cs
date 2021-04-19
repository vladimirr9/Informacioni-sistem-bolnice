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
    /// Interaction logic for PacijentWindow.xaml
    /// </summary>
    public partial class PacijentWindow : Window
    {
        public Pacijent pacijent { get; set; }
        public PacijentWindow(Pacijent pacijent)
        {
            this.pacijent = pacijent;
            InitializeComponent();
            this.DataContext = this;
            
            updateTable();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //dodaj
        {
            PacijentZakazuje zakazivanje = new PacijentZakazuje(this);
            Application.Current.MainWindow = zakazivanje;
            zakazivanje.Show();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //izmeni
        {
            Termin termin = TerminFileStorage.GetOne(((Termin)PrikazPregleda.SelectedItem).iDTermina);
            PacijentMijenja m = new PacijentMijenja(termin, this);
            Application.Current.MainWindow = m;
            m.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //ukloni
        {
            if (PrikazPregleda.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da otkažete ovaj termin?", "Potvrda brisanja", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    TerminFileStorage.RemoveTermin(((Termin)(PrikazPregleda.SelectedItem)).iDTermina);
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
                if ((termin.status == StatusTermina.zakazan) && (pacijent.korisnickoIme == termin.pacijent.korisnickoIme))
                    PrikazPregleda.Items.Add(termin);
            }
        }
    }
}
