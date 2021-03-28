// File:    PacijentFileStorage.cs
// Author:  Korisnik
// Created: Saturday, March 27, 2021 1:58:44 PM
// Purpose: Definition of Class PacijentFileStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class PacijentFileStorage
{
    private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "pacijenti.json";

    public static List<Pacijent> GetAll()
    {
        if (!File.Exists(startupPath))
        {
            var tmp = File.OpenWrite(startupPath);
            tmp.Close();
        }
        List<Pacijent> pacijenti;
        String procitano = File.ReadAllText(startupPath);
        if (procitano.Equals(""))
        {
            pacijenti = new List<Pacijent>();
        }
        else
        {
            pacijenti = JsonConvert.DeserializeObject<List<Pacijent>>(procitano);
        }
        return pacijenti;
    }

    public static Pacijent GetOne(string korisnickoIme)
    {
        List<Pacijent> pacijenti = GetAll();
        foreach (Pacijent p in pacijenti)
        {
            if (p.korisnickoIme.Equals(korisnickoIme))
                return pacijenti[pacijenti.IndexOf(p)];
        }
        return null;
    }

    public static Boolean RemovePacijent(string korisnickoIme)
    {
        List<Pacijent> pacijenti = GetAll();
        foreach (Pacijent p in pacijenti)
        {
            if (p.korisnickoIme.Equals(korisnickoIme))
            {
                pacijenti[pacijenti.IndexOf(p)].isDeleted = true;
                Save(pacijenti);
                return true;
            }
        }
        return false;
    }

    public static Boolean AddPacijent(Pacijent noviPacijent)
    {
        List<Pacijent> pacijenti = GetAll();
        pacijenti.Add(noviPacijent);
        Save(pacijenti);
        return true;
    }

    public static Boolean UpdatePacijent(string korisnickoIme, Pacijent noviPacijent)
    {
        List<Pacijent> pacijenti = GetAll();
        foreach (Pacijent p in pacijenti)
        {
            if (p.korisnickoIme.Equals(korisnickoIme))
            {
                pacijenti[pacijenti.IndexOf(p)] = noviPacijent;
                Save(pacijenti);
                return true;
            }
        }
        return false;
    }

    private static void Save(List<Pacijent> lista)
    {
        string upis = JsonConvert.SerializeObject(lista);
        File.WriteAllText(startupPath, upis);
    }

}