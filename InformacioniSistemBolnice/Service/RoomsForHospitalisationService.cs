using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Doctor_ns;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Service
{
    class RoomsForHospitalisationService : IHospitalisation
    {
        public List<Room> GetRoomsForHospitalisation(DateTime begin, DateTime end)
        {
            List<Room> rooms = new List<Room>();
            foreach (Room room in RoomFileRepository.GetAll())
            {
                if (room.RoomType == RoomType.recoveryRoom && GetAvailableBed(room, begin, end) != 0)
                {
                    rooms.Add(room);
                }
            }
            return rooms;
        }

        public int GetAvailableBed(Room room, DateTime begin, DateTime end)
        {
            List<int> beds = GetOccupiedBeds(room, begin, end);
            for (int i = 1; i <= GetBedsNumber(room); i++)
            {
                if (!beds.Contains(i))
                {
                    return i;
                }
            }
            return 0;
        }

        private int GetBedsNumber(Room room)
        {
            foreach (Inventory inventory in room.InventoryList)
            {
                if (inventory.Name == "krevet")
                {
                    return inventory.Quantity;
                }
            }

            return 0;
        }

        private List<int> GetOccupiedBeds(Room room, DateTime begin, DateTime end)
        {
            List<int> beds = new List<int>();
            foreach (Hospitalisation hospitalisation in HospitalisationFileRepository.GetAll())
            {
                if (hospitalisation.RoomId.Equals(room.RoomId) && hospitalisation.EndDate > begin && hospitalisation.BeginDate < end)
                {
                    beds.Add(hospitalisation.Bed);
                }
            }

            return beds;
        }
    }
}
