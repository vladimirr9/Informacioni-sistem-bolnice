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

namespace InformacioniSistemBolnice.Upravnik
{
    /// <summary>
    /// Interaction logic for WindowProstorije.xaml
    /// </summary>
    public partial class WindowProstorije : Window
    {
        public UpravnikWindow parent;
        public WindowProstorije(UpravnikWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            updateTable();

            this.DataContext = this;
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OpremaProstorije(object sender, RoutedEventArgs e)
        {
            if (datagridProstorije.SelectedItem != null)
            {
                OpremaWindow prozor = new OpremaWindow((Prostorija)datagridProstorije.SelectedItem, this);
                prozor.Show();
            }
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

        private void DodajProstoriju(object sender, RoutedEventArgs e)
        {
            DodavanjeProstorije prozor = new DodavanjeProstorije(this);
            prozor.Show();
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

        private void ZakaziRenoviranje(object sender, RoutedEventArgs e)
        {
            if (datagridProstorije.SelectedItem != null)
            {
                Prostorija selektovana = (Prostorija)datagridProstorije.SelectedItem;
                Prostorija p = ProstorijaFileStorage.GetOne(selektovana.IDprostorije);
                ZakazivanjeRenoviranjaWindow prozor = new ZakazivanjeRenoviranjaWindow(p, this);
                prozor.Show();
            }
        }
    }
}
