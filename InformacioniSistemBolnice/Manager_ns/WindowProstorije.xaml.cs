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
                OpremaWindow prozor = new OpremaWindow((Room)datagridProstorije.SelectedItem, this);
                prozor.Show();
            }
        }

        private void IzmeniProstoriju(object sender, RoutedEventArgs e)
        {
            if (datagridProstorije.SelectedItem != null)
            {
                Room p = (Room)datagridProstorije.SelectedItem;
                //Prostorija prostorijaZaIzmenu = ProstorijaFileStorage.GetOne((Prostorija)datagridProstorije.SelectedItem)).iDprostorije).ToString();
                Room prostorijaZaIzmenu = RoomFileRepository.GetOne(p.RoomId);
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
                    Room selektovana = (Room)datagridProstorije.SelectedItem;
                    RoomFileRepository.RemoveRoom(selektovana.RoomId);
                    datagridProstorije.Items.Remove(datagridProstorije.SelectedItem);
                    updateTable();
                }
            }
        }

        public void updateTable()
        {
            datagridProstorije.Items.Clear();
            List<Room> prostorije = RoomFileRepository.GetAll();
            foreach (Room p in prostorije)
            {
                if (!p.IsDeleted)
                    datagridProstorije.Items.Add(p);
            }
        }

        private void ZakaziRenoviranje(object sender, RoutedEventArgs e)
        {
            if (datagridProstorije.SelectedItem != null)
            {
                Room selektovana = (Room)datagridProstorije.SelectedItem;
                Room p = RoomFileRepository.GetOne(selektovana.RoomId);
                ZakazivanjeRenoviranjaWindow prozor = new ZakazivanjeRenoviranjaWindow(p, this);
                prozor.Show();
            }
        }

        private void PretragaOpreme(object sender, RoutedEventArgs e)
        {
            String search = Pretraga.Text;
            if (search != null)
            {
                FiltriranaOprema prozor = new FiltriranaOprema(this, search);
                prozor.Show();
            }
        }
    }
}
