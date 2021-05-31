using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RenovationperiodFileRepository
{
    private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "renovationPeriod.json";

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

    public static RenovationPeriod GetOne(int roomId)
    {
        List<RenovationPeriod> renovationPeriods = GetAll();
        foreach (RenovationPeriod renPer in renovationPeriods)
        {
            if (renPer.Room.RoomId == roomId)
            {
                return renovationPeriods[renovationPeriods.IndexOf(renPer)];
            }
        }
        return null;
    }

    public static Boolean RemoveRenovationPeriod(int roomId)
    {
        List<RenovationPeriod> renovationPeriods = GetAll();
        foreach (RenovationPeriod renPer in renovationPeriods)
        {
            if (renPer.Room.RoomId == roomId)
            {
                renovationPeriods[renovationPeriods.IndexOf(renPer)].IsDeleted = true;
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
        List<RenovationPeriod> renovatonPeriods = GetAll();
        renovatonPeriods.Add(newRenovationPeriod);
        Save(renovatonPeriods);
        return true;
    }

    public static Boolean UpdateRenovationPeriod(int roomId, RenovationPeriod newRenovationPeriod)
    {
        List<RenovationPeriod> renovationPeriods = GetAll();
        foreach (RenovationPeriod renPer in renovationPeriods)
        {
            if (renPer.Room.RoomId == roomId)
            {
                renovationPeriods[renovationPeriods.IndexOf(renPer)] = newRenovationPeriod;
                Save(renovationPeriods);
                return true;
            }
        }
        return false;
    }
}

