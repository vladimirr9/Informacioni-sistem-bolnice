// File:    ProstorijaFileStorage.cs
// Author:  User
// Created: Monday, March 22, 2021 7:51:09 PM
// Purpose: Definition of Class ProstorijaFileStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using InformacioniSistemBolnice.FileStorage;

public class RoomFileRepository : IRoomRepository
{
    private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "rooms.json";

    public List<Room> GetAll()
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
   
   public Room GetOne(int roomId)
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

    public Boolean Remove(int roomId)
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

    private void Save(List<Room> rooms)
    {
        string write = JsonConvert.SerializeObject(rooms);
        File.WriteAllText(startupPath, write);
    }

    public Boolean Add(Room newRoom)
   {
        List<Room> rooms = GetAll();
        rooms.Add(newRoom);
        Save(rooms);
        return true;
    }
   
   public Boolean Update(int roomId, Room newRoom)
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

    public Room GetOneByName(String name)
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

}