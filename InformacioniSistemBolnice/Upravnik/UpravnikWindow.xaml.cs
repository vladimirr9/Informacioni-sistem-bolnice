using InformacioniSistemBolnice.Upravnik;
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
    /// Interaction logic for UpravnikWindow.xaml
    /// </summary>
    public partial class UpravnikWindow : Window
    {
        public UpravnikWindow()
        {
            InitializeComponent();
            updateTable();

            this.DataContext = this;
            /*String procitano = File.ReadAllText(@"prostorije.json");
            List<Prostorija> listaProstorija = JsonConvert.DeserializeObject<List<Prostorija>>(procitano);
            */
            /*List<Prostorija> prostorije = ProstorijaFileStorage.GetAll();
            foreach(Prostorija prostorija in prostorije)
            {
                datagridProstorije.Items.Add(prostorija);
            }*/
        }

        private void ObrišiProstoriju(object sender, RoutedEventArgs e)
        {
            if (datagridProstorije.SelectedItem != null)
            {
                MessageBoxResult odgovor = MessageBox.Show("Da li želite da obrišete selektovanu prostoriju?", "Potvrda brisanja prostorije", MessageBoxButton.YesNo);
                if (odgovor == MessageBoxResult.Yes)
                {
                    Prostorija selektovana = (Prostorija)datagridProstorije.SelectedItem;
                    ProstorijaFileStorage.RemoveProstorija(selektovana.IDprostorije);
                    datagridProstorije.Items.Remove(datagridProstorije.SelectedItem);
                    updateTable();
                }
            }
        }

        private void DodajProstoriju(object sender, RoutedEventArgs e)
        {
            DodavanjeProstorije prozor = new DodavanjeProstorije(this);
            prozor.Show();
        }

        private void IzmeniProstoriju(object sender, RoutedEventArgs e)
        {
            if (datagridProstorije.SelectedItem != null)
            {
                Prostorija p = (Prostorija)datagridProstorije.SelectedItem;
                //Prostorija prostorijaZaIzmenu = ProstorijaFileStorage.GetOne((Prostorija)datagridProstorije.SelectedItem)).iDprostorije).ToString();
                Prostorija prostorijaZaIzmenu = ProstorijaFileStorage.GetOne(p.IDprostorije);
                IzmenaProstorije prozor = new IzmenaProstorije(prostorijaZaIzmenu, this);
                prozor.Show();
            }
        }

        public void updateTable()
        {
            datagridProstorije.Items.Clear();
            List<Prostorija> prostorije = ProstorijaFileStorage.GetAll();
            foreach (Prostorija p in prostorije)
            {
                if (!p.IsDeleted)
                    datagridProstorije.Items.Add(p);
            }
        }
    }
}
