using InformacioniSistemBolnice.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Controller
{
    public class RoomController
    {
        private RoomService _roomService = new RoomService();

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

        public List<Inventory> FilteredInventory(String search)
        {
            return _roomService.FilteredInventory(search);
        }
        public List<Room> GetAvailableRoomList(DateTime start, DateTime end)
        {
            return _roomService.GetAvailableRoomList(start, end);
        }
        public List<Room> GetFilteredRooms(List<Room> rooms, AppointmentType appointmentType)
        {
            return _roomService.GetFilteredRooms(rooms, appointmentType);
        }
    }
}
