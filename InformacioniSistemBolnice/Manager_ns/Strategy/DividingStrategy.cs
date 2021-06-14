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
        public AppointmentRoomController AppointmentRoomController => new AppointmentRoomController();

        public RoomController RoomController => new RoomController();

        public void DoRenovation(object firstObject, object scondObject)
        {
            //String newRoomName = Parsing(room);
            Room room = (Room)firstObject;
            Double newRoomArea = (double)scondObject;
            UpdateOldRoom(room, newRoomArea);
            Room room2 = new Room(Parsing(room), room.RoomId + 10, room.RoomType, false, true, newRoomArea, room.Floor, room.RoomNumber, new List<Inventory>());
            RoomController.AddRoom(room2);
        }

        private void UpdateOldRoom(Room room, double newRoomArea)
        {
            room.Area -= newRoomArea;
            RoomController.UpdateRoom(room);
            AppointmentRoomController.CancelAllRoomAppointments(room);
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


/*Double newRoomArea = (double)scondObject;
room.Area -= newRoomArea;
RoomController.UpdateRoom(room);
AppointmentRoomController.CancelAllRoomAppointments(room);
//Room room2 = GenerateNewRoom(scondObject, room);*/


/*private Room GenerateNewRoom(object scondObject, Room room)
{
    Double newRoomArea = (double)scondObject;
    String newRoomName = Parsing(room);
    room.Area -= newRoomArea;
    RoomController.UpdateRoom(room);
    AppointmentRoomController.CancelAllRoomAppointments(room);
    Room room2 = new Room(newRoomName, room.RoomId + 10, room.RoomType, false, true, newRoomArea, room.Floor, room.RoomNumber, new List<Inventory>());
    return room2;
}*/
