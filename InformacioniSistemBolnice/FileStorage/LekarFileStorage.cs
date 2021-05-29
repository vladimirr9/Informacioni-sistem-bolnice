// File:    LekarFileStorage.cs
// Author:  Korisnik
// Created: Saturday, March 27, 2021 2:13:31 PM
// Purpose: Definition of Class LekarFileStorage

using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class LekarFileStorage
{
    private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "lekari.json";
    public static List<Doctor> GetAll()
    {
        if (!File.Exists(startupPath))
        {
            var tmp = File.OpenWrite(startupPath);
            tmp.Close();
        }
        List<Doctor> lekari;
        String procitano = File.ReadAllText(startupPath);
        if (procitano.Equals(""))
        {
            lekari = new List<Doctor>();
        }
        else
        {
            lekari = JsonConvert.DeserializeObject<List<Doctor>>(procitano);
        }
        return lekari;
    }

    public static Doctor GetOne(string korisnickoIme)
    {
        List<Doctor> lekari = GetAll();
        foreach (Doctor l in lekari)
        {
            if (l.korisnickoIme.Equals(korisnickoIme))
                return l;
        }
        return null;
    }

    public static Boolean RemoveLekar(string korisnickoIme)
    {
        List<Doctor> lekari = GetAll();
        foreach (Doctor l in lekari)
        {
            if (l.korisnickoIme.Equals(korisnickoIme))
            {
                lekari[lekari.IndexOf(l)].isDeleted = true;
                Save(lekari);
                return true;
            }
        }
        return false;
    }

    public static Boolean AddLekar(Doctor newDoctor)
    {
        List<Doctor> lekari = GetAll();
        lekari.Add(newDoctor);
        Save(lekari);
        return true;
    }

    public static Boolean UpdateLekar(string korisnickoIme, Doctor newDoctor)
    {
        List<Doctor> lekari = GetAll();
        foreach (Doctor l in lekari)
        {
            if (l.korisnickoIme.Equals(korisnickoIme))
            {
                lekari[lekari.IndexOf(l)] = newDoctor;
                Save(lekari);
                return true;
            }
        }
        return false;
    }

    private static void Save(List<Doctor> lista)
    {
        string upis = JsonConvert.SerializeObject(lista);
        File.WriteAllText(startupPath, upis);
    }

}