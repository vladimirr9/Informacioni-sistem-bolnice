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
    /// Interaction logic for DodavanjeProstorije.xaml
    /// </summary>
    public partial class DodavanjeProstorije : Window
    {
        private WindowProstorije _parent;
        private RoomController _roomController = new RoomController(); 
        public DodavanjeProstorije(WindowProstorije parent)
        {
            InitializeComponent();
            this._parent = parent;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Room room = GeneratingRoomObject();
            _roomController.AddRoom(room);
            _parent.UpdateTable();
            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private Room GeneratingRoomObject()
        {
            String naziv = Naziv.Text;
            int roomId = Convert.ToInt32(IDprostorije.Text);
            RoomType roomType; //= (TipProstorije)TipProstorije.SelectedItem;
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
            List<Inventory> inventorylist = new List<Inventory>();

            Room room = new Room(naziv, roomId, roomType, isDeleted, isActive, area, floor, roomNumber, inventorylist);

            return room;
        }

    }
}
