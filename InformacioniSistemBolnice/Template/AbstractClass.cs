using InformacioniSistemBolnice.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InformacioniSistemBolnice.Template
{
    abstract class AbstractClass
    {
        protected RoomController _roomController;
        protected AppointmentController _appointmentController;
        public virtual void MergeRooms(Room room1, Room room2)
        {
            if (room1.RoomType.Equals(room2.RoomType) && room1.Floor == room2.Floor)
            {
                double newRoomArea = room1.Area + room2.Area;
                List<Inventory> newInventoryList = MergedRoomsInventory(room1, room2);
                Room newRoom = new Room(room1.Name, room1.RoomId + 10, room1.RoomType, false, true, newRoomArea, room1.Floor, room1.RoomNumber + 10, newInventoryList);
                _roomController.RemoveRoom(room1);
                _roomController.RemoveRoom(room2);
                _appointmentController.CancelAllRoomAppointments(room1);
                _appointmentController.CancelAllRoomAppointments(room2);
                _roomController.AddRoom(newRoom);
            }
            else
            {
                MessageBox.Show("Ne mozete spajati prostorije koje nisu istog tipa ili na istom spratu!");
            }
        }

        private List<Inventory> MergedRoomsInventory(Room room1, Room room2)
        {
            foreach (Inventory inventory in room2.InventoryList)
            {
                int index = room1.InventoryList.FindIndex(item => item.InventoryId == inventory.InventoryId);
                if (index >= 0)
                {
                    //mergedInventoryList = MergeInventoryFromRoms(room1, room2, inventory);
                    room1.InventoryList[index].Quantity += inventory.Quantity;
                    _roomController.UpdateRoom(room1);
                }
                else
                {
                    room1.InventoryList.Add(inventory);
                    _roomController.UpdateRoom(room1);
                }

            }
            return room1.InventoryList;
        }

        public virtual void DivideRoom(Room room, double area)
        {
            String newRoomName = Parsing(room);
            room.Area -= newRoomArea;
            Room room2 = new Room(newRoomName, room.RoomId + 10, room.RoomType, false, true, newRoomArea, room.Floor, room.RoomNumber, new List<Inventory>());
            _roomController.AddRoom(room2);
            _roomController.UpdateRoom(room);
        }
    }
}
