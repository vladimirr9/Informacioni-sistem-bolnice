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
    /// Interaction logic for IzmenaOpreme.xaml
    /// </summary>
    public partial class EditInventory : Window
    {
        private InventoryWindow _parent;
        private Inventory _inventoryForUpdate;
        private Room _selectedRoom;
        private RoomController _roomController = new RoomController();
        public EditInventory(Room room, Inventory inventory, InventoryWindow parent)
        {
            InitializeComponent();
            this._parent = parent;
            _inventoryForUpdate = inventory;
            _selectedRoom = room;
            SelectedInventoryData();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateInventory(object sender, RoutedEventArgs e)
        {
            Inventory newInventory = GeneratedInventoryObjectFromCollectedData();
            _roomController.UpdateInventory(_selectedRoom, _inventoryForUpdate, newInventory);
            _parent.UpdateTable();
            this.Close();
        }

        public Inventory GeneratedInventoryObjectFromCollectedData()
        {
            int roomId = _selectedRoom.GetOne(_inventoryForUpdate.InventoryId).RoomId;
            String inventoryid = Sifra.Text;
            String name = Naziv.Text;
            InventoryType inventoryType;
            if (TipOpreme.SelectedIndex == 0)
            {
                inventoryType = 0;
            }
            else
            {
                inventoryType = (InventoryType)1;
            }
            int quantity = Convert.ToInt32(Kolicina.Text);
            Boolean isDeleted = (bool)IsDeleted.IsChecked;

            Inventory updatedInventory = new Inventory(roomId, inventoryid, name, inventoryType, quantity, isDeleted);
            return updatedInventory;
        }

        public void SelectedInventoryData()
        {
            IdProstorije.Text = _selectedRoom.GetOne(_inventoryForUpdate.InventoryId).RoomId.ToString();
            Sifra.Text = _inventoryForUpdate.InventoryId;
            Naziv.Text = _inventoryForUpdate.Name;
            if (_inventoryForUpdate.InventoryType == 0)
            {
                TipOpreme.SelectedIndex = 0;
            }
            else
            {
                TipOpreme.SelectedIndex = 1;
            }
            Kolicina.Text = _inventoryForUpdate.Quantity.ToString();
            IsDeleted.IsChecked = _inventoryForUpdate.IsDeleted;
        }
    }
}
