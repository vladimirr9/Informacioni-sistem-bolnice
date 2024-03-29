﻿using InformacioniSistemBolnice.Controller;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Service
{
    public class RoomService
    {
        private IRoomRepository _roomRepository = new RoomFileRepository();
        public void AddRoom(Room room)
        {
            _roomRepository.Add(room);
        }

        public void UpdateRoom(Room room)
        {
            _roomRepository.Update(room.RoomId, room);
        }

        public void RemoveRoom(Room room)
        {
            _roomRepository.Remove(room.RoomId);
        }

        public Room GetOneRoom(int roomId)
        {
            return _roomRepository.GetOne(roomId);
        }

        public List<Room> GetAllRooms()
        {
            return _roomRepository.GetAll();
        }

        public Inventory GetOneInventoryFromSpecifiedRoom(int roomId, String inventoryId)
        {
            List<Inventory> _inventories = _roomRepository.GetOne(roomId).InventoryList;
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
            UpdateRoom(_roomRepository.GetOne(inventoryForRelocating.RoomId));
        }

        private static void IsEnoughInventory(Inventory inventoryForRelocating, int quantity)
        {
            if (inventoryForRelocating.Quantity < quantity)
                MessageBox.Show("Ne možete prebaciti više opreme nego što ima u prostoriji", "Nema dovoljno opreme!", MessageBoxButton.OK);
            return;
        }

        public void FilteredInventory(ItemCollection items, String search)
        {
            items.Clear();
            search = search.ToUpper();
            foreach (Room room in GetAllRooms())
            {
                foreach (Inventory inventory in room.InventoryList)
                {
                    if (inventory.Name.ToUpper().Contains(search))
                    {
                        items.Add(inventory);
                    }
                }
            }
        }
        public List<Room> GetAvailableRoomList(DateTime start, DateTime end)
        {
            List<Room> rooms = new List<Room>();
            foreach (Room room in _roomRepository.GetAll())
            {
                if (room.IsAvailable(start, end) && !room.IsDeleted && room.RoomType != RoomType.stockroom)
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
        public List<Room> DisplayRoomsForRelocating(Room room)
        {
            List<Room> rooms = new List<Room>();
            foreach (Room r in GetAllRooms())
            {
                if (!room.Name.Equals(r.Name))
                {
                    rooms.Add(r);
                }
            }
            return rooms;
        }

        public void StaticInventoryRelocation(Room destinationRoom, Inventory inventoryForRelocating, int quantity, DateTime relocationDate)
        {
            IsEnoughInventory(inventoryForRelocating, quantity);
            foreach (Inventory inventory in destinationRoom.InventoryList)
            {
                if (inventory.InventoryId.Equals(inventoryForRelocating.InventoryId))
                {
                    StopwatchDuration(ParsePickedDate(relocationDate) - ParseTodayDate());
                    inventory.Quantity += quantity;
                    inventoryForRelocating.Quantity -= quantity;
                }
            }
            UpdateRoom(destinationRoom);
            UpdateRoom(GetOneRoom(inventoryForRelocating.RoomId));
        }

        public int ParseTodayDate()
        {
            DateTime _today = DateTime.Now;
            string[] todayDate = _today.ToString().Split(' ');
            string[] todayDay = todayDate[0].Split('/');
            string day = todayDay[1];
            int today = int.Parse(day);

            return today;
        }

        public int ParsePickedDate(DateTime date)
        {
            string[] dateParse = date.ToString().Split(' ');
            string[] dateDayParse = dateParse[0].Split('/');
            string parsedDay = dateDayParse[1];
            int pickedDay = int.Parse(parsedDay);

            return pickedDay;
        }

        public void StopwatchDuration(int duration)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            MessageBox.Show("Oprema ce biti premestena za " + duration + " " + "dan/a");
            while (true)
            {
                if (stopwatch.ElapsedMilliseconds >= duration * 10000)
                {
                    break;
                }
            }
        }

    }
}
