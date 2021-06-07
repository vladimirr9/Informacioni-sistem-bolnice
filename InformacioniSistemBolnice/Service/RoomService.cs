using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            /*if (!IsIdunique(room.RoomId))
            {
                MessageBox.Show("Uneti ID prostorije već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }

            if (!IsNameUnique(room.Name))
            {
                MessageBox.Show("Uneto ime prostorije već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }*/

            RoomFileRepository.AddRoom(room);
        }

        /*public bool IsNameUnique(String name)
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
        }*/

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
            search = search.ToUpper();
            foreach (Room room in GetAllRooms())
            {
                foreach (Inventory inventory in room.InventoryList)
                {
                    if (inventory.Name.ToUpper().Contains(search))
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

        /*public int ParseTodayDate()
        {
            DateTime _today = DateTime.Now;
            string[] todayDate = _today.ToString().Split(' ');
            string[] todayDay = todayDate[0].Split('-');
            string day = todayDay[0];
            int today = int.Parse(day);

            return today;
        }*/
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

        public void MergingRooms(Room room1, Room room2)
        {
            if (room1.RoomType.Equals(room2.RoomType) && room1.Floor == room2.Floor)
            {
                double newRoomArea = room1.Area + room2.Area;
                List<Inventory> newInventoryList = MergedRoomsInventory(room1, room2);
                Room newRoom = new Room(room1.Name, room1.RoomId + 10, room1.RoomType, false, true, newRoomArea, room1.Floor, room1.RoomNumber + 10, newInventoryList);
                RemoveRoom(room1);
                RemoveRoom(room2);
                AddRoom(newRoom);
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
                    UpdateRoom(room1);
                }
                else
                {
                    room1.InventoryList.Add(inventory);
                    UpdateRoom(room1);
                }

            }
            return room1.InventoryList;
        }

        public void DivideRoom(Room room, double newRoomArea)
        {
            String newRoomName = Parsing(room);
            room.Area -= newRoomArea;
            Room room2 = new Room(newRoomName, room.RoomId + 10, room.RoomType, false, true, newRoomArea, room.Floor, room.RoomNumber, new List<Inventory>());
            AddRoom(room2);
            UpdateRoom(room);
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
