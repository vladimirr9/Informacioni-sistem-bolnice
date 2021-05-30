using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InformacioniSistemBolnice.Service
{
    class RoomService
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
            RoomFileRepository.UpdateRoom(room.RoomId, room);
        }

        public void RemoveRoom(Room room)
        {
            RoomFileRepository.RemoveRoom(room.RoomId);
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

        public void AddRenovatoinPeriod(RenovationPeriod newRenovationPeriod)
        {
            RenovationperiodFileRepository.AddRenovationPeriod(newRenovationPeriod);
        }

        public void AddNewInventory(Inventory newInventory)
        {
            //List<Room> rooms = RoomFileRepository.GetAll();
            foreach(Room room in RoomFileRepository.GetAll())
            {
                if(newInventory.RoomId == room.RoomId)
                {
                    room.InventoryList.Add(newInventory);
                    UpdateRoom(room);
                }
            }
        }

        public void UpdateInventory(Inventory inventoryForUpdate, Inventory updatedInventory)
        {
            foreach (Room room in RoomFileRepository.GetAll())
            {
                if (inventoryForUpdate.RoomId == room.RoomId)
                {
                    room.InventoryList[room.InventoryList.IndexOf(inventoryForUpdate)] = updatedInventory;
                    UpdateRoom(room);
                }
            }
        }

        public void RemoveInventory(Inventory inventoryForRemove)
        {
            foreach (Room room in RoomFileRepository.GetAll())
            {
                if (inventoryForRemove.RoomId == room.RoomId)
                {
                    room.InventoryList.Remove(inventoryForRemove);
                    UpdateRoom(room);
                }
            }
        }

        public void DinamicInventoryRelocation(Room roomWhere, Inventory inventoryForRelocating, int quantity)
        {
            IsEnoughInventory(inventoryForRelocating, quantity);
            if(inventoryForRelocating.RoomId == roomWhere.RoomId)
            {
                roomWhere.InventoryList[roomWhere.InventoryList.IndexOf(inventoryForRelocating)].Quantity += quantity;
                inventoryForRelocating.Quantity -= quantity;
            }
            UpdateRoom(roomWhere);
            UpdateRoom(RoomFileRepository.GetOne(inventoryForRelocating.RoomId));
        }

        private static void IsEnoughInventory(Inventory inventoryForRelocating, int quantity)
        {
            if (inventoryForRelocating.Quantity < quantity)
                MessageBox.Show("Ne možete prebaciti više opreme nego što ima u prostoriji", "Nema dovoljno opreme!", MessageBoxButton.OK);
            return;
        }
    }
}
