// File:    ProstorijaFileStorage.cs
// Author:  User
// Created: Monday, March 22, 2021 7:51:09 PM
// Purpose: Definition of Class ProstorijaFileStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class RoomFileRepository
{
    private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "rooms.json";

    public static List<Room> GetAll()
   {
        if (!File.Exists(startupPath))
        {
            var tmp = File.OpenWrite(startupPath);
            tmp.Close();
        }
        List<Room> rooms;
        String read = File.ReadAllText(startupPath);
        if (read.Equals(""))
        {
            rooms = new List<Room>();
        }
        else
        {
            rooms = JsonConvert.DeserializeObject<List<Room>>(read);
        }
        return rooms;
    }
   
   public static Room GetOne(int roomId)
   {
        List<Room> rooms = GetAll();
        foreach (Room room in rooms)
        {
            if (room.RoomId == roomId)
            {
                return rooms[rooms.IndexOf(room)];
            }
        }
        return null;
    }

    public static Boolean RemoveRoom(int roomId)
   {
        List<Room> rooms = GetAll();
        foreach (Room room in rooms)
        {
            if (room.RoomId == roomId)
            {
                rooms[rooms.IndexOf(room)].IsDeleted = true;
                rooms[rooms.IndexOf(room)].IsActive = false;
                Save(rooms);
                return true;
            }
        }
        return false;
    }

    private static void Save(List<Room> rooms)
    {
        string write = JsonConvert.SerializeObject(rooms);
        File.WriteAllText(startupPath, write);
    }

    public static Boolean AddRoom(Room newRoom)
   {
        List<Room> rooms = GetAll();
        rooms.Add(newRoom);
        Save(rooms);
        return true;
    }
   
   public static Boolean UpdateRoom(int roomId, Room newRoom)
   {
        List<Room> rooms = GetAll();
        foreach (Room room in rooms)
        {
            if (room.RoomId == roomId)
            {
                rooms[rooms.IndexOf(room)] = newRoom;
                Save(rooms);
                return true;
            }
        }
        return false;
    }

    public static Room GetOneByName(String name)
    {
        List<Room> rooms = GetAll();
        foreach (Room room in rooms)
        {
            if (room.Name == name)
            {
                return rooms[rooms.IndexOf(room)];
            }
        }
        return null;
    }
    /*public static bool IsAvailable(DateTime start, DateTime end) // proverava da li je RoomComboBox slobodna izmedju neka dva trenutka u vremenu
    {
        if (start.Equals(end))
            return true;
        bool retVal = true;
        List<Appointment> appointments = AppointmentFileRepository.GetAll();
        foreach (Appointment appointment in appointments)
        {
            if (appointment.Room.Equals(this) && appointment.AppointmentStatus == AppointmentStatus.scheduled)
            {
                if (start >= appointment.AppointmentDate && start <= appointment.AppointmentEnd)
                {
                    retVal = false;
                    break;
                }
                if (end >= appointment.AppointmentDate && end <= appointment.AppointmentEnd)
                {
                    retVal = false;
                    break;
                }
                if (start <= appointment.AppointmentDate && end >= appointment.AppointmentEnd)
                {
                    retVal = false;
                    break;
                }
            }
        }
        return retVal;
    }*/

}