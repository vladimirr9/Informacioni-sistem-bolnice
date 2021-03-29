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
    /// Interaction logic for PacijentZakazuje.xaml
    /// </summary>
    public partial class PacijentZakazuje : Window
    {
        private PacijentWindow parent;
        public PacijentZakazuje(PacijentWindow window)
        {
            InitializeComponent();
            this.parent = window;
            List<global::Lekar> lekari = LekarFileStorage.GetAll();
            lekar.ItemsSource = lekari;
            List<Pacijent> pacijenti = PacijentFileStorage.GetAll();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //potvrdi
        {
            global::Lekar l = (global::Lekar)lekar.SelectedItem;
            Pacijent p = parent.pacijent;
            
            if (time.SelectedIndex != -1)
            {  
                ComboBoxItem item = time.SelectedItem as ComboBoxItem;
                String t = item.Content.ToString();
                String d = date.Text;
                DateTime dt = DateTime.Parse(d + " " + t);
                TipTermina tipt = TipTermina.pregledKodLekaraOpstePrakse;
                int id = TerminFileStorage.GetAll().Count + 1;
                Termin termin = new Termin(id, dt, 15, tipt, StatusTermina.zakazan, p, l);
                TerminFileStorage.AddTermin(termin);
                parent.updateTable();
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // odustani
        {
            this.Close();
        }
    }
}
