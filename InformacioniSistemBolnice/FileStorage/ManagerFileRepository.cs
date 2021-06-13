// File:    UpravnikFileStorage.cs
// Author:  User
// Created: Saturday, March 27, 2021 2:15:03 PM
// Purpose: Definition of Class UpravnikFileStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using InformacioniSistemBolnice.FileStorage;

public class ManagerFileRepository : GenericUserRepository<Manager>
{
    public ManagerFileRepository()
    {
        StartupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "managers.json";

    }

    /*
    private string _startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "managers.json";

    public List<Manager> GetAll()
   {
        if (!File.Exists(_startupPath))
        {
            var tmp = File.OpenWrite(_startupPath);
            tmp.Close();
        }

        List<Manager> managers;
        String read = File.ReadAllText(_startupPath);
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
   
   public Manager GetOne(String username)
   {
        List<Manager> managers = GetAll();
        foreach (Manager u in managers)
        {
            if (u.Username.Equals(username))
                return u;
        }
        return null;
    }
   
   public Boolean RemoveManager(int managerId)
   {
      throw new NotImplementedException();
   }
   
   public Boolean AddManager(Manager newManager)
   {
       List<Manager> managers = GetAll();
       managers.Add(newManager);
       Save(managers);
       return true;
    }
   
   public Boolean UpdateManager(int managerId, Manager newManager)
   {
      throw new NotImplementedException();
   }
   private void Save(List<Manager> managers)
   {
       string serializeObject = JsonConvert.SerializeObject(managers);
       File.WriteAllText(_startupPath, serializeObject);
   }
   */

}