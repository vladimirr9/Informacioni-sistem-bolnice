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

namespace InformacioniSistemBolnice.Lekar
{
    /// <summary>
    /// Interaction logic for LekarDodajTerminWindow.xaml
    /// </summary>
    public partial class LekarDodajTerminWindow : Window
    {
        private LekarWindow parent;
        public LekarDodajTerminWindow(LekarWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            List<global::Lekar> lekari = LekarFileStorage.GetAll();
            lekar.ItemsSource = lekari;
            List<Pacijent> pacijenti = PacijentFileStorage.GetAll();
            pacijent.ItemsSource = pacijenti;
            List<Room> prostorije = RoomFileRepoistory.GetAll();
            prostorija.ItemsSource = prostorije;
        }

        //odustani
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //potvrdi
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Pacijent p = (Pacijent)pacijent.SelectedItem;
            global::Lekar l = (global::Lekar)lekar.SelectedItem;
            Room pr = (Room)prostorija.SelectedItem;
            if (time.SelectedIndex != -1)
            {
                ComboBoxItem item = time.SelectedItem as ComboBoxItem;
                String t = item.Content.ToString();
                String d = date.Text;
                DateTime dt = DateTime.Parse(d + " " + t);
                TipTermina tt = (TipTermina)tip.SelectedIndex;
                int id = TerminFileStorage.GetAll().Count + 1;
                Termin termin = new Termin(id, dt, 15, tt, StatusTermina.zakazan, p, l, pr);
                TerminFileStorage.AddTermin(termin);
                parent.UpdateTable();
                this.Close();
            }
        }
    }
}
