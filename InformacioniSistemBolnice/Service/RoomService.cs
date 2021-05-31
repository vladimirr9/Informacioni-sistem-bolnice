using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InformacioniSistemBolnice.Service
{
    public class RoomService
    {
        public void AddRoom(Room room)
        {
            if (!IsIdunique(room.RoomId))
            {
                MessageBox.Show("Uneti ID prostorije već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }

            if (!IsNameUnique(room.Name))
            {
                MessageBox.Show("Uneto ime prostorije već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }

            RoomFileRepository.AddRoom(room);
        }

        public bool IsNameUnique(String name)
        {
            if (RoomFileRepository.GetOneByName(name) == null)
            {
                return true;
            }
            else { return false; }
        }

        public bool IsIdunique(int roomId)
        {
            if (RoomFileRepository.GetOne(roomId) == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateRoom(Room room)
        {
            RoomFileRepository.UpdateRoom(room.RoomId, room);
        }

        public void RemoveRoom(Room room)
        {
            RoomFileRepository.RemoveRoom(room.RoomId);
        }

        public Room GetOneRoom(int roomId)
        {
            return RoomFileRepository.GetOne(roomId);
        }

        public List<Room> GetAllRooms()
        {
            return RoomFileRepository.GetAll();
        }

        public Inventory GetOneInventoryFromSpecifiedRoom(int roomId, String inventoryId)
        {
            List<Inventory> _inventories = RoomFileRepository.GetOne(roomId).InventoryList;
            foreach (Inventory inventory in _inventories)
            {
                if (inventory.InventoryId.Equals(inventoryId))
                {
                    return _inventories[_inventories.IndexOf(inventory)];
                }
            }
            return null;
        }

        public void AddNewInventory(Room room, Inventory newInventory)
        {
            room.InventoryList.Add(newInventory);
            UpdateRoom(room);
        }

        public void UpdateInventory(Room room, Inventory inventoryForUpdate, Inventory updatedInventory)
        {
            room.InventoryList[room.InventoryList.IndexOf(inventoryForUpdate)] = updatedInventory;
            UpdateRoom(room);
        }

        public void RemoveInventory(Room room, Inventory inventoryForRemove)
        {
            room.InventoryList.Remove(inventoryForRemove);
            UpdateRoom(room);
        }

        public void DinamicInventoryRelocation(Room destinationRoom, Inventory inventoryForRelocating, int quantity)
        {
            IsEnoughInventory(inventoryForRelocating, quantity);
            foreach (Inventory inventory in destinationRoom.InventoryList)
            {
                if (inventory.InventoryId.Equals(inventoryForRelocating.InventoryId))
                {
                    inventory.Quantity += quantity;
                    inventoryForRelocating.Quantity -= quantity;
                }
            }
            UpdateRoom(destinationRoom);
            UpdateRoom(RoomFileRepository.GetOne(inventoryForRelocating.RoomId));
        }

        private static void IsEnoughInventory(Inventory inventoryForRelocating, int quantity)
        {
            if (inventoryForRelocating.Quantity < quantity)
                MessageBox.Show("Ne možete prebaciti više opreme nego što ima u prostoriji", "Nema dovoljno opreme!", MessageBoxButton.OK);
            return;
        }

        public List<Inventory> FilteredInventory(String search)
        {
            List<Inventory> inventoryList = new List<Inventory>();
            foreach (Room room in GetAllRooms())
            {
                foreach (Inventory inventory in room.InventoryList)
                {
                    if (inventory.Name.Contains(search))
                    {
                        inventoryList.Add(inventory);
                    }
                }
            }
            return inventoryList;
        }
        public List<Room> GetAvailableRoomList(DateTime start, DateTime end)
        {
            List<Room> rooms = new List<Room>();
            foreach (Room room in RoomFileRepository.GetAll())
            {
                if (room.IsAvailable(start, end) && !room.IsDeleted)
                {
                    rooms.Add(room);
                }
            }
            return rooms;
        }

        public List<Room> GetFilteredRooms(List<Room> rooms, AppointmentType appointmentType)
        {
            List<Room> filteredRooms = new List<Room>();
            if (appointmentType == AppointmentType.operation)
            {
                foreach (Room room in rooms)
                {
                    if (room.RoomType == RoomType.operatingRoom)
                        filteredRooms.Add(room);
                }
            }
            else
            {
                foreach (Room room in rooms)
                {
                    if (room.RoomType == RoomType.examinationRoom)
                        filteredRooms.Add(room);
                }
            }
            return filteredRooms;
        }
    }
}
