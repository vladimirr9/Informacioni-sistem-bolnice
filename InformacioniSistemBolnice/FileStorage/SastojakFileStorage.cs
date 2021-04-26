using InformacioniSistemBolnice;
using InformacioniSistemBolnice.Korisnik;
using InformacioniSistemBolnice.Lekar;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class SastojakFileStorage
{
    private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "sastojci.json";

    public static List<Sastojak> GetAll()
    {
        if (!File.Exists(startupPath))
        {
            var tmp = File.OpenWrite(startupPath);
            tmp.Close();
        }
        List<Sastojak> sastojci;
        String procitano = File.ReadAllText(startupPath);
        if (procitano.Equals(""))
        {
            sastojci = new List<Sastojak>();
        }
        else
        {
            sastojci = JsonConvert.DeserializeObject<List<Sastojak>>(procitano);
        }
        return sastojci;
    }

    public static Sastojak GetOne(int id)
    {
        List<Sastojak> sastojci = GetAll();
        foreach (Sastojak a in sastojci)
        {
            if (a.id.Equals(id))
                return sastojci[sastojci.IndexOf(a)];
        }
        return null;
    }

    public static Boolean RemoveAlergen(int id)
    {
        List<Sastojak> sastojci = GetAll();
        foreach (Sastojak a in sastojci)
        {
            if (a.id.Equals(id))
            {
                sastojci[sastojci.IndexOf(a)].isDeleted = true;
                Save(sastojci);
                return true;
            }
        }
        return false;
    }

    public static Boolean AddSastojak(Sastojak noviAlergen)
    {
        List<Sastojak> sastojci = GetAll();
        sastojci.Add(noviAlergen);
        Save(sastojci);
        return true;
    }

    public static Boolean UpdateSastojak(int id, Sastojak noviSastojak)
    {
        List<Sastojak> sastojci = GetAll();
        foreach (Sastojak a in sastojci)
        {
            if (a.id.Equals(id))
            {
                sastojci[sastojci.IndexOf(a)] = noviSastojak;
                Save(sastojci);
                return true;
            }
        }
        return false;
    }

    private static void Save(List<Sastojak> lista)
    {
        string upis = JsonConvert.SerializeObject(lista);
        File.WriteAllText(startupPath, upis);
    }

}