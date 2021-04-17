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
    /// Interaction logic for IzmeniTerminWindow.xaml
    /// </summary>
    public partial class IzmeniTerminWindow : Window
    {
        private TerminiPage parent;
        private Termin selektovan;
        public IzmeniTerminWindow(TerminiPage parent, Termin selektovan)
        {
            this.selektovan = selektovan;
            this.parent = parent;
            InitializeComponent();

            List<global::Lekar> lekari = LekarFileStorage.GetAll();
            lekar.ItemsSource = lekari;
            List<Pacijent> pacijenti = PacijentFileStorage.GetAll();
            pacijent.ItemsSource = pacijenti;
            List<Prostorija> prostorije = ProstorijaFileStorage.GetAll();
            prostorija.ItemsSource = prostorije;

            date.SelectedDate = selektovan.datumZakazivanja;
            time.SelectedValue = selektovan.datumZakazivanja.ToString("HH:mm");

           

            foreach (global::Lekar l in lekari)
            {
                if (l.korisnickoIme.Equals(selektovan.lekar.korisnickoIme))
                    lekar.SelectedItem = l;
            }

            foreach (Pacijent p in pacijenti)
            {
                if (p.korisnickoIme.Equals(selektovan.pacijent.korisnickoIme))
                    pacijent.SelectedItem = p;
            }
            foreach (Prostorija pros in prostorije)
            {
                if (pros.IDprostorije == selektovan.prostorija.IDprostorije)
                {
                    prostorija.SelectedItem = pros;
                }
            }
            Trajanje.Text = selektovan.trajanjeUMinutima.ToString();

            tip.SelectedIndex = (int)selektovan.tipTermina;

        }

        private void PotvrdiB_Click(object sender, RoutedEventArgs e)
        {
            Pacijent p = (Pacijent)pacijent.SelectedItem;
            global::Lekar l = (global::Lekar)lekar.SelectedItem;
            Prostorija pros = (Prostorija)prostorija.SelectedItem;
            if (time.SelectedIndex != -1)
            {
                ComboBoxItem item = time.SelectedItem as ComboBoxItem;
                String t = item.Content.ToString();
                String d = date.Text;
                DateTime dt = DateTime.Parse(d + " " + t);
                TipTermina tt = (TipTermina)tip.SelectedIndex;
                Termin termin = new Termin(selektovan.iDTermina, dt, 15, tt, StatusTermina.zakazan, p, l, pros);
                TerminFileStorage.UpdateTermin(selektovan.iDTermina, termin);
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
