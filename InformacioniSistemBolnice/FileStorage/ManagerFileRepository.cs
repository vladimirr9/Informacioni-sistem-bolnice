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

        List<Manager> managerList;
        String read = File.ReadAllText(startupPath);
        if (read.Equals(""))
        {
            managerList = new List<Manager>();
        }
        else
        {
            managerList = JsonConvert.DeserializeObject<List<Manager>>(read);
        }
        return managerList;
    }
   
   public static Manager GetOne(String username)
   {
        List<Manager> managerList = GetAll();
        foreach (Manager manager in managerList)
        {
            if (manager.korisnickoIme.Equals(username))
                return manager;
        }
        return null;
    }
   
   public static Boolean RemoveManager(int managerId)
   {
      throw new NotImplementedException();
   }
   
   public static Boolean AddUpravnik(Manager noviUpravnik)
   {
      throw new NotImplementedException();
   }
   
   public static Boolean UpdateUpravnik(int iDUpravnika, Manager noviUpravnik)
   {
      throw new NotImplementedException();
   }

}