// File:    UpravnikFileStorage.cs
// Author:  Korisnik
// Created: Saturday, March 27, 2021 2:15:03 PM
// Purpose: Definition of Class UpravnikFileStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class ManagerFileRepository
{
    private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "upravnik.json";

    public static List<Manager> GetAll()
   {
        if (!File.Exists(startupPath))
        {
            var tmp = File.OpenWrite(startupPath);
            tmp.Close();
        }

        List<Manager> managers;
        String read = File.ReadAllText(startupPath);
        if (read.Equals(""))
        {
            managers = new List<Manager>();
        }
        else
        {
            managers = JsonConvert.DeserializeObject<List<Manager>>(read);
        }
        return managers;
    }
   
   public static Manager GetOne(String username)
   {
        List<Manager> managers = GetAll();
        foreach (Manager u in managers)
        {
            if (u.korisnickoIme.Equals(username))
                return u;
        }
        return null;
    }
   
   public static Boolean RemoveManager(int managerId)
   {
      throw new NotImplementedException();
   }
   
   public static Boolean AddManager(Manager newmanager)
   {
      throw new NotImplementedException();
   }
   
   public static Boolean UpdateManager(int managerId, Manager newManager)
   {
      throw new NotImplementedException();
   }

}