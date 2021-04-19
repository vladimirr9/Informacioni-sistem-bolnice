// File:    ProstorijaFileStorage.cs
// Author:  Korisnik
// Created: Monday, March 22, 2021 7:51:09 PM
// Purpose: Definition of Class ProstorijaFileStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class ProstorijaFileStorage
{
    private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "prostorije4.json";

    public static List<Prostorija> GetAll()
   {
        if (!File.Exists(startupPath))
        {
            var tmp = File.OpenWrite(startupPath);
            tmp.Close();
        }
        List<Prostorija> prostorijeLista;
        String procitano = File.ReadAllText(startupPath);
        if (procitano.Equals(""))
        {
            prostorijeLista = new List<Prostorija>();
        }
        else
        {
            prostorijeLista = JsonConvert.DeserializeObject<List<Prostorija>>(procitano);
        }
        return prostorijeLista;
    }
   
   public static Prostorija GetOne(int iDProstorije)
   {
        List<Prostorija> prostorije = GetAll();
        foreach (Prostorija p in prostorije)
        {
            if (p.IDprostorije == iDProstorije)
            {
                return prostorije[prostorije.IndexOf(p)];
            }
        }
        return null;
    }

    public static Boolean RemoveProstorija(int iDProstorije)
   {
        List<Prostorija> prostorije = GetAll();
        foreach (Prostorija p in prostorije)
        {
            if (p.IDprostorije == iDProstorije)
            {
                prostorije[prostorije.IndexOf(p)].IsDeleted = true;
                prostorije[prostorije.IndexOf(p)].IsActive = false;
                Save(prostorije);
                return true;
            }
        }
        return false;
    }

    private static void Save(List<Prostorija> prostorije)
    {
        string upisivanje = JsonConvert.SerializeObject(prostorije);
        File.WriteAllText(startupPath, upisivanje);
    }

    public static Boolean AddProstorija(Prostorija novaProstorija)
   {
        List<Prostorija> prostorije = GetAll();
        prostorije.Add(novaProstorija);
        Save(prostorije);
        return true;
    }
   
   public static Boolean UpdateProstorija(int iDProstorije, Prostorija novaProstorija)
   {
        List<Prostorija> prostorije = GetAll();
        foreach (Prostorija p in prostorije)
        {
            if (p.IDprostorije == iDProstorije)
            {
                prostorije[prostorije.IndexOf(p)] = novaProstorija;
                Save(prostorije);
                return true;
            }
        }
        return false;
    }

}