// File:    RenovationPeriodFileStorage.cs
// Author:  Tamara
// Created: Monday, May 17, 2021 6:47:54 PM
// Purpose: Definition of Class RenovationPeriodFileStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class RenovationPeriodFileStorage
{
    private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "renovationperiod.json";

    public static List<RenovationPeriod> GetAll()
   {
        if (!File.Exists(startupPath))
        {
            var tmp = File.OpenWrite(startupPath);
            tmp.Close();
        }
        List<RenovationPeriod> renovationPeriods;
        String read = File.ReadAllText(startupPath);
        if (read.Equals(""))
        {
            renovationPeriods = new List<RenovationPeriod>();
        }
        else
        {
            renovationPeriods = JsonConvert.DeserializeObject<List<RenovationPeriod>>(read);
        }
        return renovationPeriods;
    }

    public static RenovationPeriod GetOne(Room room)
    {
        List<RenovationPeriod> renovationPeriods = GetAll();
        foreach (RenovationPeriod rp in renovationPeriods)
        {
            if (rp.Room.Equals(room))
            {
                return renovationPeriods[renovationPeriods.IndexOf(rp)];
            }
        }
        return null;
    }

    public static Boolean RemoveRenovationPeriod(Room room)
    {
        List<RenovationPeriod> renovationPeriods = GetAll();
        foreach (RenovationPeriod rp in renovationPeriods)
        {
            if (rp.Room.Equals(room))
            {
                renovationPeriods[renovationPeriods.IndexOf(rp)].IsDeleted = false;
                Save(renovationPeriods);
                return true;
            }
        }
        return false;
    }

    private static void Save(List<RenovationPeriod> renovationPeriods)
    {
        string write = JsonConvert.SerializeObject(renovationPeriods);
        File.WriteAllText(startupPath, write);
    }

    public static Boolean AddRenovationPeriod(RenovationPeriod newRenovationPeriod)
    {
        List<RenovationPeriod> renovationPeriods = GetAll();
        renovationPeriods.Add(newRenovationPeriod);
        Save(renovationPeriods);
        return true;
    }

    public static Boolean UpdateRenovationPeriod(Room room, RenovationPeriod newRenovationPeriod)
    {
        List<RenovationPeriod> renovationPeriods = GetAll();
        foreach (RenovationPeriod rp in renovationPeriods)
        {
            if (rp.Room.Equals(room))
            {
                renovationPeriods[renovationPeriods.IndexOf(rp)] = newRenovationPeriod;
                Save(renovationPeriods);
                return true;
            }
        }
        return false;
    }
}