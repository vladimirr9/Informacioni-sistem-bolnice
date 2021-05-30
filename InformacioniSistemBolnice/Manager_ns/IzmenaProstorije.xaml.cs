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
    /// Interaction logic for IzmenaProstorije.xaml
    /// </summary>
    public partial class IzmenaProstorije : Window
    {
        private Room _roomForUpdate;
        private WindowProstorije _parent;
        private RoomController _roomController = new RoomController();
        public IzmenaProstorije(Room room, WindowProstorije parent)
        {
            _roomForUpdate = room;
            InitializeComponent();
            SelectedRoomData();
            this._parent = parent;
        }

        private void UpdateRoom(object sender, RoutedEventArgs e)
        {
            Room newRoom = GenerateRoomObjectFromCollectedData();
            _roomController.UpdateRoom(newRoom);
            _parent.UpdateTable();
            Close();
        }


        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void SelectedRoomData()
        {
            Naziv.Text = _roomForUpdate.Name;
            IDprostorije.Text = _roomForUpdate.RoomId.ToString();
            if (_roomForUpdate.RoomType == 0)
            {
                TipProstorije.SelectedIndex = 0;
            }
            else if (_roomForUpdate.RoomType == (RoomType)1)
            {
                TipProstorije.SelectedIndex = 1;
            }
            else
            {
                TipProstorije.SelectedIndex = 2;
            }
            //IsDeleted.IsChecked = prostorijaZaIzmenu.IsDeleted;
            IsActive.IsChecked = _roomForUpdate.IsActive;
            Kvadratura.Text = _roomForUpdate.Area.ToString();
            BrSprata.Text = _roomForUpdate.Floor.ToString();
            BrSobe.Text = _roomForUpdate.RoomNumber.ToString();
        }

        public Room GenerateRoomObjectFromCollectedData()
        {
            String name = Naziv.Text;
            int roomId = Convert.ToInt32(IDprostorije.Text);
            RoomType roomType; // = (TipProstorije)TipProstorije.SelectedItem;
            if (TipProstorije.SelectedIndex == 0)
            {
                roomType = 0;
            }
            else if (TipProstorije.SelectedIndex == 1)
            {
                roomType = (RoomType)1;
            }
            else
            {
                roomType = (RoomType)2;
            }
            Boolean isDeleted = false;
            Boolean isActive = (Boolean)IsActive.IsChecked;
            Double area = Convert.ToDouble(Kvadratura.Text);
            int floor = Convert.ToInt32(BrSprata.Text);
            int roomNumber = Convert.ToInt32(BrSobe.Text);
            List<Inventory> inventoryList = _roomForUpdate.InventoryList;

            Room room = new Room(name, roomId, roomType, isDeleted, isActive, area, floor, roomNumber, inventoryList);

            return room;
        }
    }
}
