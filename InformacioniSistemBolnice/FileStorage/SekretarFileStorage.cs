// File:    SekretarFileStorage.cs
// Author:  Korisnik
// Created: Saturday, March 27, 2021 2:10:32 PM
// Purpose: Definition of Class SekretarFileStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class SekretarFileStorage
{
    private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "sekretari.json";
    public static List<Sekretar> GetAll()
   {
        if (!File.Exists(startupPath))
        {
            var tmp = File.OpenWrite(startupPath);
            tmp.Close();
        }
        List<Sekretar> sekretari;
        String procitano = File.ReadAllText(startupPath);
        if (procitano.Equals(""))
        {
            sekretari = new List<Sekretar>();
        }
        else
        {
            sekretari = JsonConvert.DeserializeObject<List<Sekretar>>(procitano);
        }
        return sekretari;
    }
   
   public Sekretar GetOne(string korisnickoIme)
   {
        List<Sekretar> sekretari = GetAll();
        foreach (Sekretar s in sekretari)
        {
            if (s.korisnickoIme.Equals(korisnickoIme))
                return s;
        }
        return null;
    }
   
   public Boolean RemoveSekretar(string korisnickoIme)
   {
        List<Sekretar> sekretari = GetAll();
        foreach (Sekretar s in sekretari)
        {
            if (s.korisnickoIme.Equals(korisnickoIme))
            {
                sekretari[sekretari.IndexOf(s)].isDeleted = true;
                Save(sekretari);
                return true;
            }
        }
        return false;
    }
   
   public Boolean AddSekretar(Sekretar noviSekretar)
   {
        List<Sekretar> sekretari = GetAll();
        sekretari.Add(noviSekretar);
        Save(sekretari);
        return true;
    }
   
   public Boolean UpdateSekretar(string korisnickoIme, Sekretar noviSekretar)
   {
        List<Sekretar> sekretari = GetAll();
        foreach (Sekretar s in sekretari)
        {
            if (s.korisnickoIme.Equals(korisnickoIme))
            {
                sekretari[sekretari.IndexOf(s)] = noviSekretar;
                Save(sekretari);
                return true;
            }
        }
        return false;
    }
    private static void Save(List<Sekretar> lista)
    {
        string upis = JsonConvert.SerializeObject(lista);
        File.WriteAllText(startupPath, upis);
    }

}