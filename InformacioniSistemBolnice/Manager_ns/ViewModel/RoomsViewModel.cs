using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.Upravnik;
using InformacioniSistemBolnice.View.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InformacioniSistemBolnice.Manager_ns.ViewModel
{
    class RoomsViewModel : BindableBase
    {
        private UpravnikWindow _parent;
        private WindowProstorije _thisWindow;
        private RoomController _roomController = new RoomController();
        private ObservableCollection<Room> rooms;
        private List<Room> selectedRooms;
        private Room selectedRoom;
        private String searchBox;
        private String areaBox;
        private MyICommand InventoryCommand { get; set; }
        private MyICommand AddRoomCommand { get; set; }
        private MyICommand EditRoomCommand { get; set; }
        private MyICommand DeletedRoomCommand { get; set; }
        private MyICommand SearchInventoryCommand { get; set; }
        private MyICommand MergeRoomsCommand { get; set; }
        private MyICommand SplitRoomCommand { get; set; }
        private MyICommand RenovationCommand { get; set; }
        private MyICommand CloseWindowCommand { get; set; }

        public RoomsViewModel(UpravnikWindow parent, WindowProstorije parent2)
        {
            _parent = parent;
            _thisWindow = parent2;
            InventoryCommand = new MyICommand(RoomInventory);
            AddRoomCommand = new MyICommand(AddRoom);
            EditRoomCommand = new MyICommand(UpdateRoom);
            DeletedRoomCommand = new MyICommand(RemoveRoom);
            SearchInventoryCommand = new MyICommand(InventorySearch);
            MergeRoomsCommand = new MyICommand(MergeRooms);
            SplitRoomCommand = new MyICommand(SplitRoom);
            RenovationCommand = new MyICommand(Renovation);
            CloseWindowCommand = new MyICommand(Close);
            UpdateTable();
        }

        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                selectedRoom = value;
                OnPropertyChanged("SelectedRoom");
            }
        }

        public List<Room> SelectedRooms
        {
            get { return selectedRooms; }
            set
            {
                selectedRooms = value;
                OnPropertyChanged("SelectedRooms");
            }
        }

        public ObservableCollection<Room> Rooms
        {
            get { return rooms; }
            set
            {
                rooms = value;
                OnPropertyChanged("Rooms");
            }
        }

        public String SearchBox 
        {
            get { return searchBox; }
            set
            {
                searchBox = value;
                OnPropertyChanged("SearchBox");
            }
        }
        public String AreaBox
        {
            get { return areaBox; }
            set
            {
                areaBox = value;
                OnPropertyChanged("AreaBox");
            }
        }

        private void Close()
        {
            this.Close();
        }

        private void RoomInventory()
        {
            if (selectedRoom != null)
            {
                OpremaWindow inventoryWindow = new OpremaWindow(selectedRoom, _thisWindow);
                inventoryWindow.Show();
            }
        }

        private void UpdateRoom()
        {
            if (selectedRoom != null)
            {
                Room roomForUpdate = selectedRoom;
                IzmenaProstorije updateRoomWindow = new IzmenaProstorije(roomForUpdate, _thisWindow);
                updateRoomWindow.Show();
            }
        }

        private void AddRoom()
        {
            DodavanjeProstorije addRoomWindow = new DodavanjeProstorije(_thisWindow);
            addRoomWindow.Show();
        }

        private void RemoveRoom()
        {
            if (selectedRoom != null)
            {
                MessageBoxResult answer = MessageBox.Show("Da li želite da obrišete selektovanu prostoriju?", "Potvrda brisanja prostorije", MessageBoxButton.YesNo);
                if (answer == MessageBoxResult.Yes)
                {
                    Room _selectedRoom = selectedRoom;
                    _roomController.RemoveRoom(_selectedRoom);
                    Rooms.Remove(selectedRoom);
                    UpdateTable();
                }
            }
        }

        public void UpdateTable()
        {
            List<Room> roomsList = new List<Room>();
            foreach (Room r in _roomController.GetAllRooms())
            {
                if (!r.IsDeleted)
                    roomsList.Add(r);
            }
            Rooms = new ObservableCollection<Room>(roomsList);
            OnPropertyChanged("Rooms");
        }

        private void Renovation()
        {
            if (selectedRoom != null)
            {
                Room selektovana = selectedRoom;
                Room p = _roomController.GetOneRoom(selektovana.RoomId);
                ZakazivanjeRenoviranjaWindow prozor = new ZakazivanjeRenoviranjaWindow(p, _thisWindow);
                prozor.Show();
            }
        }

        private void InventorySearch()
        {
            String search = SearchBox;
            if (search != null)
            {
                FiltriranaOprema filteredInventoryWindow = new FiltriranaOprema(_thisWindow, search);
                filteredInventoryWindow.Show();
            }
        }

        private void MergeRooms()
        {
            if (selectedRooms.Count == 2)
            {
                Room room1 = selectedRooms[0];
                Room room2 = selectedRooms[1];
                _roomController.MergingRooms(room1, room2);
                UpdateTable();
            }
        }

        private void SplitRoom()
        {
            if (selectedRoom != null && AreaBox != null)
            {
                double newRoomArea = Convert.ToDouble(AreaBox);
                Room selected = selectedRoom;
                _roomController.DivideRoom(selected, newRoomArea);
                UpdateTable();
            }
        }
    }
}
