using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Service
{
    class RoomsForHospitalisationService
    {
        public List<Room> GetRoomsForHospitalisation(DateTime begin, DateTime end)
        {
            List<Room> rooms = new List<Room>();
            foreach (Room room in RoomFileRepository.GetAll())
            {
                if (room.RoomType == RoomType.recoveryRoom && room.IsAvailable(begin, end))
                {
                    rooms.Add(room);
                }
            }
            return rooms;
        }
    }
}
