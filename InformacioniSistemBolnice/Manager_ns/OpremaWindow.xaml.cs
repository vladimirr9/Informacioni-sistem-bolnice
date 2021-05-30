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
    public partial class OpremaWindow : Window
    {
        private WindowProstorije _parent;
        private Room _selectedRoom;
        private RoomController _roomController = new RoomController();
        public OpremaWindow(Room room, WindowProstorije parent)
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
            DodavanjeOpreme newInventoryWindow = new DodavanjeOpreme(_selectedRoom, this);
            newInventoryWindow.Show();
        }

        private void UpdateInventory(object sender, RoutedEventArgs e)
        {
            if (dataGridInventory.SelectedItem != null)
            {
                Inventory inventoryForUpdate = (Inventory)dataGridInventory.SelectedItem;
                IzmenaOpreme prozor = new IzmenaOpreme(_selectedRoom, inventoryForUpdate, this);
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
                /*if (o.InventoryType == InventoryType.dinamicInv)
                {*/
                    RasporedjivanjeOpreme relocateInventoryWindow = new RasporedjivanjeOpreme(_selectedRoom, inventoryForRelocation, this);
                    relocateInventoryWindow.Show();
                /*}
                else
                {
                    RasporedjivanjeStatickeWindow prozor = new RasporedjivanjeStatickeWindow(_selectedRoom, opremaZaPremestanje, this);
                    prozor.Show();
                }*/
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

        private void Pretraga_TextChanged(object sender, KeyEventArgs e)
        {
            dataGridInventory.Items.Clear();
            List<Inventory> opremaLista = _selectedRoom.InventoryList;
            var filtered = opremaLista.Where(oprema => oprema.Name.StartsWith(Pretraga.Text) || oprema.Name.Contains(Pretraga.Text));

            dataGridInventory.ItemsSource = filtered;
        }
    }
}
