using InformacioniSistemBolnice.Korisnik;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class ObavestenjeFileStorage
{
    private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "obavestenja.json";

    public static List<Obavestenje> GetAll()
    {
        if (!File.Exists(startupPath))
        {
            var tmp = File.OpenWrite(startupPath);
            tmp.Close();
        }
        List<Obavestenje> obavestenja;
        String procitano = File.ReadAllText(startupPath);
        if (procitano.Equals(""))
        {
            obavestenja = new List<Obavestenje>();
        }
        else
        {
            obavestenja = JsonConvert.DeserializeObject<List<Obavestenje>>(procitano);
        }
        return obavestenja;
    }

    public static Obavestenje GetOne(int idObavestenja)
    {
        List<Obavestenje> obavestenja = GetAll();
        foreach (Obavestenje o in obavestenja)
        {
            if (o.idObavestenja.Equals(idObavestenja))
                return obavestenja[obavestenja.IndexOf(o)];
        }
        return null;
    }

    public static Boolean RemoveObavestenje(int idObavestenja)
    {
        List<Obavestenje> obavestenja = GetAll();
        foreach (Obavestenje o in obavestenja)
        {
            if (o.idObavestenja.Equals(idObavestenja))
            {
                obavestenja[obavestenja.IndexOf(o)].isDeleted = true;
                Save(obavestenja);
                return true;
            }
        }
        return false;
    }

    public static Boolean AddObavestenje(Obavestenje noviObavestenje)
    {
        List<Obavestenje> obavestenja = GetAll();
        obavestenja.Add(noviObavestenje);
        Save(obavestenja);
        return true;
    }

    public static Boolean UpdateObavestenje(int idObavestenja, Obavestenje noviObavestenje)
    {
        List<Obavestenje> obavestenja = GetAll();
        foreach (Obavestenje o in obavestenja)
        {
            if (o.idObavestenja.Equals(idObavestenja))
            {
                obavestenja[obavestenja.IndexOf(o)] = noviObavestenje;
                Save(obavestenja);
                return true;
            }
        }
        return false;
    }

    private static void Save(List<Obavestenje> lista)
    {
        string upis = JsonConvert.SerializeObject(lista);
        File.WriteAllText(startupPath, upis);
    }

}