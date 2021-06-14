using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InformacioniSistemBolnice.Service
{
    class RoomMergeService
    {
        private AppointmentService _appointmentService = new AppointmentService();
        private RoomService _roomService = new RoomService();
        private AppointmentRoomService _appointmentRoomService = new AppointmentRoomService();
        public void MergingRooms(Room room1, Room room2)
        {
            if (room1.RoomType.Equals(room2.RoomType) && room1.Floor == room2.Floor)
            {
                double newRoomArea = room1.Area + room2.Area;
                List<Inventory> newInventoryList = MergedRoomsInventory(room1, room2);
                Room newRoom = new Room(room1.Name, room1.RoomId + 10, room1.RoomType, false, true, newRoomArea, room1.Floor, room1.RoomNumber + 10, newInventoryList);
                _roomService.RemoveRoom(room1);
                _roomService.RemoveRoom(room2);
                _appointmentRoomService.CancelAllRoomAppointments(room1);
                _appointmentRoomService.CancelAllRoomAppointments(room2);
                _roomService.AddRoom(newRoom);
            }
            else
            {
                MessageBox.Show("Ne mozete spajati prostorije koje nisu istog tipa ili na istom spratu!");
            }
        }

        public List<Inventory> MergedRoomsInventory(Room room1, Room room2)
        {
            foreach (Inventory inventory in room2.InventoryList)
            {
                int index = room1.InventoryList.FindIndex(item => item.InventoryId == inventory.InventoryId);
                if (index >= 0)
                {
                    //mergedInventoryList = MergeInventoryFromRoms(room1, room2, inventory);
                    room1.InventoryList[index].Quantity += inventory.Quantity;
                    _roomService.UpdateRoom(room1);
                }
                else
                {
                    room1.InventoryList.Add(inventory);
                    _roomService.UpdateRoom(room1);
                }

            }
            return room1.InventoryList;
        }

        public void DivideRoom(Room room, double newRoomArea)
        {
            String newRoomName = Parsing(room);
            room.Area -= newRoomArea;
            Room room2 = new Room(newRoomName, room.RoomId + 10, room.RoomType, false, true, newRoomArea, room.Floor, room.RoomNumber, new List<Inventory>());
            _roomService.AddRoom(room2);
            _roomService.UpdateRoom(room);
        }

        public String Parsing(Room room)
        {
            String newRoomName;
            if (room.RoomType.Equals(RoomType.examinationRoom))
            {
                string[] splitName = room.Name.Split(' ');
                int parseNumber = Convert.ToInt32(splitName[1]);
                newRoomName = splitName[0] + " " + (parseNumber + 10).ToString();
            }
            else
            {
                string[] splitName = room.Name.Split(' ');
                int parseNumber = Convert.ToInt32(splitName[2]);
                newRoomName = splitName[0] + " " + splitName[1] + " " + (parseNumber + 10).ToString();
            }

            return newRoomName;
        }
    }
}
