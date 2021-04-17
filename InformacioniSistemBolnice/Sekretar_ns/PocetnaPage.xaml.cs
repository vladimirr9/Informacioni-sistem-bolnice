using InformacioniSistemBolnice.Korisnik;
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
    /// Interaction logic for PocetnaPage.xaml
    /// </summary>
    public partial class PocetnaPage : Page
    {
        public List<Obavestenje> obavestenja { get; set; }
        private Sekretar tekSekretar;
        private static PocetnaPage instance;
        private PocetnaPage(Sekretar tekSekretar)
        {
            this.tekSekretar = tekSekretar;
            
            InitializeComponent();
            updateTable();
        }

        public static PocetnaPage GetPage(Sekretar tekSekretar)
        {
            if (instance == null)
                instance = new PocetnaPage(tekSekretar);
            return instance;
        }

        public void updateTable()
        {
            PrikazObavestenja.Items.Clear();
            obavestenja = new List<Obavestenje>();
            foreach (Obavestenje o in ObavestenjeFileStorage.GetAll())
            {
                if (o.korisnickoIme == null || o.korisnickoIme.Equals(tekSekretar.korisnickoIme))
                {
                    obavestenja.Add(o);
                }
            }
            PrikazObavestenja.ItemsSource = obavestenja;
        }

        private void Novo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
