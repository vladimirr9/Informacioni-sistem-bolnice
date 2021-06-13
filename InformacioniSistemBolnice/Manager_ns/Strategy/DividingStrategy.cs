using InformacioniSistemBolnice.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Manager_ns.Strategy
{
    public class DividingStrategy : IStrategy
    {
        public AppointmentController AppointmentController => new AppointmentController();

        public RoomController RoomController => new RoomController();

        public void DoRenovate(object firstObject, object scondObject)
        {
            Room room = (Room)firstObject;
            Room room2 = GenerateNewRoom(scondObject, room);
            RoomController.AddRoom(room2);
            RoomController.UpdateRoom(room);
        }

        private Room GenerateNewRoom(object scondObject, Room room)
        {
            Double newRoomArea = (double)scondObject;
            String newRoomName = Parsing(room);
            room.Area -= newRoomArea;
            Room room2 = new Room(newRoomName, room.RoomId + 10, room.RoomType, false, true, newRoomArea, room.Floor, room.RoomNumber, new List<Inventory>());
            return room2;
        }

        private string Parsing(Room room)
        {
            String newRoomName;
            if (room.RoomType.Equals(RoomType.examinationRoom))
            {
                newRoomName = newExaminationRoomName(room);
            }
            else
            {
                newRoomName = NewOtherRoomName(room);
            }

            return newRoomName;
        }

        private static string NewOtherRoomName(Room room)
        {
            string newRoomName;
            string[] splitName = room.Name.Split(' ');
            int parseNumber = Convert.ToInt32(splitName[2]);
            newRoomName = splitName[0] + " " + splitName[1] + " " + (parseNumber + 10).ToString();
            return newRoomName;
        }

        private static string newExaminationRoomName(Room room)
        {
            string newRoomName;
            string[] splitName = room.Name.Split(' ');
            int parseNumber = Convert.ToInt32(splitName[1]);
            newRoomName = splitName[0] + " " + (parseNumber + 10).ToString();
            return newRoomName;
        }
    }
}
