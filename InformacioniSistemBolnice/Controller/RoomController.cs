using InformacioniSistemBolnice.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InformacioniSistemBolnice.Controller
{
    public class RoomController
    {
        private RoomService _roomService = new RoomService();
        private RoomMergeService _roomMergeService = new RoomMergeService();
        private RoomsForHospitalisationService _forHospitalisationService = new RoomsForHospitalisationService();

        public void AddRoom(Room room)
        {
            _roomService.AddRoom(room);
        }

        public void UpdateRoom(Room room)
        {
            _roomService.UpdateRoom(room);
        }

        public void RemoveRoom(Room room)
        {
            _roomService.RemoveRoom(room);
        }

        public Room GetOneRoom(int roomId)
        {
            return _roomService.GetOneRoom(roomId);
        }

        public List<Room> GetAllRooms()
        {
            return _roomService.GetAllRooms();
        }

        public Inventory GetOneInventoryFromSpecifiedRoom(int roomId, String inventoryId)
        {
            return _roomService.GetOneInventoryFromSpecifiedRoom(roomId, inventoryId);
        }

        public void AddInventory(Room room, Inventory inventory)
        {
            _roomService.AddNewInventory(room, inventory);
        }

        public void UpdateInventory(Room room, Inventory inventoryForUpdate, Inventory updatedInventory)
        {
            _roomService.UpdateInventory(room, inventoryForUpdate, updatedInventory);
        }

        public void RemoveInventory(Room room, Inventory inventory)
        {
            _roomService.RemoveInventory(room, inventory);
        }

        public void DinamicInventoryRelocation(Room room, Inventory inventoryForRelocating, int quantity)
        {
            _roomService.DinamicInventoryRelocation(room, inventoryForRelocating, quantity);
        }

        public void FilteredInventory(ItemCollection items, String search)
        {
            _roomService.FilteredInventory(items, search);
        }

        public List<Room> GetAvailableRoomList(DateTime start, DateTime end)
        {
            return _roomService.GetAvailableRoomList(start, end);
        }

        public List<Room> GetFilteredRooms(List<Room> rooms, AppointmentType appointmentType)
        {
            return _roomService.GetFilteredRooms(rooms, appointmentType);
        }

        public List<Room> GetRoomsForHospitalisation(DateTime begin, DateTime end)
        {
            return _forHospitalisationService.GetRoomsForHospitalisation(begin, end);
        }

        public int GetAvailableBed(Room room, DateTime begin, DateTime end)
        {
            return _forHospitalisationService.GetAvailableBed(room, begin, end);
        }
        public List<Room> DisplayRoomsForRelocating(Room room)
        {
            return _roomService.DisplayRoomsForRelocating(room);
        }
        public void StaticInventoryRelocation(Room destinationRoom, Inventory inventoryForRelocating, int quantity, DateTime relocationDate)
        {
            _roomService.StaticInventoryRelocation(destinationRoom, inventoryForRelocating, quantity, relocationDate);
        }
        public void MergingRooms(Room room1, Room room2)
        {
            _roomMergeService.MergingRooms(room1, room2);
        }
        public void DivideRoom(Room room, double newRoomArea)
        {
            _roomMergeService.DivideRoom(room, newRoomArea);
        }
    }
}
