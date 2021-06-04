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
    /// Interaction logic for OpremaWindow.xaml
    /// </summary>
    public partial class InventoryWindow : Window
    {
        private WindowProstorije _parent;
        private Room _selectedRoom;
        private RoomController _roomController = new RoomController();
        public InventoryWindow(Room room, WindowProstorije parent)
        {
            this._parent = parent;
            this._selectedRoom = room;
            InitializeComponent();
            this.DataContext = this;
            if (room.InventoryList != null)
            {
                UpdateTable();
            }
            UpdateTable();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddInventory(object sender, RoutedEventArgs e)
        {
            AddNewInventory newInventoryWindow = new AddNewInventory(_selectedRoom, this);
            newInventoryWindow.Show();
        }

        private void UpdateInventory(object sender, RoutedEventArgs e)
        {
            if (dataGridInventory.SelectedItem != null)
            {
                Inventory inventoryForUpdate = (Inventory)dataGridInventory.SelectedItem;
                EditInventory prozor = new EditInventory(_selectedRoom, inventoryForUpdate, this);
                prozor.Show();
            }
        }

        private void RemoveInventory(object sender, RoutedEventArgs e)
        {
            if (dataGridInventory.SelectedItem != null)
            {
                MessageBoxResult odgovor = MessageBox.Show("Da li želite da obrišete selektovanu opremu?", "Potvrda brisanja opreme", MessageBoxButton.YesNo);
                if (odgovor == MessageBoxResult.Yes)
                {
                    Inventory selectedInventory = (Inventory)dataGridInventory.SelectedItem;
                    dataGridInventory.Items.Remove(dataGridInventory.SelectedItem);
                    _roomController.RemoveInventory(_selectedRoom, selectedInventory);
                    UpdateTable();
                }
            }
        }

        private void RelocateInventory(object sender, RoutedEventArgs e)
        {
            if (dataGridInventory.SelectedItem != null)
            {
                Inventory inventoryForRelocation = (Inventory)dataGridInventory.SelectedItem;
                if (inventoryForRelocation.InventoryType == InventoryType.dinamicInv)
                {
                    RasporedjivanjeOpreme relocateInventoryWindow = new RasporedjivanjeOpreme(_selectedRoom, inventoryForRelocation, this);
                    relocateInventoryWindow.Show();
                }
                else
                {
                    RelocateStaticInventoryWindow window = new RelocateStaticInventoryWindow(_selectedRoom, inventoryForRelocation, this);
                    window.Show();
                }
            }
        }

        public void UpdateTable()
        {
            dataGridInventory.Items.Clear();
            foreach (Inventory i in _selectedRoom.InventoryList)
            {
                if (!i.IsDeleted)
                    dataGridInventory.Items.Add(i);
            }
        }
        public void Search()
        {
            String searchText = searchBox.Text;
            if(searchText == "")
            {
                ResetTable();
            }
            else
            {
                FilterTable(searchText);
            }
        }

        private void FilterTable(String filter)
        {
            dataGridInventory.Items.Clear();
            filter = filter.ToUpper();

            foreach(Inventory inventory in _selectedRoom.InventoryList)
            {
                if (inventory.Name.ToUpper().Contains(filter))
                {
                    dataGridInventory.Items.Add(inventory);
                }
            }
        }

        private void ResetTable()
        {
            dataGridInventory.Items.Clear();
            foreach(Inventory inventory in _selectedRoom.InventoryList)
            {
                dataGridInventory.Items.Add(inventory);
            }
        }

        private void Pretraga_TextChanged(object sender, KeyEventArgs e)
        {
            Search();
        }


        /*dataGridInventory.Items.Clear();
        List<Inventory> opremaLista = _selectedRoom.InventoryList;
        var filtered = opremaLista.Where(oprema => oprema.Name.StartsWith(Pretraga.Text) || oprema.Name.Contains(Pretraga.Text));

        dataGridInventory.ItemsSource = filtered;*/
    }
}
