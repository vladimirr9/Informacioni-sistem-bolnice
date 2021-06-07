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
    /// Interaction logic for DodavanjeOpreme.xaml
    /// </summary>
    public partial class AddNewInventory : Window
    {
        private InventoryWindow _parent;
        private Room _selectedRoom;
        private RoomController _roomController = new RoomController();
        public AddNewInventory(Room room, InventoryWindow parent)
        {
            InitializeComponent();
            this._parent = parent;
            _selectedRoom = room;

            IdProstorije.Text = _selectedRoom.RoomId.ToString();
        }

        private void AddInventory(object sender, RoutedEventArgs e)
        {
            Inventory newInventory = GenerateInventoryObjectFromCollectedData();
            _roomController.AddInventory(_selectedRoom, newInventory);
            _parent.UpdateTable();
            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public Inventory GenerateInventoryObjectFromCollectedData()
        {
            int roomId = _selectedRoom.RoomId;
            String inventroyId = Sifra.Text;
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
            Inventory newInventory = new Inventory(roomId, inventroyId, name, inventoryType, quantity, isDeleted);

            return newInventory;
        }
    }
}
