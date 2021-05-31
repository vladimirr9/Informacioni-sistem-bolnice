using InformacioniSistemBolnice.Controller;
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
        private UpravnikWindow _parent;
        private RoomController _roomController = new RoomController();
        public WindowProstorije(UpravnikWindow parent)
        {
            InitializeComponent();
            this._parent = parent;
            UpdateTable();

            this.DataContext = this;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RoomInventory(object sender, RoutedEventArgs e)
        {
            if (datagridProstorije.SelectedItem != null)
            {
                OpremaWindow inventoryWindow = new OpremaWindow((Room)datagridProstorije.SelectedItem, this);
                inventoryWindow.Show();
            }
        }

        private void UpdateRoom(object sender, RoutedEventArgs e)
        {
            if (datagridProstorije.SelectedItem != null)
            {
                Room roomForUpdate = (Room)datagridProstorije.SelectedItem;
                IzmenaProstorije updateRoomWindow = new IzmenaProstorije(roomForUpdate, this);
                updateRoomWindow.Show();
            }
        }

        private void AddRoom(object sender, RoutedEventArgs e)
        {
            DodavanjeProstorije addRoomWindow = new DodavanjeProstorije(this);
            addRoomWindow.Show();
        }

        private void RemoveRoom(object sender, RoutedEventArgs e)
        {
            if (datagridProstorije.SelectedItem != null)
            {
                MessageBoxResult answer = MessageBox.Show("Da li želite da obrišete selektovanu prostoriju?", "Potvrda brisanja prostorije", MessageBoxButton.YesNo);
                if (answer == MessageBoxResult.Yes)
                {
                    Room selectedRoom = (Room)datagridProstorije.SelectedItem;
                    _roomController.RemoveRoom(selectedRoom);
                    datagridProstorije.Items.Remove(datagridProstorije.SelectedItem);
                    UpdateTable();
                }
            }
        }

        public void UpdateTable()
        {
            datagridProstorije.Items.Clear();
            foreach (Room r in _roomController.GetAllRooms())
            {
                if (!r.IsDeleted)
                    datagridProstorije.Items.Add(r);
            }
        }

        private void Renovation(object sender, RoutedEventArgs e)
        {
            if (datagridProstorije.SelectedItem != null)
            {
                Room selektovana = (Room)datagridProstorije.SelectedItem;
                Room p = RoomFileRepository.GetOne(selektovana.RoomId);
                ZakazivanjeRenoviranjaWindow prozor = new ZakazivanjeRenoviranjaWindow(p, this);
                prozor.Show();
            }
        }

        private void InventorySearch(object sender, RoutedEventArgs e)
        {
            String search = Pretraga.Text;
            if (search != null)
            {
                FiltriranaOprema filteredInventoryWindow = new FiltriranaOprema(this, search);
                filteredInventoryWindow.Show();
            }
        }
    }
}
