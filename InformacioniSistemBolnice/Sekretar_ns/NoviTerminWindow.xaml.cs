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
            InitializeComponent();
            List<global::Lekar> lekari = LekarFileStorage.GetAll();
            lekar.ItemsSource = lekari;
            List<Pacijent> pacijenti = PacijentFileStorage.GetAll();
            pacijent.ItemsSource = pacijenti;
            List<Prostorija> prostorije = ProstorijaFileStorage.GetAll();
            prostorija.ItemsSource = prostorije;
        }

        private void PotvrdiB_Click(object sender, RoutedEventArgs e)
        {
            Pacijent p = (Pacijent) pacijent.SelectedItem;
            global::Lekar l = (global::Lekar)lekar.SelectedItem;
            if (time.SelectedIndex != -1)
            {
                ComboBoxItem vremeItem = time.SelectedItem as ComboBoxItem;
                String timeS = vremeItem.Content.ToString();
                String dateS = date.Text;
                DateTime dt = DateTime.Parse(dateS + " " + timeS);
                TipTermina tipTermina = (TipTermina)tip.SelectedIndex;
                int id = TerminFileStorage.GetAll().Count + 1;
                int trajanje = Int32.Parse(Trajanje.Text);
                Termin termin = new Termin(id, dt, trajanje, tipTermina, StatusTermina.zakazan, p, l);
                TerminFileStorage.AddTermin(termin);
                parent.updateTable();
                this.Close();
            }
        }

        private void OdustaniB_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
