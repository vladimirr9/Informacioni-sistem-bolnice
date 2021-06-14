using InformacioniSistemBolnice.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InformacioniSistemBolnice.Manager_ns.Strategy
{
    public class MergingStrategy : IStrategy
    {
        public AppointmentController AppointmentController => new AppointmentController();

        public RoomController RoomController => new RoomController();

        public AppointmentRoomController AppointmentRoomController = new AppointmentRoomController();
        

        public void DoRenovate(object firstObject, object scondObject)
        {
            Room room1 = (Room)firstObject;
            Room room2 = (Room)scondObject;
            if (room1.RoomType.Equals(room2.RoomType) && room1.Floor == room2.Floor)
            {
                Room newRoom = GenerateNewRoom(room1, room2);
                UpdateOldRooms(room1, room2);
                RoomController.AddRoom(newRoom);
            }
            else
            {
                MessageBox.Show("Ne mozete spajati prostorije koje nisu istog tipa ili na istom spratu!");
            }
        }

        private Room GenerateNewRoom(Room room1, Room room2)
        {
            double newRoomArea = room1.Area + room2.Area;
            List<Inventory> newInventoryList = MergedRoomsInventory(room1, room2);
            Room newRoom = new Room(room1.Name, room1.RoomId + 10, room1.RoomType, false, true, newRoomArea, room1.Floor, room1.RoomNumber + 10, newInventoryList);
            return newRoom;
        }

        private void UpdateOldRooms(Room room1, Room room2)
        {
            RoomController.RemoveRoom(room1);
            RoomController.RemoveRoom(room2);
            AppointmentRoomController.CancelAllRoomAppointments(room1);
            AppointmentRoomController.CancelAllRoomAppointments(room2);
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
                    RoomController.UpdateRoom(room1);
                }
                else
                {
                    room1.InventoryList.Add(inventory);
                    RoomController.UpdateRoom(room1);
                }

            }
            return room1.InventoryList;
        }
    }
}
