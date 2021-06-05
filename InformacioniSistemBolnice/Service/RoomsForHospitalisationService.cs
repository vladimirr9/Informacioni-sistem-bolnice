using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Doctor_ns;
using InformacioniSistemBolnice.FileStorage;

namespace InformacioniSistemBolnice.Service
{
    class RoomsForHospitalisationService
    {
        public List<Room> GetRoomsForHospitalisation(DateTime begin, DateTime end)
        {
            List<Room> rooms = new List<Room>();
            foreach (Room room in RoomFileRepository.GetAll())
            {
                if (room.RoomType == RoomType.recoveryRoom && GetAvailableBed(room, begin, end) <= GetBedsNumber(room))
                {
                    rooms.Add(room);
                }
            }
            return rooms;
        }

        public int GetAvailableBed(Room room, DateTime begin, DateTime end)
        {
            int bed = 0;
            foreach (Hospitalisation hospitalisation in HospitalisationFileRepository.GetAll())
            {
                if (hospitalisation.RoomId.Equals(room.RoomId) && hospitalisation.EndDate > begin && hospitalisation.BeginDate < end)
                {
                    bed += 1;
                }
            }

            return bed + 1;
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
    }
}
